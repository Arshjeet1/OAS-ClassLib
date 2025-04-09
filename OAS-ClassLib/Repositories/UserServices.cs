using Microsoft.Data.SqlClient;
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

        //public bool UserIDExists(int userId)
        //{
        //    try
        //    {
        //        var parameters = new DB1.nameValuePairList
        //        {
        //            new DB1.nameValuePair("@UserID", userId)
        //        };

        //        object result = DB1.ExecuteScalar(DB1.StoredProcedures.CheckUserID, parameters);

        //        return result != null;
        //    }
        //    catch (Exception exp)
        //    {
        //        Console.WriteLine($"Error checking UserID: {exp.Message}");
        //        return false;
        //    }
        //}

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
