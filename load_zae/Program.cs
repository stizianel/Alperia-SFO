using Alperia_ISU_Lib;
using CsvHelper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace load_zae
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Inizio caricamento");

            var ctx = new ZaeContext();

            var wreader = new StreamReader("c:\\work\\Alperia\\PRD\\zae400-chiusi-2.txt");
            var wcsv = new CsvReader(wreader, CultureInfo.InvariantCulture);
            wcsv.Configuration.Delimiter = "|";
            wcsv.Configuration.BadDataFound = null;
            wcsv.Configuration.TrimOptions = CsvHelper.Configuration.TrimOptions.Trim;

            var lZae = ProcessZae(wcsv);

            InsMongoMulti(lZae, ctx);

            Console.WriteLine("Fine programma");
        }

        private static List<Zae> ProcessZae(CsvReader wcsv)
        {
            try
            {
                var zrecs = wcsv.GetRecords<Zae>().ToList();
                return zrecs;
            }
            catch (Exception)
            {
                Console.WriteLine("Errore su file {0}", "Zae");
                throw;
            }
        }
        private static void InsMongoMulti(List<Zae> lZae, ZaeContext ctx)
        {
            try
            {
                ctx.ZaeCollection.InsertMany(lZae);
            }
            catch (Exception)
            {

                Log.Information("errore scrittura");
            }
        }
    }
}
