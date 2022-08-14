﻿using GeekBurguer.Ingredientes.Model;
using Microsoft.EntityFrameworkCore;

namespace GeekBurguer.Ingredientes.Repository.Context
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
        {

        }

        public DbSet<ProductIngredients> ProductIgredients { get; set; }
    }
}
