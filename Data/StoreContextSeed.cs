using project.Entities;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace project.Data
{
    public class StoreContextSeed
    {
        public static async Task seedData(StoreDbContext context)
        {
           if (!context.Productbrand.Any())
            {
                var brandData = File.ReadAllText("Data/SeedData/brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandData);
                context.Productbrand.AddRange(brands);
                await context.SaveChangesAsync();

            }
            if (!context.Producttype.Any())
            {
                var ProductType = File.ReadAllText("Data/SeedData/types.json");
                var Type = JsonSerializer.Deserialize<List<ProductType>>(ProductType);
                context.Producttype.AddRange(Type);
                await context.SaveChangesAsync();
            }

                if (!context.Products.Any())
                {
                    var ProductData = File.ReadAllText("Data/SeedData/products.json");
                    var product = JsonSerializer.Deserialize<List<Product>>(ProductData);
                    context.Products.AddRange(product);
                await context.SaveChangesAsync();

            }

          
            }
        }
    }

