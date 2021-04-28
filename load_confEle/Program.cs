using Alperia_ISU_Lib;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace load_confEle
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Load ConfEle start");
            var ctx = new ConfEleContext();
            var readerFile = new StreamReader("c:\\$work\\Alperia\\PRD\\100_20210412_CONFCOMM_ELE.csv");
            var csv = new CsvReader(readerFile, CultureInfo.InvariantCulture);
            csv.Configuration.Delimiter = ";";
            csv.Configuration.IgnoreQuotes = true;

            var lrecs = ProcessConfEle(csv);
            InsMongoMulti(lrecs, ctx);

            Console.WriteLine("Load ConfEle end");
        }

        private static List<ConfEle> ProcessConfEle(CsvReader csv)
        {
            try
            {
                var zrecs = csv.GetRecords<ConfEle>().ToList();
                return zrecs;
            }
            catch (Exception)
            {
                Console.WriteLine("Errore su file {0}", "ConfCommEle");
                throw;
            }
        }
        private static void InsMongoMulti(List<ConfEle> lConfEle, ConfEleContext ctx)
        {
            try
            {
                ctx.ConfEleCollection.InsertMany(lConfEle);
            }
            catch (Exception)
            {

                //Log.Information("errore scrittura");
            }
        }
    }
}
