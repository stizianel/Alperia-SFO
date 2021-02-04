using CsvHelper;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace MainGasOut
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("MainGasOut!");
            var ctx = new MainGasContext();
            using var writer = new StreamWriter("e:\\work\\Alperia\\PRD\\400_MAINGAS.csv");
            using var outCsv = new CsvWriter(writer, CultureInfo.InvariantCulture);
            outCsv.Configuration.Delimiter = ";";
            outCsv.Configuration.HasHeaderRecord = true;

            var LGas = ctx.MainGasCollection.AsQueryable().Where(p => p.SRC_SYSTEM == "PRDCLNT400").ToList();

            List<MainGas> LoutGas = new List<MainGas>();

            foreach (var rgas in LGas)
            {
                var depRec = new MainGas();
                depRec = rgas;
                // Caso 1 Cessato
                if(rgas.AUSZDAT != "99991231")
                {
                    depRec = SistemaCaso1(rgas);
                };
                // Caso 2a
                if (rgas.AUSZDAT == "99991231" && (rgas.Z_CUTOFF == rgas.EINZDAT))
                {
                    depRec = SistemaCaso2a(depRec);
                }
                if (rgas.AUSZDAT == "99991231" && (rgas.Z_CUTOFF == "20201231"))
                {
                    depRec = SistemaCaso2b(depRec);
                }
                depRec = SistemaPcs(depRec);

                LoutGas.Add(depRec);
            }
            outCsv.WriteRecords(LoutGas);
            writer.Close();
            Console.WriteLine("Fine MainGasOut");
        }

        private static MainGas SistemaCaso2a(MainGas depRec)
        {
            //REA_ADAT, EADAT, KEYDATE, EADAT_MIS, EDAT_COR, REA_ADAT_COR
            depRec.KEYDATE = depRec.EINZDAT;
            if (!String.IsNullOrEmpty(depRec.EADAT_MIS))
            {
                depRec.EADAT_MIS = depRec.EINZDAT;
            }
            if (!String.IsNullOrEmpty(depRec.REA_ADAT))
            {
                depRec.REA_ADAT = depRec.EINZDAT;
            }
            if(!String.IsNullOrEmpty(depRec.EADAT_COR))
            {
                depRec.EADAT_COR = depRec.EINZDAT;
            }
            if (!String.IsNullOrEmpty(depRec.REA_ADAT_COR))
            {
                depRec.REA_ADAT_COR = depRec.EINZDAT;
            }
            return depRec;
        }

        private static MainGas SistemaCaso1(MainGas rgas)
        {
            rgas.Z_CUTOFF = rgas.AUSZDAT;
            return rgas;
        }

        private static MainGas SistemaCaso2b(MainGas rgas)
        {
            //REA_ADAT, EADAT, KEYDATE, EADAT_MIS, EDAT_COR, REA_ADAT_COR
            rgas.KEYDATE = "20210101";
            if(!String.IsNullOrEmpty(rgas.EADAT_MIS))
            {
                rgas.EADAT_MIS = "20210101";
                
            };
            if (!String.IsNullOrEmpty(rgas.EADAT_COR))
            {
                rgas.EADAT_COR = "20210101";

            };
            if (!String.IsNullOrEmpty(rgas.REA_ADAT))
            {
                rgas.REA_ADAT = "20210101";

            };
            if (!String.IsNullOrEmpty(rgas.REA_ADAT_COR))
            {
                rgas.REA_ADAT_COR = "20210101";

            };
            return rgas;
        }

        private static MainGas SistemaPcs(MainGas rgas)
        {
            if (rgas.OP_GF_PCSZERO == "0.3810000")
            {
                rgas.OP_GF_PCSZERO = "0.0381000";
            } else if (rgas.OP_GF_PCSZERO == "0.3852000")
            {
                rgas.OP_GF_PCSZERO = "0.0385200";
            } else
            {
                rgas.OP_GF_PCSZERO = "0.0100000";
            }
            return rgas;
        }
    }
}
