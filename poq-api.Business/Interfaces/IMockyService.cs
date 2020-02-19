using System.Threading.Tasks;

namespace poq_api.Business
{
    public interface IMockyService
    {
        Task<MockyResponse> GetProducts();
    }
}
