using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBurguer.Ingredientes.Contract.DTOs
{
    public class IngredientesRequest
    {
        public string StoreName { get; set; }
        public List<string> Restrictions { get; set; }
    }
}
