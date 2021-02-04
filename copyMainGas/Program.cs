using System;
//using Npgsql;
using System.Text.Json;

namespace copyMainGas
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            //using var con = new NpgsqlConnection("User ID=postgres;Password=Bld13@dll;Host=localhost;Port=5432;Database=Alperia;");

            //var cmd = new NpgsqlCommand();

            var ist = new MainGas();
            var res = ist.getJsonSchema();
            var json = JsonSerializer.Serialize(res);

            Console.WriteLine("Fine Programma");


        }
    }
}
