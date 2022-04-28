using AdoNet.Env;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet
{
    public class SingletonDBConnection

    {
        private static SingletonDBConnection instance;
        private readonly SqlConnection sqlConnection = new SqlConnection(Connection.connectionString);
        
        private SingletonDBConnection() { }


        public static SingletonDBConnection GetInstance() 
        {
            if (instance==null)
            {
                instance = new SingletonDBConnection();
            }
            return instance;
        }

        public SqlConnection GetConnection()
        {
            try
            {
                sqlConnection.Open();
            }
            catch (Exception ex)
            {
            }
            return sqlConnection;
        }

    }
}
