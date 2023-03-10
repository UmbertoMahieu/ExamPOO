
namespace FoodSearchTutorial.ViewModel
{
    public partial class ProductsSearchViewModel : BaseViewModel
    {
        public ObservableCollection<string> SearchTermsHistory { get; } = new(); //on créé une collection afin de pouvoir afficher l'historique de recherche

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsSearchTermsHistoryNotEmpty))]
        bool isSearchTermsHistoryEmpty = true;

        public bool IsSearchTermsHistoryNotEmpty => !isSearchTermsHistoryEmpty;

        public ProductsSearchViewModel() //constructeur avec le titre de notre page
        {
            Title = "Rechercher";
        }

        [RelayCommand]
        async Task SearchProductsAsync(string searchTerm) //Commande qui permet de rechercher le produit indiqué
        {
            SearchTermsHistory.Add(searchTerm);

            IsSearchTermsHistoryEmpty = false;

            await Shell.Current.GoToAsync($"{nameof(MainPage)}", true, new Dictionary<string, object> //permet de se rendre sur la main page après la réalisation d'une recherche
            {
                { "SearchTerm", searchTerm }
            });
        }

        [RelayCommand]
        public void ClearSearchTermsHistory() //permet de suppirmer l'historique de recherche grace au boutton
        {
            SearchTermsHistory.Clear();

            IsSearchTermsHistoryEmpty = true;
        }
    }

}
