using System.Diagnostics;

namespace FoodSearchTutorial.ViewModel
{
    [QueryProperty("Product", "Product")] //requête réalisée par la main page
    public partial class ProductDetailsViewModel : BaseViewModel
    {
        [ObservableProperty]
        Product product; //le produit pour lequle on va afficher le détail

        [RelayCommand]
        async Task OpenProductAsync(Product product) //Fonction qui va permettre d'ouvrir la page d'open foodfact lorsqu'on appuie sur le lien de la fiche produit détalilée
        {
            if (product is null)
                return;

            try
            {
                Uri uri = new(product.Url);
                await Browser.Default.OpenAsync(uri, BrowserLaunchMode.SystemPreferred);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await Shell.Current.DisplayAlert("Error", "Unable to open browser", "OK");
            }
        }
    }
}
