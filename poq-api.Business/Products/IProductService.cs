using System.Threading.Tasks;

namespace poq_api.Business.Products
{
    public interface IProductService
    {
        Task<FilterResult> FilterProducts(FilterQuery query);
    }
}
