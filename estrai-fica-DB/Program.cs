using Alperia_ISU_Lib;
using CsvHelper;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace estrai_fica_DB
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var ctx = new DocficaContext();
            var db = ctx.DocficaCollection.Database;
            var docFica = db.GetCollection<Docfica>("Docfica");

            var writerFica = new StreamWriter("e:\\work\\Alperia\\PRD\\chiuse-dal0111-docfica100.csv");
            var outFica = new CsvWriter(writerFica, CultureInfo.InvariantCulture);
            outFica.Configuration.Delimiter = ";";
            outFica.Configuration.HasHeaderRecord = true;

            var res = docFica.AsQueryable<Docfica>().Where(d => d.AUGST == "9")
                .ToList()
                .Where(x => DateTime.ParseExact(x.BLDAT, "yyyyMMdd", CultureInfo.InvariantCulture).Date >= new DateTime(2020, 11, 01).Date)
                .ToList();

            outFica.WriteRecords(res);
            writerFica.Close();

            Console.WriteLine("Fine pgm");
        }
    }
}
