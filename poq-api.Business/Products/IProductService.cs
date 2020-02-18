using System.Collections.Generic;
using System.Threading.Tasks;

namespace poq_api.Business.Products
{
    public interface IProductService
    {
        Task<List<Product>> GetProducts();
    }
}
