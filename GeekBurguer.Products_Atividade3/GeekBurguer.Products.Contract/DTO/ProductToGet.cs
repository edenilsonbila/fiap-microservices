using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBurger.Service.Contract.DTO
{
    public class ProductToGet
    {
        public ProductToGet()
        {
            Items = new List<ItemToGet>();
        }
        public Guid StoreId { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public List<ItemToGet> Items { get; set; }
        public decimal Price { get; set; }
    }
}
