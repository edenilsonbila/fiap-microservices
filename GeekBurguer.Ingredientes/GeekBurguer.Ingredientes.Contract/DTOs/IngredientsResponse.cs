using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBurguer.Ingredientes.Contract.DTOs
{
    public class IngredientsResponse
    {
        public Guid ProductId { get; set; }
        public List<string> Ingredients { get; set; }
    }
}
