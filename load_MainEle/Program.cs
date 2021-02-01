using CsvHelper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace load_MainEle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var ctx = new MainEleContext();

            var readerEle = new StreamReader("e:\\work\\Alperia\\PRD\\100_20210131_MAIN_ELE.csv");
            var csvEle = new CsvReader(readerEle, CultureInfo.InvariantCulture);
            csvEle.Configuration.Delimiter = ";";
            csvEle.Configuration.BadDataFound = null;

            var lEle = ProcessEle(csvEle);

            InsMongoMulti(lEle, ctx);

            Console.WriteLine("Fine programma");
        }
        private static List<MainEle> ProcessEle(CsvReader csvEle)
        {
            try
            {
                var zGas = csvEle.GetRecords<MainEle>().ToList();
                return zGas;
            }
            catch (Exception)
            {
                Console.WriteLine("Errore su file {0}", "MainGas");
                throw;
            }
        }
        private static void InsMongoMulti(List<MainEle> lEle, MainEleContext ctx)
        {
            try
            {
                ctx.MainEleCollection.InsertMany(lEle);
            }
            catch (Exception)
            {

                Log.Information("errore scrittura");
            }
        }
    }
}
