namespace FoodSearchTutorial.Model;

public class Settings //modèle permettant de gérer les configurations de notre app
{
    public static string NutriScore //propriété static afin de pouvoir y accéder partout sans devoir instancier l'objet settings 
    {
        get => Preferences.Get(nameof(NutriScore), "ALL"); //preferences permet de définir des préférences utilisateurs qui sont save dans l'app (même si on coupe et relance l'app)
        set => Preferences.Set(nameof(NutriScore), value);
    }
}