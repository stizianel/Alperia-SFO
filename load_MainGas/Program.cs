using CsvHelper;
//using Npgsql;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace load_MainGas
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Load MainGas");
            //   var cs = "Host=localhost;Username=postgres;Password=Bld13@dll;Database=postgres";
            //   using var con = new NpgsqlConnection(cs);
            //   con.Open();

            var ctx = new MainGasContext();

            var readerGas = new StreamReader("c:\\$work\\Temp\\100_20210226_MAIN_GAS.csv");
            var csvGas = new CsvReader(readerGas, CultureInfo.InvariantCulture);
            csvGas.Configuration.Delimiter = ";";
            csvGas.Configuration.IgnoreQuotes = true;

            List<string> badRecord = new List<string>();
            csvGas.Configuration.BadDataFound = null;

            var lGas = ProcessGas(csvGas);
            
            if (badRecord.Count > 0)
            {
                Console.WriteLine($"Errori nel processo file csv {badRecord.Count}");
            }

            //var queryables = from n in lGas.AsQueryable()
            //                 where n.AUSZDAT == "99991231"
            //                 group n by new
            //                 {
            //                     PDR = n.EXT_UI,
            //                     n.AUSZDAT,
            //                     COUNT = n.Count()
            //                 };

            InsMongoMulti(lGas, ctx);

            Console.WriteLine("Fine Load MainGas");
        }
        private static List<MainGas> ProcessGas(CsvReader csvGas)
        {
            try
            {
                var zGas = csvGas.GetRecords<MainGas>().ToList();
                return zGas;
            }
            catch (Exception)
            {
                Console.WriteLine("Errore su file {0}", "MainGas");
                throw;
            }
        }
        private static void InsMongoSingle(MainGas doc, MainGasContext ctx)
        {
            try
            {
                ctx.MainGasCollection.InsertOne(doc);
            }
            catch (Exception)
            {

                Log.Information("errore scrittura");
            }
        }
        private static void InsMongoMulti(List<MainGas> lGas, MainGasContext ctx)
        {
            try
            {
                ctx.MainGasCollection.InsertMany(lGas);
            }
            catch (Exception)
            {

                Log.Information("errore scrittura");
            }
        }
    }
}
