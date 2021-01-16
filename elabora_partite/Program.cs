using Alperia_ISU_Lib;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace elabora_partite
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Elabora partite");
            var readerDfkkop = new StreamReader("c:\\work\\Alperia\\PRD\\aperte100.csv");
            var csvDfkkop = new CsvReader(readerDfkkop, CultureInfo.InvariantCulture);
            csvDfkkop.Configuration.Delimiter = "|";
            csvDfkkop.Configuration.BadDataFound = null;
            csvDfkkop.Configuration.TrimOptions = CsvHelper.Configuration.TrimOptions.Trim;

            var lPartite = ProcessPartite(csvDfkkop);

            Console.WriteLine("Fine programma");
        }
        private static List<Dfkkop> ProcessPartite(CsvReader csvDfkkop)
        {
            try
            {
                var zGas = csvDfkkop.GetRecords<Dfkkop>().ToList();
                return zGas;
            }
            catch (Exception)
            {
                Console.WriteLine("Errore su file {0}", "MainGas");
                throw;
            }
        }
    }
    
}
