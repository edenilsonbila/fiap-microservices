﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekBurguer.Products.Contract;

namespace GeekBurguer.Products.Controllers
{
    [Route("api/products")]
    public class ProductsController : Controller
    {
        List<Product> products = new List<Product>();
        public ProductsController()
        {
            var paulistaStore = "Paulista";
            var morumbiStore = "Morumbi";

            var beef = new Item { ItemId = Guid.NewGuid(), Name = "beef" };
            var pork = new Item { ItemId = Guid.NewGuid(), Name = "pork" };
            var mustard = new Item { ItemId = Guid.NewGuid(), Name = "mustard" };
            var ketchup = new Item { ItemId = Guid.NewGuid(), Name = "ketchup" };
            var bread = new Item { ItemId = Guid.NewGuid(), Name = "bread" };
            var wBread = new Item { ItemId = Guid.NewGuid(), Name = "whole bread" };

            products = new List<Product>()
            {       
                new Product { ProductId = Guid.NewGuid(), Name = "Darth Bacon",
                    Image = "hamb1.png", StoreName = paulistaStore,
                    Items = new List<Item> {beef, ketchup, bread }
                },
                new Product { ProductId = Guid.NewGuid(), Name = "Cap. Spork",
                    Image = "hamb2.png", StoreName = paulistaStore,
                    Items = new List<Item> { pork, mustard, wBread }
                },
                new Product { ProductId = Guid.NewGuid(), Name = "Beef Turner",
                    Image = "hamb3.png", StoreName = morumbiStore,
                    Items = new List<Item> {beef, mustard, bread }
                }
            };
        }


        [HttpGet("{storename}")]
        public IActionResult GetProductsByStoreName(string storeName)
        {
            var productsByStore = products.Where(product =>
                product.StoreName == storeName).ToList();

            if (productsByStore.Count <= 0)
                return NotFound();

            return Ok(productsByStore);

        }
    }
}
