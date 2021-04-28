using Alperia_ISU_Lib;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace load_Dfkkop
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Start load_Dfkkop!");
            var ctx = new DfkkopContext();

            var readerDfkkop = new StreamReader("c:\\$work\\Alperia\\PRD\\dfkkop.csv", Encoding.GetEncoding("ISO-8859-1"));
            var csvDfkkop = new CsvReader(readerDfkkop, CultureInfo.InvariantCulture);
            csvDfkkop.Configuration.Delimiter = "|";
            csvDfkkop.Configuration.BadDataFound = null;
            csvDfkkop.Configuration.TrimOptions = CsvHelper.Configuration.TrimOptions.Trim;
            Console.WriteLine("inizio caricamento partite");
            var lPartite = ProcessPartite(csvDfkkop);

            InsMongoMulti(lPartite, ctx);

            Console.WriteLine("fine caricamento partite");
        }

        private static void InsMongoMulti(List<Dfkkop> lPartite, DfkkopContext ctx)
        {
            try
            {
                ctx.DfkkopCollection.InsertMany(lPartite);
            }
            catch (Exception)
            {

                //Log.Information("errore scrittura");
            }
        }

        private static List<Dfkkop> ProcessPartite(CsvReader csvDfkkop)
        {
            try
            {
                var zPar = csvDfkkop.GetRecords<Dfkkop>().ToList();
                return zPar;
            }
            catch (Exception)
            {
                Console.WriteLine("Errore su file {0}", "Dfkkop");
                throw;
            }
        }
    }
}
