using Microsoft.Data.SqlClient;
using OAS_ClassLib.Interfaces;
using OAS_ClassLib.Models;

namespace OAS_ClassLib.Repositories
{
    public class UserServices : IUserServices
    {
        private DB1 DB1 = new DB1();

        #region Operations
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
        public User GetUserByUsernameAndId(string username,  string password)
        {
            List<User> users = GetUsers();
            return users.SingleOrDefault(u => u.Name == username && u.Password == password);
        }
        public int Validate(string username, string password)
        {
            List<User> users = GetUsers();
            var user = users.SingleOrDefault(u => u.Name == username && u.Password == password);

            return user != null ? 1 : -1; 
        }
       

        public List<User> GetUsers()
        {
            List<User> users = new List<User>();
            DB1.nameValuePairList nvpList = new DB1.nameValuePairList();

            try
            {
                SqlDataReader reader = DB1.GetDataReader(DB1.StoredProcedures.DisplayUsers, nvpList);

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        User user = new User
                        {
                            UserId = Convert.ToInt32(reader["UserId"]),
                            Name = reader["Name"].ToString(),
                            Email = reader["Email"].ToString(),
                            Password = reader["Password"].ToString(),
                            Role = reader["Role"].ToString(),
                            ContactNumber = reader["ContactNumber"].ToString()
                        };

                        users.Add(user);
                    }

                    reader.Close();
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("Error fetching auction details: " + exp.Message);
            }

            return users;
        }

        #endregion
    }
}