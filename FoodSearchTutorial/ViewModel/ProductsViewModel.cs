using FoodSearchTutorial.Services;
using FoodSearchTutorial.View;
using System.Diagnostics;

namespace FoodSearchTutorial.ViewModel;

[QueryProperty("SearchTerm", "SearchTerm")] //requête réalisée par la main page lors d'une recherche spécifique de produit

public partial class ProductsViewModel : BaseViewModel
{
    ProductService productService;

    public ObservableCollection<Product> Products { get; } = new(); //instancie une nouvelle list par défaut lors de l'instanciation du view model 

    public ObservableCollection<Product> SearchedProducts { get; } = new();

    public bool FirstRun { get; set; } = true;

    [ObservableProperty]
    bool isRefreshing;

    [ObservableProperty]
    string searchTerm;

    [ObservableProperty]
    string searchedTitle;

    public ProductsViewModel(ProductService productService) //injection du service dans ma page 
    {
        Title = "Produits"; //donne le titre de la page Produit lors de la génération de la page
        this.productService = productService;
    }

    [RelayCommand]
    async Task GetRandomProductsAsync() //permet de choisir de manière random 10 produit a afficher sur la main page
    {
        if (IsBusy) //vérifie si la page n'est pas déjà utilisée
            return;

        try
        {
            IsBusy = true; //déclre qu'on est en train de travailler

            var products = await productService.GetRandomProductsAsync();

            Products.Clear(); //n'affiche que 10 produits max

            foreach (var product in products)
                Products.Add(product);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Erreur", "Impossible d'importer les produits", "OK"); //indique le messge d'erreur en cas de débuggage
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        } //libère la page et permet ed recommencer à y travailler si nécessaire et de stopper le rafraichissement
    }

    [RelayCommand]
    async Task SearchProductsAsync() //fonction permettant la recherche du produit spécifique
    {
        if (IsBusy)
            return;

        try
        {
            IsBusy = true;

            SearchedTitle = SearchTerm;
            Title = SearchedTitle;

            SearchedProducts.Clear();

            var products = await productService.SearchProductsAsync(SearchTerm);

            foreach (var product in products)
                SearchedProducts.Add(product);

            SearchTerm = null;
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Erreur", "Impossible de rechercher des produits", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    async Task GoToDetailsAsync(Product product) //envoi vers la page DetailsProduit si un produit est sélectionné
    {
        if (product is null)
            return;

        await Shell.Current.GoToAsync($"{nameof(DetailsPage)}", true, new Dictionary<string, object>
        {
            { "Product", product }
        });
    }
}
