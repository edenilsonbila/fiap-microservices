using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBurger.Service.Contract.DTO
{
    public class ItemToGet
    {
        public Guid ItemId { get; set; }
        public string Name { get; set; }

    }
}
