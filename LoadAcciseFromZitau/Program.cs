using Alperia_ISU_Lib;
using CsvHelper;
using CsvHelper.Configuration;
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
            Console.WriteLine("Hello World!");
            var ctx = new Zs4hAcciseContext();
            var reader = new StreamReader("c:\\$work\\Temp\\zitau.csv");
            var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
            csv.Configuration.Delimiter = "|";
            csv.Configuration.IgnoreQuotes = true;
            csv.Configuration.TrimOptions = TrimOptions.Trim;

            var lRec = ProcessFile(csv);
            Console.WriteLine("Fine programma");
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
    }
}
