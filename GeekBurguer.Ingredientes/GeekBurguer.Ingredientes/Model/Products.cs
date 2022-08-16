using System.ComponentModel.DataAnnotations;

namespace GeekBurguer.Ingredientes.Model
{
    public class Products
    {
        [Key]
        public Guid ProductId { get; set; }
        public string StoreName { get; set; }

        public List<ItemIngredients> Items { get; set; }
    }
}
