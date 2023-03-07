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
            builder.Services.AddSingleton<MainPage>(); //injection de dépendance pour l'utilisation de notre Main Page. Lorsqu'on ouvre l'app et qu'on navigue dedans, on aura tjrs le même état 

            builder.Services.AddSingleton<ProductsViewModel>();

            return builder.Build();
        }
    }
}