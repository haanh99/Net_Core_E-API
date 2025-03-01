using Core.Entities;
using System.Text.Json;
using System.IO;

namespace Infrastructure.Data;

public class StoreContextSeed
{
 
    public static async Task SeedAsync(StoreContext context){
        if (!context.Products.Any())
        {
            var productsData = await File.ReadAllTextAsync("../Infrastructure/Data/SeedData/products.json");

            var products = JsonSerializer.Deserialize<List<Product>>(productsData);

            if (products == null) return;
            foreach (var item in products){
                context.Products.Add(item);
            }

            await context.SaveChangesAsync();
        }
    }
}
