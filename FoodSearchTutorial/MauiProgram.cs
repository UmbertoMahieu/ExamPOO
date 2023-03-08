namespace FoodSearchTutorial
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<ProductService>();

            builder.Services.AddSingleton<ProductsViewModel>();
            builder.Services.AddTransient<MainPage>(); //injection de dépendance pour l'utilisation de notre Main Page. Lorsqu'on ouvre l'app et qu'on navigue dedans, on aura un état variable selon d'autre facture

            builder.Services.AddSingleton<ProductsSearchViewModel>();
            builder.Services.AddSingleton<SearchPage>(); //Service pour la searchpage

            builder.Services.AddTransient<ProductDetailsViewModel>(); //On utilise un service Transcient parce que la page sera différente à chaque rafraichissement
            builder.Services.AddTransient<DetailsPage>();

            builder.Services.AddSingleton<SettingsViewModel>();
            builder.Services.AddSingleton<SettingsPage>();

            return builder.Build();
        }
    }

}