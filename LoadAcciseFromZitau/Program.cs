using Alperia_ISU_Lib;
using CsvHelper;
using CsvHelper.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace LoadAcciseFromZitau
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Load Zs4hAccise!");
            var ctx = new Zs4hAcciseContext();
            var reader = new StreamReader("c:\\$work\\Temp\\zitau.csv");
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Configuration.Delimiter = "|";
            csv.Configuration.IgnoreQuotes = true;
            csv.Configuration.TrimOptions = TrimOptions.Trim;

            var lRec = ProcessFile(csv);

            InsMongoMulti(lRec, ctx);

            Console.WriteLine("Fine Load Zs4hAccise");
        }

        private static List<Zs4hAccise> ProcessFile(CsvReader csv)
        {
            try
            {
                var zRecs = csv.GetRecords<Zs4hAccise>().ToList();
                return zRecs;
            }
            catch (Exception)
            {
                Console.WriteLine("Errore su file {0}", "Zitau");
                throw;
            }
        }
        private static void InsMongoSingle(Zs4hAccise doc, Zs4hAcciseContext ctx)
        {
            try
            {
                ctx.AcciseCollection.InsertOne(doc);
            }
            catch (Exception)
            {

                Log.Information("errore scrittura");
            }
        }
        private static void InsMongoMulti(List<Zs4hAccise> ldoc, Zs4hAcciseContext ctx)
        {
            try
            {
                ctx.AcciseCollection.InsertMany(ldoc);
            }
            catch (Exception)
            {

                Log.Information("errore scrittura");
            }
        }
    }
}
