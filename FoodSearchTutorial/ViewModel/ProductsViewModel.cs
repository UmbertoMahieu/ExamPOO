using FoodSearchTutorial.Services;
using FoodSearchTutorial.View;
using System.Diagnostics;

namespace FoodSearchTutorial.ViewModel;

public partial class ProductsViewModel : BaseViewModel
{
    ProductService productService;

    public bool FirstRun { get; set; } = true;
    public ObservableCollection<Product> Products { get; set; } = new(); //instancie une nouvelle list par défaut lors de l'instanciation du view model 
    
    [ObservableProperty]
    bool isRefreshing;
    public ProductsViewModel(ProductService productService) //injection du service dans ma page 
    {
        Title = "Produits"; //donne le titre de la page Produit lors de la génération de la page
        this.productService = productService;
    }

    [RelayCommand]
    async Task GetRandomProductsAsync()
    {
        if(IsBusy) return; //vérifie si la page n'est pas déjà utilisée

        try
        {
            IsBusy = true; //déclre qu'on est en train de travailler

            var products = await productService.GetRandomProductsAsync();

            products.Clear(); //n'affiche que 10 produits max

            foreach (var product in products)
            {
                products.Add(product);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("erreur", "impossible de récupérer la liste de produit", "OK"); //indique le messge d'erreur en cas de débuggage
        }
        finally
        { 
           IsBusy = false;
           IsRefreshing= false;
        } //libère la page et permet ed recommencer à y travailler si nécessaire et de stopper le rafraichissement

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
}
