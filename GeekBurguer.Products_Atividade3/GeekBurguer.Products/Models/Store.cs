using System;
using System.ComponentModel.DataAnnotations;

namespace GeekBurguer.Products.Models
{
    public class Store
    {
        [Key]
        public Guid StoreId { get; set; }

        public string Name { get; set; }
    }
}
