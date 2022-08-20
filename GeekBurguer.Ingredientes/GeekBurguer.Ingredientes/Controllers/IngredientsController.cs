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
            var response = _service.GetProductsByRestrictions(request).Result;
            if (response != null && response.Any())
                return Ok(response);
            return NotFound("Nenhum produto encontrado.");
            
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _service.GetAll();
            if (response != null && response.Any())
                return Ok(response);
            return NotFound("Nenhum produto encontrado.");

        }
    }
}
