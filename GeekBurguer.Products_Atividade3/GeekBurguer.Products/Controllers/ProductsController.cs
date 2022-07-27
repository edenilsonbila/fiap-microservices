using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GeekBurguer.Products.Repository;
using AutoMapper;
using GeekBurger.Service.Contract.DTO;
using GeekBurguer.Products.Contract.DTO;
using GeekBurguer.Products.Models;

namespace GeekBurguer.Products.Controllers
{
    [Route("api/products")]
    public class ProductsController : Controller
    {
        private IProductsRepository _productsRepository;
        private IMapper _mapper;
        public ProductsController(IProductsRepository productsRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult GetProductsByStoreName([FromQuery] string storeName)
        {
            var productsByStore = _productsRepository.GetProductsByStoreName(storeName).ToList();

            if (productsByStore.Count <= 0)
                return NotFound("Nenhum dado encontrado");

            var productsToGet = _mapper.Map<IEnumerable<ProductToGet>>(productsByStore);

            return Ok(productsToGet);

        }

        [HttpPost()]
        public IActionResult AddProduct([FromBody] ProductToUpsert productToAdd)
        {
            if (productToAdd == null)
                return BadRequest();

            var product = _mapper.Map<Product>(productToAdd);

            if (product.StoreId == Guid.Empty)
                return new
                    Helper.UnprocessableEntityResult(ModelState);

            _productsRepository.Add(product);
            _productsRepository.Save();
            var productToGet = _mapper.Map<ProductToGet>(product);

            return CreatedAtRoute("GetProduct",
                new { id = productToGet.ProductId },
                productToGet);
        }

        [HttpGet("{id}", Name = "GetProduct")]
        public IActionResult GetProduct(Guid id)
        {
            var product = _productsRepository.GetProductById(id);
            var productToGet = _mapper.Map<ProductToGet>(product);

            return Ok(productToGet);
        }



    }
}
