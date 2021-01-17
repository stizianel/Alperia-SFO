using Alperia_ISU_Lib;
using CsvHelper;
using MongoDB.Driver;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace elabora_dfkkop_sum
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            //.WriteTo.Console()
            .WriteTo.File("c:\\work\\Alperia\\PRD\\log-elabora-partite-400.log", rollingInterval: RollingInterval.Hour)
            .CreateLogger();
            Log.Logger.Information("Inizio elaborazione");

            var ctx = new ZaeContext();
            var db = ctx.ZaeCollection.Database;
            var cZae = db.GetCollection<Zae>("Zae");

            var readerDfkkop = new StreamReader("c:\\work\\Alperia\\PRD\\chiusi_400.csv");
            var csvDfkkop = new CsvReader(readerDfkkop, CultureInfo.InvariantCulture);
            csvDfkkop.Configuration.Delimiter = "|";
            csvDfkkop.Configuration.BadDataFound = null;
            csvDfkkop.Configuration.TrimOptions = CsvHelper.Configuration.TrimOptions.Trim;

            var writerFica = new StreamWriter("c:\\work\\Alperia\\PRD\\400_20210116_C_DOCFICA.csv");
            var csvFica = new CsvWriter(writerFica, CultureInfo.InvariantCulture);
            csvFica.Configuration.Delimiter = ";";
            csvFica.Configuration.HasHeaderRecord = false;

            var lPartite = ProcessPartite(csvDfkkop);
            // sezione creazione DocFica
            int ind = 1199;

          List<Docfica> lDocFica = new List<Docfica>();

            foreach (var p in lPartite)
            {
                var dep_doc = new Docfica();
                var dep_dati = getFromZae(p.VTREF, cZae, p.GPART);
                dep_doc.SRC_SYSTEM = "PRDCLNT400";
                dep_doc.RUN_ID = "20210117_1";
                dep_doc.ROW_ID = ind++.ToString().PadLeft(10, '0');
                dep_doc.KEY_EXT = p.OPBEL + p.OPUPK.PadLeft(3,'0');
                dep_doc.EXT_UI = dep_dati.Item3;
                dep_doc.BPEXT = String.Concat("400_", p.GPART.PadLeft(10, '0'));
                dep_doc.CF_TAXNUM = dep_dati.Item1;
                dep_doc.PI_TAXNUM = dep_dati.Item2;
                dep_doc.FL_NORIFCONTRA = "";
                dep_doc.BLART = p.BLART;
                dep_doc.BLDAT = decode_data(p.BLDAT);
                dep_doc.XBLNR = p.XBLNR;
                var depNu = System.Convert.ToDecimal(p.BETRW);
                dep_doc.BETRW = depNu.ToString("+0.00;-#.00");
                dep_doc.FAEDN = decode_data(p.FAEDN);
                dep_doc.HKONT = p.HKONT;
                dep_doc.VKONT = String.Concat("400_", p.VKONT.PadLeft(12, '0')); 
                dep_doc.VTREF = String.Concat("400_", p.VTREF.PadLeft(10, '0'));
                dep_doc.OPTXT = p.OPTXT;
                dep_doc.VALUT = "00000000";
                dep_doc.RFZAS = "";
                dep_doc.CL_RFZAS = "";
                dep_doc.BLOCCO_INTERESSI = "";
                dep_doc.AB_COMPE = "00000000";
                dep_doc.BIS_COMPE = "00000000";
                dep_doc.AUGST = p.AUGST;
                dep_doc.LOCKR_SOL = "";
                dep_doc.TDATE_SOL = "00000000";
                dep_doc.LOCKR_PAG = "";
                dep_doc.TDATE_PAG = "00000000";
                dep_doc.LOCKR_PAR = "";
                dep_doc.TDATE_PAR = "00000000";
                dep_doc.AUGRD = p.AUGRD;
                dep_doc.ADD_REFOBJ = "";
                dep_doc.PYMET = "";
                dep_doc.GSBER = p.GSBER;
                dep_doc.ZNUM_PIANO = "";
                dep_doc.ZSCAD_RATA = "00000000";
                dep_doc.AUSDT = "00000000";
                dep_doc.STEP = "";
                dep_doc.ID_SOLLECITO = "";
                dep_doc.LOTTO_AFFIDO = "";
                dep_doc.ID_STATO = "";
                dep_doc.FISC_AFFIDO = "";
                dep_doc.IMPORTO_IVA = "+0,00";
                dep_doc.SBASH = "+0,00";
                var depSc = System.Convert.ToDecimal(p.SCTAX);
                dep_doc.SCTAX = depSc.ToString("+0.00;-#.00"); ;
                dep_doc.MWSKZ = p.MWSKZ;
                dep_doc.FLG_REC_IVA = "";
                dep_doc.SPERZ = p.SPERZ;
                dep_doc.STUDT = decode_data(p.STUDT);
                lDocFica.Add(dep_doc);
                Console.WriteLine("Elaborato partita {0}", dep_doc.ROW_ID);
            }
            csvFica.WriteRecords(lDocFica);
            writerFica.Close();
            Console.WriteLine("Programma terminato");
            Log.Logger.Information("Fine elaborazione");
        }

        private static string decode_data(string bLDAT)
        {
            if (String.IsNullOrEmpty(bLDAT))
            {
                return "00000000";
            }
            var anno = bLDAT.Substring(6, 4);
            var mese = bLDAT.Substring(3, 2);
            var giorno = bLDAT.Substring(0, 2);
            return String.Concat(anno, mese, giorno);
        }

        private static Tuple<string, string, string> getFromZae(string vTREF, MongoDB.Driver.IMongoCollection<Zae> cZae, string gPART)
        {
            if (!string.IsNullOrEmpty(vTREF))
            {
                var res = cZae.AsQueryable<Zae>().Where(p => p.Vtref == vTREF && p.Mandt == "400").FirstOrDefault();
                if (res != null)
                {
                    return Tuple.Create(res.CF, res.PI, res.ExtUi);
                } else
                {
                    Log.Logger.Error("Zae non trovata per vtref {0}", vTREF);
                }
            } else
            {
                var res = cZae.AsQueryable<Zae>().Where(p => p.Gpart == gPART && p.Mandt == "400").FirstOrDefault();
                if (res != null)
                {
                    return Tuple.Create(res.CF, res.PI, "");
                } else
                {
                    Log.Logger.Error("Zae non trovata per gpart {0}", gPART);
                }
            }
            
            return Tuple.Create("", "", "");
        }

        private static List<Dfkkop> ProcessPartite(CsvReader csvDfkkop)
        {
            try
            {
                var zRes = csvDfkkop.GetRecords<Dfkkop>().ToList();
                return zRes;
            }
            catch (Exception)
            {
                Console.WriteLine("Errore su file {0}", "Partite");
                throw;
            }
        }
    }
}
