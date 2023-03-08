namespace FoodSearchTutorial.View;


public partial class SettingsPage : ContentPage
{
    public SettingsPage()//SettingsViewModel viewModel)
    {
        InitializeComponent();
        //BindingContext = viewModel;
    }

    protected override void OnAppearing() //m�thode qui d�clenche la s�lection des nutriscore dispo dans les settings lorsqu'on affiche la page
    {
        var nutriScore = Preferences.Get("NutriScore", "ALL");

        var radioButtons = nutriScoreStackLayout.Children.Where(c => c is RadioButton);

        foreach (RadioButton radioButton in radioButtons)
        {
            if (radioButton.Value.ToString() == nutriScore)
            {
                radioButton.IsChecked = true;
                break;
            }
        }

        base.OnAppearing();
    }

    private void OnNutriScoreCheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var nutriScore = ((RadioButton)sender).Value.ToString();

        Settings.NutriScore = nutriScore;
    }
}