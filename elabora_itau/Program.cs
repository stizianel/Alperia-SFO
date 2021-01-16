using CsvHelper;
using System;
using System.Globalization;
using System.IO;
using Alperia_ISU_Lib;
using System.Linq;
using System.Collections.Generic;

namespace elabora_itau
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var readerFile = new StreamReader("E:\\work\\Alperia\\PRD\\zitau-g1.csv");
            var csvFile = new CsvReader(readerFile, CultureInfo.InvariantCulture);
            csvFile.Configuration.Delimiter = ";";
            List<Zitau> zitau = ProcessZitau(csvFile);
            
            var ltoelab = zitau.Select(p => p.VERTRAG).Distinct().ToList();

            foreach (var item in ltoelab)
            {
                var resl = zitau.Where(x => x.VERTRAG == item).OrderByDescending(x => x.BIS);

                if(resl.Count() == 1)
                {
                    var res = resl.First();
                    var strres = $"{res.VERTRAG} {res.AB} {res.BIS} {res.COD_ESENZIONE} {res.PERC_ESENZ}";
                    Console.WriteLine(strres);
                }
                if (resl.Count() == 2)
                {
                    var res1 = resl.First();
                    var strres = $"{res1.VERTRAG} {res1.AB} {res1.BIS} {res1.COD_ESENZIONE} {res1.PERC_ESENZ}";
                    Console.WriteLine(strres);
                    var res2 = resl.Last();
                    strres = $"{res2.VERTRAG} {res2.AB} {res2.BIS} {res2.COD_ESENZIONE} {res2.PERC_ESENZ}";
                    Console.WriteLine(strres);
                    var vt1 = ValueTuple.Create(res1.COD_ESENZIONE, res2.COD_ESENZIONE);
                   
                }
            }

            Console.WriteLine("End");
        }

        private static List<Zitau> ProcessZitau(CsvReader csvFile)
        {
            try
            {
                var zitau = csvFile.GetRecords<Zitau>().ToList();
                return zitau;
            }
            catch (Exception)
            {
                Console.WriteLine("Errore su file {0}", "Zitau");
                throw;
            }
        }
    }
}
