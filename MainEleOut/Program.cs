using CsvHelper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MainEleOut
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MainEleOut!");
            var ctx = new MainEleContext();
            using var writer = new StreamWriter("e:\\work\\Alperia\\PRD\\400_MAINELE.csv");
            using var outCsv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            outCsv.Configuration.Delimiter = ";";
            outCsv.Configuration.HasHeaderRecord = true;

            var LEle = ctx.MainEleCollection.AsQueryable().Where(p => p.SRC_SYSTEM == "PRDCLNT400").ToList();

            List<MainEle> LoutEle = new List<MainEle>();
            foreach (var rele in LEle)
            {
                var depRec = new MainEle();
                depRec = rele;
                // Caso 1 Cessato
                if (rele.AUSZDAT != "99991231")
                {
                    depRec = SistemaCaso1(rele);
                };
                // Caso 2a
                if (rele.AUSZDAT == "99991231" && (rele.Z_CUTOFF == rele.EINZDAT))
                {
                    depRec = SistemaCaso2a(depRec);
                }
                if (rele.AUSZDAT == "99991231" && (rele.Z_CUTOFF == "20201231"))
                {
                    depRec = SistemaCaso2b(depRec);
                }
                LoutEle.Add(depRec);
            }
            outCsv.WriteRecords(LoutEle);
            writer.Close();
            Console.WriteLine("Fine MainEleOut");
        }
        private static MainEle SistemaCaso1(MainEle rele)
        {
            rele.Z_CUTOFF = rele.AUSZDAT;
            return rele;
        }
        private static MainEle SistemaCaso2a(MainEle depRec)
        {
            //REA_ADAT, EADAT, KEYDATE, EADAT_MIS, EDAT_COR, REA_ADAT_COR
            depRec.KEYDATE = depRec.EINZDAT;
            if (!String.IsNullOrEmpty(depRec.EADAT))
            {
                depRec.EADAT = depRec.EINZDAT;
            }
            if (!String.IsNullOrEmpty(depRec.REA_ADAT))
            {
                depRec.REA_ADAT = depRec.EINZDAT;
            }
            return depRec;
        }
        private static MainEle SistemaCaso2b(MainEle rec)
        {
            //REA_ADAT, EADAT, KEYDATE, EADAT_MIS, EDAT_COR, REA_ADAT_COR
            rec.KEYDATE = "20210101";
            if (!String.IsNullOrEmpty(rec.EADAT))
            {
                rec.EADAT = "20210101";

            };
            if (!String.IsNullOrEmpty(rec.REA_ADAT))
            {
                rec.REA_ADAT = "20210101";

            };
           
            return rec;
        }
    }
}
