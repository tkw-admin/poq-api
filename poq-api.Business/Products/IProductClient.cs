using RestEase;
using System.Threading.Tasks;

namespace poq_api.Business.Products
{
    public interface IProductClient
    {
        //[Header("Authorization")]
        //AuthenticationHeaderValue Authorization { get; set; }

        [Get("http://www.mocky.io/v2/5e307edf3200005d00858b49")]
        Task<ProductResult> GetProducts();
    }
}
