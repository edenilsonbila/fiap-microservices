using GeekBurguer.Products.Models;

namespace GeekBurguer.Products.Repository
{
    public interface IStoreRepository
    {
        Store GetStoreByName(string storeName);
    }
}
