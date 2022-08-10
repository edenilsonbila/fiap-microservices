using GeekBurguer.Ingredientes.Contract.DTOs;
using GeekBurguer.Ingredientes.Services;
using Microsoft.AspNetCore.Mvc;

namespace GeekBurguer.Ingredientes.Controllers
{
    [Route("api/products")]
    public class IngredientsController : Controller
    {
        private readonly IngredientsService _service;
        public IngredientsController(IngredientsService service)
        {
            _service = service;
        }       
        [HttpGet]
        [Route("byrestrictions")]
        public IActionResult GetProductsByRestrictions([FromQuery] IngredientsRequest request)
        {
            /*
            var inge = new IngredientsResponse();
            inge.ProductId = new Guid();
            return Ok(new List<IngredientsResponse>() { 
                new IngredientsResponse() { ProductId = new Guid(), Ingredients = new List<string> { "soy"} 
                } 
            });
            */
            
            var response = _service.GetProductsByRestrictions(request).Result;
            if (response != null)
                return Ok(response);
            return NotFound("Nenhum produto encontrado.");
            
        }
    }
}
