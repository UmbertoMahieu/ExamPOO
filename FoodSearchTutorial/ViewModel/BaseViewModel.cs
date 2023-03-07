
namespace FoodSearchTutorial.ViewModel;

public partial class BaseViewModel : ObservableObject
{
    public BaseViewModel()
    {
       
    }

    [ObservableProperty] //Attribut qui va observer la propriété ciblée. fait appel au code généré par le packageNugget
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]  
    bool isBusy; //permet de savoir si notre application est en train d'effectuer un traitement ou pas

    [ObservableProperty]
    string title; 
    public bool IsNotBusy => !isBusy; 
}
