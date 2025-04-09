using OAS_ClassLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS_ClassLib.Interfaces
{
    public interface IUserServices
    {
        bool AddUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(int userId);
        List<User> GetUsers();
        // Add these methods to match the controller requirements
        int Validate(string username, string password);
        User GetUserByUsernameAndId(string username, string password);
    }
}

