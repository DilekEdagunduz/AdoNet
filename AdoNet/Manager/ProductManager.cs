using AdoNet.Env;
using AdoNet.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdoNet.Manager
{
    public class ProductManager
    {
        //1) Dışarıdan decimal minimum ve maximum price alan ve onlara uygun ürünleri bana dönen metot.

        //2) Stokta olmayan(stok sayısı 0) olan ürünleri bana dönen metot.

        //3) Dışarıdan name alan ve aldığı name değerindeki ürünleri arayıp bana dönen metot.

        //4) Ürünlerin ortalama fiyatını bana veren metot.

        //5) Dışarıdan CategoryId alan ve o categoryId e ait ürünlerin ortalama fiyatını bana dönen metot.

        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            using (SqlConnection sqlConnection = new SqlConnection(Connection.connectionString))
            {

                try
                {
                    sqlConnection.Open();


                    SqlCommand cmd = new SqlCommand("select * from Products", sqlConnection);

                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Product product = new Product();

                        product.Id = Convert.ToInt32(reader["ProductId"]);
                        product.Name = reader["ProductName"].ToString();
                        product.UnitPrice = Convert.ToDecimal(reader["UnitPrice"]);
                        product.UnitsInStock = Convert.ToInt32(reader["UnitsInStock"]);

                        products.Add(product);

                    }


                    sqlConnection.Close();

                    return products;
                }
                catch (Exception ex)
                {
                    return products;
                }

            }
        }

        public Product GetProductById(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Connection.connectionString))
            {
                try
                {
                    sqlConnection.Open();
                    string queryString = "select * from Products where ProductID = @productId";

                    SqlCommand sqlCommand = new SqlCommand(queryString, sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@productId", id);

                    var reader = sqlCommand.ExecuteReader();

                    Product product = new Product();

                    while (reader.Read())
                    {

                        product.Id = Convert.ToInt32(reader["ProductID"]);
                        product.Name = reader["ProductName"].ToString();
                        product.UnitPrice = Convert.ToDecimal(reader["UnitPrice"]);
                        product.UnitsInStock = Convert.ToInt32(reader["UnitsInStock"]);
                    }

                    sqlConnection.Close();
                    return product;
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
        }
        public void AddProduct(Product product)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Connection.connectionString))
            {
                sqlConnection.Open();

                SqlCommand sqlCommand = new SqlCommand("insert into Products(ProductName, UnitPrice, UnitsInStock) values(@name, @price, @stock)", sqlConnection);

                sqlCommand.Parameters.AddWithValue("@name", product.Name);
                sqlCommand.Parameters.AddWithValue("@price", product.UnitPrice);
                sqlCommand.Parameters.AddWithValue("@stock", product.UnitsInStock);


                sqlCommand.ExecuteNonQuery();


                sqlConnection.Close();
            }
        }
        public void DeleteProduct(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(Connection.connectionString))
            {
                try
                {
                    sqlConnection.Open();

                    var command = new SqlCommand("delete from Products where ProductID = @productId", sqlConnection);

                    command.Parameters.AddWithValue("@productId", id);

                    command.ExecuteNonQuery();


                    sqlConnection.Close();
                }
                catch (Exception ex)
                {
                    throw;
                }

            }
        }
        //DB de kaç adet ürün olduğunu bana veren metot
        public int GetProductsCount()
        {

            int count = 0;

            using (SqlConnection sqlConnection = new SqlConnection(Connection.connectionString))
            {
                sqlConnection.Open();

                SqlCommand command = new SqlCommand("select Count(*) from Products", sqlConnection);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    count = Convert.ToInt32(reader[0]);
                }

                sqlConnection.Close();
            }

            return count;
        }
        //1) Dışarıdan decimal minimum ve maximum price alan ve onlara uygun ürünleri bana dönen metot.
        public List<Product>  GetMaxMin(decimal max,decimal min)
        {
            List<Product> products = new List<Product>();   
            using (SqlConnection sqlConnection = new SqlConnection(Connection.connectionString))
            {
                sqlConnection.Open();
               
                SqlCommand command = new SqlCommand("select * from Products where UnitPrice between @min and @max ", sqlConnection);
                command.Parameters.AddWithValue("@max", max);

                command.Parameters.AddWithValue("@min", min);
                var reader = command.ExecuteReader();
               
                while (reader.Read())
                {
                    Product product = new Product();
                    product.Id = Convert.ToInt32(reader["ProductId"]);
                    product.Name = reader["ProductName"].ToString();
                    product.UnitPrice = Convert.ToDecimal(reader["UnitPrice"]);
                    product.UnitsInStock = Convert.ToInt32(reader["UnitsInStock"]);

                    products.Add(product);
                }

                sqlConnection.Close();
                return products;

            }
        }

        //2) Stokta olmayan(stok sayısı 0) olan ürünleri bana dönen metot.
        public List<Product> StockNull()
        {
            List<Product> products = new List<Product>();
            using (SqlConnection sqlConnection = new SqlConnection(Connection.connectionString))
            {
                sqlConnection.Open();

                SqlCommand command = new SqlCommand("select * from Products where UnitsInStock=0 ", sqlConnection);
              
                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product();

                    product.Id = Convert.ToInt32(reader["ProductId"]);
                    product.Name = reader["ProductName"].ToString();
                    product.UnitPrice = Convert.ToDecimal(reader["UnitPrice"]);
                    product.UnitsInStock = Convert.ToInt32(reader["UnitsInStock"]);

                    products.Add(product);

                }

                sqlConnection.Close();
                return products;

            }
        }

        //3) Dışarıdan name alan ve aldığı name değerindeki ürünleri arayıp bana dönen metot.

        public List<Product> GetSearch(string name)
        {
            List<Product> products = new List<Product>();
            using (SqlConnection sqlConnection = new SqlConnection(Connection.connectionString))
            {
                sqlConnection.Open();

                SqlCommand command = new SqlCommand("select * from Products where ProductName like '%'+@name+'%' ", sqlConnection);
                command.Parameters.AddWithValue("@name", name);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product();

                    product.Id = Convert.ToInt32(reader["ProductId"]);
                    product.Name = reader["ProductName"].ToString();
                    product.UnitPrice = Convert.ToDecimal(reader["UnitPrice"]);
                    product.UnitsInStock = Convert.ToInt32(reader["UnitsInStock"]);

                    products.Add(product);

                }

                sqlConnection.Close();
                return products;

            }

        }
        //4) Ürünlerin ortalama fiyatını bana veren metot.
        public double GetAVG(string name)
        {
            List<Product> products = new List<Product>();
            using (SqlConnection sqlConnection = new SqlConnection(Connection.connectionString))
            {
                sqlConnection.Open();

                SqlCommand command = new SqlCommand("select AVG(UnitPrice) from Products ", sqlConnection);
                

                var reader = command.ExecuteReader();
                double result = 0;
                while (reader.Read())
                {
                    result= Convert.ToInt32(reader[0]);
                }

                sqlConnection.Close();
                return result;

            }

        }

        //5) Dışarıdan CategoryId alan ve o categoryId e ait ürünlerin ortalama fiyatını bana dönen metot.

        public double GetCategoryID (int id)
        {
            double result = 0;  
            using (SqlConnection sqlConnection = new SqlConnection(Connection.connectionString))
            {
                sqlConnection.Open();

                SqlCommand command = new SqlCommand("select AVG(UnitPrice) from Products where CategoryID = @id", sqlConnection);

                command.Parameters.AddWithValue("@id", id);

                var reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result = Convert.ToInt32(reader[0]);
                }

                sqlConnection.Close();
            }
            return result;
        }

    }
}
    