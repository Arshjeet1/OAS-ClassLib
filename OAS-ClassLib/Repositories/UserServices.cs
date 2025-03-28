using OAS_ClassLib.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS_ClassLib.Repositories
{
    public class UserServices
    {
        private DB1 DB1 = new DB1();

        #region Operations

        public bool UserIDExists(int userId)
        {
            try
            {
                var parameters = new DB1.nameValuePairList
                {
                    new DB1.nameValuePair("@UserID", userId)
                };

                object result = DB1.ExecuteScalar(DB1.StoredProcedures.CheckUserID, parameters);

                return result != null;
            }
            catch (Exception exp)
            {
                Console.WriteLine($"Error checking UserID: {exp.Message}");
                return false;
            }
        }

        public bool AddUser(User user)
        {
            try
            {
                var parameters = new DB1.nameValuePairList
                {
                    new DB1.nameValuePair("@UserName", user.Name),
                    new DB1.nameValuePair("@UserEmail", user.Email),
                    new DB1.nameValuePair("@UserPassword", user.Password),
                    new DB1.nameValuePair("@UserRole", user.Role),
                    new DB1.nameValuePair("@UserContactNumber", user.ContactNumber)
                };

                int result = DB1.Insert(DB1.StoredProcedures.InsertUser, parameters);
                return result > 0;
            }
            catch (Exception exp)
            {
                Console.WriteLine($"Error adding user: {exp.Message}");
                return false;
            }
        }

        public bool UpdateUser(User user)
        {
            try
            {
                var parameters = new DB1.nameValuePairList
                {
                    new DB1.nameValuePair("@UserId", user.UserId),
                    new DB1.nameValuePair("@UserName", user.Name),
                    new DB1.nameValuePair("@UserEmail", user.Email),
                    new DB1.nameValuePair("@UserPassword", user.Password),
                    new DB1.nameValuePair("@UserRole", user.Role),
                    new DB1.nameValuePair("@UserContactNumber", user.ContactNumber)
                };

                int result = DB1.Update(DB1.StoredProcedures.UpdateUser, parameters);
                return result > 0;
            }
            catch (Exception exp)
            {
                Console.WriteLine($"Error updating user: {exp.Message}");
                return false;
            }
        }

        public bool DeleteUser(int userId)
        {
            try
            {
                var parameters = new DB1.nameValuePairList
                {
                    new DB1.nameValuePair("@UserId", userId)
                };

                int result = DB1.Delete(DB1.StoredProcedures.DeleteUser, parameters);
                return result > 0;
            }
            catch (Exception exp)
            {
                Console.WriteLine($"Error deleting user: {exp.Message}");
                return false;
            }
        }

        public List<User> GetUsers()
        {
            try
            {
                var parameters = new DB1.nameValuePairList();
                return DB1.DisplayUsers(DB1.StoredProcedures.DisplayUsers, parameters);
            }
            catch (Exception exp)
            {
                Console.WriteLine($"Error fetching users: {exp.Message}");
                return new List<User>();
            }
        }

        #endregion
    }
}
