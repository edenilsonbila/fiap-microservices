using AutoMapper;
using GeekBurguer.Products.Contract.DTO;
using GeekBurguer.Products.Models;
using GeekBurguer.Products.Repository;
using System;
using System.Linq;

namespace GeekBurguer.Products.Helper
{
    public class MatchItemsFromRepository : IMappingAction<ItemToUpsert, Item>
    {
        private IProductsRepository _productRepository;
        public MatchItemsFromRepository(IProductsRepository
            productRepository)
        {
            _productRepository = productRepository;
        }

        public void Process(ItemToUpsert source, Item destination, ResolutionContext context)
        {
            var fullListOfItems =
                _productRepository.GetFullListOfItems();

            var itemFound = fullListOfItems?
                .FirstOrDefault(item => item.Name
                .Equals(source.Name,
                    StringComparison.InvariantCultureIgnoreCase));

            if (itemFound != null)
                destination.ItemId = itemFound.ItemId;
            else
                destination.ItemId = Guid.NewGuid();
        }
    }
}
