using FakeStoreAPI;
using System.Text.Encodings.Web;
using System.Text.Json;

Console.WriteLine("Retrieving products");

// get products from the api
var products = await StoreApi.GetAllProducts();

// check if getting the products failed
if (products == null)
{
    Console.WriteLine("Getting products failed.");
    return;
}

// check if the result was empty
if (products.Length == 0)
{
    Console.WriteLine("No products found.");
    return;
}

// 1. sort the products by price
// 2. group the products by category
// 3. transform into a dictionary and select only necessary properties
var groupedProducts = products.
    OrderBy(product => product.Price).
    GroupBy(product => product.Category).
    ToDictionary(group => group.Key, group => group.Select(product => new
    {
        product.Id,
        product.Title,
        product.Price
    }).ToArray());

// serialize the grouped products to JSON
string json = JsonSerializer.Serialize(groupedProducts, new JsonSerializerOptions(JsonSerializerDefaults.Web)
{
    WriteIndented = true,
    Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
});

// save to a json file
File.WriteAllText("grouped_products.json", json);

Console.WriteLine("Products saved.");