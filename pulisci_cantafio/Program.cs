using Alperia_ISU_Lib;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace pulisci_cantafio
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var readerFica = new StreamReader("C:\\work\\Alperia\\PRD\\100_20210116_1_DOCFICA.csv");
            var csvFica = new CsvReader(readerFica, CultureInfo.InvariantCulture);
            csvFica.Configuration.Delimiter = ";";
            //csvFica.Configuration.HeaderValidated = true;
            //csvFica.Configuration.MissingFieldFound = null;

            var writerFica = new StreamWriter("C:\\work\\Alperia\\PRD\\A100_20210116_1_DOCFICA.csv");
            var outFica = new CsvWriter(writerFica, CultureInfo.InvariantCulture);
            outFica.Configuration.Delimiter = ";";
            outFica.Configuration.HasHeaderRecord = false;

            var lfica = LoadFica(csvFica);

            List<Docfica> loutFica = new List<Docfica>();

            foreach (var fica in lfica)
            {
                var depFica = new Docfica();
                var dtLimit = DateTime.ParseExact("20201130",
                                  "yyyyMMdd",
                                   CultureInfo.InvariantCulture);
                var wBldat = DateTime.ParseExact(fica.BLDAT,
                                  "yyyyMMdd",
                                   CultureInfo.InvariantCulture);

                if (fica.BLART == "RA" || fica.AUGST == "9" || wBldat > dtLimit)
                {
                    Console.WriteLine("Scartato {0} - {1}", fica.BLART, fica.AUGST );
                    continue;
                }
                depFica = fica;
                depFica.ADD_REFOBJ = "";
                loutFica.Add(depFica);
            }
            outFica.WriteRecords(loutFica);
            writerFica.Close();
            Console.WriteLine("Fine programma");
        }

        private static List<Docfica> LoadFica(CsvReader csvFica)
        {
            try
            {
                var zfica = csvFica.GetRecords<Docfica>().ToList();
                return zfica;
            }
            catch (Exception)
            {
                Console.WriteLine("Errore su file {0}", "Doc-Fica");
                throw;
            }
        }
    }
}
