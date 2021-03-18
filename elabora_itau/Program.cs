using CsvHelper;
using System;
using System.Globalization;
using System.IO;
using Alperia_ISU_Lib;
using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;

namespace elabora_itau
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Elabora Itau");
            var csx = new Zs4hAcciseContext();
            var writer1 = new StreamWriter("c:\\$work\\temp\\zitau1.csv");
            var csv1 =   new CsvWriter(writer1, CultureInfo.InvariantCulture);
            csv1.Configuration.Delimiter = ";";

            var lOutf1 = new List<Zs4hAccise>();

            List<Zs4hAccise> zitau = csx.AcciseCollection.AsQueryable().ToList();
            
            var ltoelab = zitau.Where(x => x.SPARTE == "E8").Select(p => p.VERTRAG).Distinct().ToList();

            foreach (var item in ltoelab)
            {
                var resl = zitau.Where(x => x.VERTRAG == item).OrderByDescending(x => x.BIS).ToList<Zs4hAccise>();

                if(resl.Count() == 1)
                {
                    var res = resl.First();
                    var strres = $"{res.VERTRAG} {res.AB} {res.BIS} {res.COD_ESENZIONE} {res.PERC_ESENZ}";
                    lOutf1.Add(res);

                    //Console.WriteLine(strres);
                }
                if (resl.Count() == 2)
                {
                    Console.WriteLine("Contratto con 2 timeslice");
                    var res1 = resl.First();
                    var strres = $"{res1.VERTRAG} {res1.AB} {res1.BIS} {res1.COD_ESENZIONE} {res1.PERC_ESENZ}";
                    Console.WriteLine(strres);
                    var res2 = resl.Last();
                    strres = $"{res2.VERTRAG} {res2.AB} {res2.BIS} {res2.COD_ESENZIONE} {res2.PERC_ESENZ}";
                    Console.WriteLine(strres);
                    //var vt1 = ValueTuple.Create(res1.COD_ESENZIONE, res2.COD_ESENZIONE);shra
                }
            }

            csv1.WriteRecords<Zs4hAccise>(lOutf1);
            writer1.Close();
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
