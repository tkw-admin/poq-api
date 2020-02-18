using System.Collections.Generic;
using System.Threading.Tasks;

namespace poq_api.Business.Products
{
    public interface IProductService
    {
        Task<FilterResult> FilterProducts(int? maxprice, string size, string highlight);
    }
}
