using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using homework22.Models;

namespace homework22.DbContext
{
    public static class DbSeed
    {
        public static async Task Seed(ShopDbContext context)
        {
            if (!context.Categories.Any())
            {
                await context.Categories.AddRangeAsync(new List<Category>
                {
                    new() {Name = "Meat"},
                    new() {Name = "Fruits and Vegetables"},
                    new() {Name = "Flavorings"},
                    new() {Name = "Edible fats"},
                    new() {Name = "Dairy"},
                    new() {Name = "Starch, sugar, honey and confectionery"},
                    new() {Name = "Bakery"},
                    new() {Name = "Fish and fish products"},
                });
                await context.SaveChangesAsync();
            }
        }
    }
}