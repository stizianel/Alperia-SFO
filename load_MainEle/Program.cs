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
            Console.WriteLine("Load Main Ele");
            var ctx = new MainEleContext();

            var readerEle = new StreamReader("c:\\$work\\alperia\\prd\\100_20210412_MAIN_ELE.csv");
            var csvEle = new CsvReader(readerEle, CultureInfo.InvariantCulture);
            List<string> badRecord = new List<string>();

            csvEle.Configuration.Delimiter = ";";
            csvEle.Configuration.IgnoreQuotes = true;
            //csvEle.Configuration.BadDataFound = null;
            csvEle.Configuration.BadDataFound = context => badRecord.Add(context.RawRecord);

            var lEle = ProcessEle(csvEle);

            if(badRecord.Count > 0)
            {
                Console.WriteLine($"Errori nel processo file csv {badRecord.Count}");
            }

            List<MainEle> depLele = new List<MainEle>();

            var count = 0;
            foreach (var item in lEle)
            {
                count++;
                depLele.Add(item);
                if(count == 10000)
                {
                    Console.WriteLine("Scrivo blocco 10000");
                    InsMongoMulti(depLele, ctx);
                    depLele = new List<MainEle>();
                    count = 0;
                }
            }

            InsMongoMulti(depLele, ctx);

            Console.WriteLine("Fine Load Main Ele");
        }
        private static List<MainEle> ProcessEle(CsvReader csvEle)
        {
            try
            {
                var zEle = csvEle.GetRecords<MainEle>().ToList();
                return zEle;
            }
            catch (Exception)
            {
                Console.WriteLine("Errore su file {0}", "MainEle");
                throw;
            }
        }
        private static void InsMongoMulti(List<MainEle> lEle, MainEleContext ctx)
        {
            try
            {
                ctx.MainEleCollection.InsertMany(lEle);
            }
            catch (Exception e)
            {

                Console.WriteLine($"errore scrittura first {e.Message}");
            }
        }
    }
}
