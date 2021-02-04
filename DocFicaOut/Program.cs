using Alperia_ISU_Lib;
using CsvHelper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace DocFicaOut
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("DocFicaOut!");
            var ctx = new DocficaContext();
            using var writer = new StreamWriter("e:\\work\\Alperia\\PRD\\100_DOCFICA.csv");
            using var outCsv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            outCsv.Configuration.Delimiter = ";";
            outCsv.Configuration.HasHeaderRecord = true;

            var aList = ctx.DocficaCollection.AsQueryable().ToList();
            List<Docfica> Lout = new List<Docfica>();
            var blartExcludi = new List<String> { "MV", "MO", "MT", "MF", "MU", "IS", "PA", "ST", "MI" };
            foreach (var item in aList)
            {
                if (item.AUGST == "9" && String.IsNullOrEmpty(item.XBLNR) && blartExcludi.Contains(item.BLART)) {
                    Console.WriteLine($"Scartato {item.BLART} {item.AUGST} {item.XBLNR}");
                    continue;
                } else
                {
                    Lout.Add(item);
                }
            };
            outCsv.WriteRecords(Lout);
            writer.Close();
            Console.WriteLine("Fine DocFicaOut");
        }
    }
}
