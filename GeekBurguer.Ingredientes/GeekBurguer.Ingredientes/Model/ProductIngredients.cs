using System.ComponentModel.DataAnnotations;

namespace GeekBurguer.Ingredientes.Model
{
    public class ProductIngredients
    {
        [Key]
        public Guid ProductId { get; set; }
        public string StoreName { get; set; }

        public List<string> Ingredients { get; set; }
    }
}
