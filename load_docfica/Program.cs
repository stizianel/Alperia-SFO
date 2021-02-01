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

            var readerFica = new StreamReader("E:\\work\\Alperia\\PRD\\100_20210120_1_DOCFICA.csv");
            var csvFica = new CsvReader(readerFica, CultureInfo.InvariantCulture);
            csvFica.Configuration.Delimiter = ";";
            //csvFica.Configuration.HeaderValidated = true;
            //csvFica.Configuration.MissingFieldFound = null;

            var lrecs = ProcessDocFica(csvFica);

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
