using System.Threading.Tasks;

namespace poq_api.Business
{
    public interface IProductService
    {
        Task<FilterResult> FilterProducts(FilterQueryRequest query);
    }
}
