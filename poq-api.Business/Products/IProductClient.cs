using RestEase;
using System.Threading.Tasks;

namespace poq_api.Business.Products
{
    public interface IProductClient
    {
        //[Header("Authorization")]
        //AuthenticationHeaderValue Authorization { get; set; }

        [Get]
        Task<ProductResult> GetProducts();
    }
}
