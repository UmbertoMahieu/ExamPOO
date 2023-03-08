using FoodSearchTutorial.ViewModel;

namespace FoodSearchTutorial.View;

public partial class MainPage : ContentPage
{
    ProductsViewModel viewModel;

    public MainPage(ProductsViewModel viewModel)
    {
        InitializeComponent();

        this.viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing() //méthode permettant d'afficher les produits au rafraichissement
    {
        productsCollection.ItemsSource = viewModel.Products;

        if (viewModel.FirstRun && viewModel.GetRandomProductsCommand.CanExecute(null)) //permet d'afficher directement les produits sans avoir à rafraichir (au premier lancement)
        {
            await viewModel.GetRandomProductsCommand.ExecuteAsync(null);
            viewModel.FirstRun = false;
        }

        base.OnAppearing();
    }

    protected override async void OnNavigatedTo(NavigatedToEventArgs args) // méthode qui se produit lorsqu'on navigue dans l'appli. 
    {
        if (!string.IsNullOrEmpty(viewModel.SearchTerm)) //on vérifie si on a fait une recherche (et si oui on appelle la fonction de recherche avec le searchterms quand on navigue vers cette page
        {
            await viewModel.SearchProductsCommand.ExecuteAsync(null);
        }

        if (Parent is ShellSection && ((ShellSection)Parent).Route == "IMPL_SearchPage") //Permet de détecter qu'on vient de naviguer depuis la barre de recherche et pas autrement
        {
            viewModel.Title = viewModel.SearchedTitle; //si la condition est respectée, on utilise le titre du produit recherché
            productsCollection.ItemsSource = viewModel.SearchedProducts;
        }
        else
        {
            viewModel.Title = "Produits";
            productsCollection.ItemsSource = viewModel.Products;
        }

        base.OnNavigatedTo(args);
    }
}