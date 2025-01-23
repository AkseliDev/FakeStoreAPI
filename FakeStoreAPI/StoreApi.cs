using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FakeStoreAPI;

internal class StoreApi
{
    const int MaxRetries = 5;

    private static HttpClient HttpClient = new HttpClient();

    /// <summary>
    /// Gets list of all products from the api
    /// </summary>
    /// <returns>List of products or <c>null</c> if the request failed.</returns>
    public static async Task<Product[]?> GetAllProducts()
    {
        Product[]? products = null;
        int retries = MaxRetries;

        do
        {

            // request the products from api and log errors
            try
            {
                products = await HttpClient.GetFromJsonAsync<Product[]>("https://fakestoreapi.com/products");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            // retry a couple times if getting the products failed

        } while (products is null && retries-- > 0);

        return products;
    }
}
