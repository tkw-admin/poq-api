using System.Collections.Generic;

namespace poq_api.Business.Security
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
    }
}
