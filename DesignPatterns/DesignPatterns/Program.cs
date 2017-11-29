using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Threading.Tasks;
using DesignPatterns.Object_Pool_Pattern;

namespace DesignPatterns
{
    class Program
    {
        static void Main(string[] args)
        {
            var connection = new PooledSqlConnection();

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            Parallel.For(0, 1000001, (i) => Execute(new SqlCommand("SELECT getdate()")));
            Console.WriteLine($"Using normal connection: {stopWatch.Elapsed}");

            stopWatch.Restart();

            Parallel.For(0, 1000001, (i) => connection.Execute(new SqlCommand("SELECT getdate()")));
            Console.WriteLine($"Using pooled connection: {stopWatch.Elapsed}");

        }

        private static string Execute(IDbCommand cmd)
        {
            var connection = new SqlConnection("Server=.\\SQLEXPRESS;Database=master;Integrated Security=SSPI;App=IDisposablePattern");
            try
            {
                connection.Open();
                cmd.Connection = connection;
                // do some stuff with it this is just an example
                return cmd.ExecuteScalar().ToString();
            }
            finally
            {
                connection.Dispose();
            }
        }
    }
}
