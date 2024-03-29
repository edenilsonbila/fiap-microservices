﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekBurguer.Products.Models
{
    public class Item
    {
        [Key]
        public Guid ItemId { get; set; }
        public string Name { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

    }
}
