using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Diagnostics;

namespace ItemsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //string connectionString = "Server=.;Database=ITEMSDB;Trusted_Connection=True;";
            ////using statement makes it so that disposal doesn't get messed up
            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    var sql = "select * from [dbo].ItemsTable";
            //    var cmd = new SqlCommand(sql, connection);
            //    connection.Open();

            //    //an iterable list that doesn't need to be enumerated
            //    //like a foreach loop
            //    //points to rows
            //    var reader = cmd.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        Debug.Write((string)reader["Name"]);
            //        Debug.Write((string)reader["description"]);
            //    }

            //    connection.Close();
            //}
            CreateHostBuilder(args).Build().Run();

        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
