using AdoNet.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet.Manager
{
    public class OrderManager
    {
        public SingletonDBConnection singletonDBConnection;
        public OrderManager()
        {
            singletonDBConnection = SingletonDBConnection.GetInstance();
        }

        public List<Order> GetOrders()
        {
            var sqlConnection = singletonDBConnection.GetConnection();
            SqlCommand cmd = new SqlCommand("select * from Orders", sqlConnection);
            var reader = cmd.ExecuteReader();
            List<Order> orders = new List<Order>();
    
            sqlConnection.Close();
            return orders;
        }

        public List<Order> GetOrders(string query)
        {
            SqlConnection sqlConnection = singletonDBConnection.GetConnection();

            SqlCommand command = new SqlCommand(query, sqlConnection);

            var reader = command.ExecuteReader();

            List<Order> order = MapSqlDataToOrders(reader);


            sqlConnection.Close();

            return order;
        }
        //public List<Order> GetGermany()
        //{
        //    var sqlConnection = singletonDBConnection.GetConnection();
        //    SqlCommand cmd = new SqlCommand("select * from Orders Where ShipCountry= Germany", sqlConnection);
        //    var reader = cmd.ExecuteReader();
        //    List<Order> orders1 = new List<Order>();
        //    orders1.GetOrders();

        //    orders1.Add(order11);

        //}
        //sqlConnection.Close();

        //---------------------------------------------------------------------------------------------------------------

        //Bu metot sql sonucunu alıp bana orderlistesi verir. 
        // MAplemek : başka bir designi eşleştirmek
        private List<Order> MapSqlDataToOrders(SqlDataReader reader)
        {
            List<Order> orders = new List<Order>();
            while (reader.Read())
            {
                Order order = new Order();
                order.OrderId = Convert.ToInt32(reader["OrderId"]);
                order.EmployeeId = Convert.ToInt32(reader["EmployeeId"]);
                order.OrderDate = Convert.ToDateTime(reader["OrderDate"]);
                order.RequiredDate = Convert.ToDateTime(reader["RequiredDate"]);
                order.Freight = Convert.ToDecimal(reader["Freight"]);
                order.ShipCity = reader["ShipCity"].ToString();
                order.ShipCountry = reader["ShipCountry"].ToString();

                orders.Add(order);
            }
            return orders;
        }
    }
}
