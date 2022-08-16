namespace GeekBurguer.Ingredientes.Model
{
    public class ItemIngredients
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        public List<string> Ingredients { get; set; }
    }
}
