using FoodSearchTutorial.ViewModel;

namespace FoodSearchTutorial.View;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(ProductDetailsViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}