using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GeekBurguer.Ingredientes.Model
{
    public class Item
    {
        [Key]
        public Guid ItemId { get; set; }
        public string Name { get; set; }
    }
}
