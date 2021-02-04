using Alperia_ISU_Lib;
using CsvHelper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace load_docfica
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Load DocFica start");

            var ctx = new DocficaContext();
            List<string> badRecord = new List<string>();
            var readerFica = new StreamReader("E:\\work\\Alperia\\PRD\\100_20210204_DOCFICA.csv");
            var csvFica = new CsvReader(readerFica, CultureInfo.InvariantCulture);
            csvFica.Configuration.Delimiter = ";";
            csvFica.Configuration.IgnoreQuotes = true;
            csvFica.Configuration.BadDataFound = context => badRecord.Add(context.RawRecord);
            
            //csvFica.Configuration.HeaderValidated = true;
            //csvFica.Configuration.MissingFieldFound = null;

            var lrecs = ProcessDocFica(csvFica);
            if (badRecord.Count > 0)
            {
                Console.WriteLine($"Errori nel processo file csv {badRecord.Count}");
            }

            InsMongoMulti(lrecs, ctx);

            Console.WriteLine("Load DocFica end");
        }

        private static List<Docfica> ProcessDocFica(CsvReader wcsv)
        {
            try
            {
                var zrecs = wcsv.GetRecords<Docfica>().ToList();
                return zrecs;
            }
            catch (Exception)
            {
                Console.WriteLine("Errore su file {0}", "Docfica");
                throw;
            }
        }
        private static void InsMongoMulti(List<Docfica> lDocfica, DocficaContext ctx)
        {
            try
            {
                ctx.DocficaCollection.InsertMany(lDocfica);
            }
            catch (Exception)
            {

                //Log.Information("errore scrittura");
            }
        }
    }
}
