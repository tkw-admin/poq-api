using System;
using System.Collections.Generic;
using System.Text;

namespace poq_api.Business
{
    public interface IUserService
    {
        User Authenticate(string username, string password);
        IEnumerable<User> GetAll();
    }
}
