using AdoNet;
using AdoNet.Manager;
using System;
using System.Data.SqlClient;

namespace MyApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
            OrderManager orderManager = new OrderManager();


            var orders = orderManager.GetOrders();
            var orders1 = orderManager.GetOrders("select * from Orders where ShipCountry = 'Germany'");

            Console.WriteLine(orders1);





            ProductManager productManager = new ProductManager();
            //1) Dışarıdan decimal minimum ve maximum price alan ve onlara uygun ürünleri bana dönen metot.
            Console.WriteLine("max değer ?");
            decimal max =Convert.ToDecimal(Console.ReadLine());
            Console.WriteLine("min değer ?");
            decimal min =Convert.ToDecimal(Console.ReadLine());

            var between = productManager.GetMaxMin(max, min);
            //2) Stokta olmayan(stok sayısı 0) olan ürünleri bana dönen metot.
            Console.WriteLine(productManager.StockNull);
            //3) Dışarıdan name alan ve aldığı name değerindeki ürünleri arayıp bana dönen metot.
            Console.WriteLine("aradığınız ürünü yazınız===>>>");
            string urun =Console.ReadLine();
            Console.WriteLine(productManager.GetSearch(urun));

            //4) Ürünlerin ortalama fiyatını bana veren metot.
            Console.WriteLine( productManager.GetAVG);
            //5) Dışarıdan CategoryId alan ve o categoryId e ait ürünlerin ortalama fiyatını bana dönen metot.
            Console.WriteLine("Category Id ===>>");
            int id=Convert.ToInt32(Console.ReadLine());
            Console.WriteLine(productManager.GetCategoryID(id));
            //string connectionString = "Server=94.73.145.4;Database=u9751868_db9EC;User Id=u9751868_user9EC;Password=PWtw68S5BRcg01S;";
            //SqlConnection sqlConnection = new SqlConnection(connectionString);

            //SqlCommand cmd = new SqlCommand("select * from Products", sqlConnection); // Vt ismini yazıp burada çalışacağımızı söyleriz

            //sqlConnection.Open();


            //var result = cmd.ExecuteReader();

            //while (result.Read())
            //{
            //    Console.WriteLine(result["ProductName"]);

            //}
            //Console.Read();

            //sqlConnection.Close();

        }
    }
}