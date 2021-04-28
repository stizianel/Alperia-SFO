using Alperia_ISU_Lib;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace BonusMockUp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var readerXLS = new StreamReader("c:\\$work\\alperia\\BonusMock.csv");
            var csvXls = new CsvReader(readerXLS, CultureInfo.InvariantCulture);
            csvXls.Configuration.Delimiter = ";";
            csvXls.Configuration.IgnoreQuotes = true;
            csvXls.Configuration.HasHeaderRecord = true;

            var fobonus_ee = new StreamWriter("c:\\$work\\alperia\\prd\\BonusEE.csv");
            var csvEle = new CsvWriter(fobonus_ee, CultureInfo.InvariantCulture);
            csvEle.Configuration.Delimiter = ";";

            var fobonus_gn = new StreamWriter("c:\\$work\\alperia\\prd\\BonusGN.csv");
            var csvGas = new CsvWriter(fobonus_gn, CultureInfo.InvariantCulture);
            csvGas.Configuration.Delimiter = ";";

            var lXls = ProcessXls(csvXls);

            var loutEE = lXls.Where(p => p.COD_POD[0] == 'I').ToList();
            var lfiltGN = lXls.Where(p => p.COD_POD[0] != 'I').ToList();

            var loutGN = new List<GasBonus>();
            foreach (var item in lfiltGN)
            {
                loutGN.Add(item);
            }

            csvEle.WriteRecords(loutEE);
            csvGas.WriteRecords(loutGN);

            fobonus_ee.Close();
            fobonus_gn.Close();

            Console.WriteLine("Fine Programma");
        }

        private static List<XlsBonus> ProcessXls(CsvReader csvXls)
        {
            try
            {
                var zXls = csvXls.GetRecords<XlsBonus>().ToList();
                return zXls;
            }
            catch (Exception)
            {
                Console.WriteLine("Errore su file {0}", "XlsBonus");
                throw;
            }
        }
        
    }
}
