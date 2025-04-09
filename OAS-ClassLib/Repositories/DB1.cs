using Microsoft.Data.SqlClient;
using OAS_ClassLib.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OAS_ClassLib.Repositories
{
    public class DB1
    {
        private const string ConnectionString = "Data Source=LTIN593499;Initial Catalog=OAS;persist security info=True;Integrated Security=SSPI;Encrypt=False";
        #region Helper Methods

        private SqlParameter CreateSqlParameter(string name, object value)
        {
            return new SqlParameter(name, value ?? DBNull.Value);
        }

        #endregion

        #region Insert, Update, Delete Methods

        public int Insert(StoredProcedures sp, nameValuePairList parameters)
        {
            return ExecuteNonQuery(sp, parameters);
        }

        public int Update(StoredProcedures sp, nameValuePairList parameters)
        {
            return ExecuteNonQuery(sp, parameters);
        }

        public int Delete(StoredProcedures sp, nameValuePairList parameters)
        {
            return ExecuteNonQuery(sp, parameters);
        }

        #endregion


        #region Execute Methods

        public object ExecuteScalar(StoredProcedures sp, nameValuePairList parameters)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sp.ToString(), connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        foreach (var param in parameters)
                        {
                            cmd.Parameters.Add(CreateSqlParameter(param.ColName, param.Value));
                        }

                        connection.Open();
                        return cmd.ExecuteScalar();
                    }
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("Error: " + exp.Message);
                return null;
            }
        }

        private int ExecuteNonQuery(StoredProcedures sp, nameValuePairList parameters)
        
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(ConnectionString))
                {
                    using (SqlCommand cmd = new SqlCommand(sp.ToString(), connection))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        foreach (var param in parameters)
                        {
                            cmd.Parameters.Add(CreateSqlParameter(param.ColName, param.Value));
                        }

                        connection.Open();
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception exp)
            {
                Console.WriteLine("Error: " + exp.Message);
                return -1;
            }
        }

       

        public SqlDataReader GetDataReader(StoredProcedures DisplayUsers, nameValuePairList parameters)
        {
            try
            {
                SqlConnection connection = new SqlConnection(ConnectionString);
                SqlCommand cmdObject = new SqlCommand(DisplayUsers.ToString(), connection);

                cmdObject.CommandType = CommandType.StoredProcedure;

                foreach (var param in parameters)
                {
                    cmdObject.Parameters.Add(CreateSqlParameter(param.ColName, param.Value));
                }

                connection.Open();
                return cmdObject.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception exp)
            {
                Console.WriteLine("Error: " + exp.Message);
                return null;
            }
        }

        #endregion

        #region Nested Classes

        public class nameValuePairList : List<nameValuePair>
        {
        }

        public class nameValuePair
        {
            public string ColName { get; set; }
            public object Value { get; set; }

            public nameValuePair(string name, object value)
            {
                ColName = name;
                Value = value;
            }
        }

        #endregion

        #region Enums

        public enum StoredProcedures
        {
            InsertUser,
            UpdateUser,
            DeleteUser,
            DisplayUsers,
            InsertAuction,
            UpdateAuction,
            DeleteAuction,
            GetAllAuctions,
            CheckUserID
        }

        #endregion
    }
}
