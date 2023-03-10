using System.Net.Http.Json;

namespace FoodSearchTutorial.Services;

public class ProductService //permet d'accéder et d'aller chercher les données depuis foodfactProduct
{
    const string BASE_SEARCH_URL = "https://fr.openfoodfacts.org/cgi/search.pl?action=process&json=true";

    HttpClient httpClient;

    public ProductService()
    {
        httpClient = new HttpClient();
    }

    public async Task<List<Product>> GetRandomProductsAsync() //défini un nombre entre 1 & 1000 pour chopper des produits random
    {
        var page = new Random().Next(1, 1000);

        var url = $"{BASE_SEARCH_URL}&page={page}&page_size=10&{ProductService.GetNutriScoreFilter(0)}";

        var response = await httpClient.GetAsync(url);

        var products = await GetProductsAsync(response);

        return products;
    }

    public async Task<List<Product>> SearchProductsAsync(string searchTerm) //va chercher la liste de produit sur le site 
    {

        var url = $"{BASE_SEARCH_URL}&tag_contains_0=contains&tagtype_0=categories" +
            $"&tagtype_1=label&tag_0={searchTerm}&{ProductService.GetNutriScoreFilter(2)}&page_size=10";

        var response = await httpClient.GetAsync(url);

        var products = await GetProductsAsync(response);

        return products;
    }

    private static async Task<List<Product>> GetProductsAsync(HttpResponseMessage response) //met la liste de produict dans la variable produits
    {
        List<Product> products = new();

        if (response.IsSuccessStatusCode) //si la réponse est bonne, alors on met dans la liste mes produits
        {
            products = (await response.Content.ReadFromJsonAsync<ProductsResult>()).Products; //convertit le json
        }

        return products;
    }
    
    private static string GetNutriScoreFilter(int tagId) //méthod qui permet de récupérer les produits selon le nutriscore sélectionné
    {
        var nutriScore = Settings.NutriScore;

        return ("ALL" == nutriScore) ? string.Empty : $"tagtype_{tagId}=nutrition_grades&tag_contains_{tagId}=contains&tag_{tagId}={nutriScore}"; //on rajoute le nutriscore souhaité à la recherche url 
    }
}
