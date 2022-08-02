using System.ComponentModel.DataAnnotations;

namespace GeekBurguer.Ingredientes.Model
{
    public class Ingredients
    {
        [Key]
        public Guid ProductId { get; set; }
        public string StoreName { get; set; }

        public List<Item> Items { get; set; }
    }
}
