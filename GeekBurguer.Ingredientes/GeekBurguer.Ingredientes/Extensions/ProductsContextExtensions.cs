using GeekBurguer.Ingredientes.Model;
using GeekBurguer.Ingredientes.Repository.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeekBurguer.Ingredientes.Extensions
{
    public static class ProductsContextExtensions
    {
        public static void Seed(this ProductsDbContext context)
        {
            /*
            context.ProductIgredients.RemoveRange(context.ProductIgredients);

            context.SaveChanges();

            context.ProductIgredients.AddRange(
             new List<Ingredients> {
            new Ingredients { ProductId = new  Guid("0d61770a-9c75-4922-a1bd-b1b853a7e04c"),
            StoreName = "Paulista",
                Items = new List<Item> {
                    new Item { ItemId = new Guid("95c746a9-d2e2-489b-9edf-1df502aa21df"), Name = "beef" }
                }
            }
            });

            context.SaveChanges();
            */
        }
    }
}
