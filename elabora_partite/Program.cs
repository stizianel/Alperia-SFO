using Alperia_ISU_Lib;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using MongoDB.Driver.Linq;
using MongoDB.Driver;
using Serilog;

namespace elabora_partite
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Elabora partite");
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            //.WriteTo.Console()
            .WriteTo.File("c:\\work\\Alperia\\PRD\\log-elabora-partite.log", rollingInterval: RollingInterval.Hour)
            .CreateLogger();
            Log.Logger.Information("Inizio elaborazione");

            var cte = new MainEleContext();
            var ctg = new MainGasContext();


            var db = cte.MainEleCollection.Database;
            var cEle = db.GetCollection<MainEle>("MainEle");
            var cGas = db.GetCollection<MainGas>("MainGas");


            var firstEle = cEle.AsQueryable<MainEle>().FirstOrDefault();
            Console.WriteLine("inizio caricamento liste");
            //var CtrGas = cGas.AsQueryable<MainGas>().ToList();
            //var CtrEle = cEle.AsQueryable<MainEle>().ToList();
            Console.WriteLine("fine caricamento liste");
            List<string> BpMancanti = new List<string>();
            List<string> ContrattiMancanti = new List<string>();

            var readerDfkkop = new StreamReader("c:\\work\\Alperia\\PRD\\aperti400.csv");
            var csvDfkkop = new CsvReader(readerDfkkop, CultureInfo.InvariantCulture);
            csvDfkkop.Configuration.Delimiter = "|";
            csvDfkkop.Configuration.BadDataFound = null;
            csvDfkkop.Configuration.TrimOptions = CsvHelper.Configuration.TrimOptions.Trim;
            Console.WriteLine("inizio caricamento partite");
            var lPartite = ProcessPartite(csvDfkkop);
            Console.WriteLine("fine caricamento partite");

            if (args[0] == "PARZ")
            {
                foreach (var item in lPartite)
                {
                    if(item.VTREF == "")
                    {
                        BpMancanti.Add(item.GPART);
                    } else
                    {
                        ContrattiMancanti.Add(item.VTREF);
                    }
                }
                var BpToFile = BpMancanti.Distinct();
                var CtrToFile = ContrattiMancanti.Distinct();
                System.IO.File.WriteAllLines("c:\\work\\Alperia\\PRD\\BpMancanti.txt", BpToFile.ToList());
                System.IO.File.WriteAllLines("c:\\work\\Alperia\\PRD\\CtrMancanti.txt", CtrToFile.ToList());
                Console.WriteLine("Fine parziale");
                Environment.Exit(0);
            }

            List<Docfica> lDocFica = new List<Docfica>();
            var dep_row_id = 0;
            int righe = 0;
            Console.WriteLine("inizo processo");
            foreach (var recDfk in lPartite)
            {
                if (recDfk.VTREF == "")
                {
                    var wCfPi = GetCfPi(recDfk.GPART, recDfk.SPART, cEle, cGas);
                }
                dep_row_id += 1;
                var res = ComponiDocFica(recDfk, firstEle, dep_row_id, wCfPi);
                lDocFica.Add(res);
                Console.WriteLine(righe += 1);
                if(res.EXT_UI == "")
                {
                    ContrattiMancanti.Add(res.VTREF);
                }
                if(res.CF_TAXNUM == "ERR")
                {
                    BpMancanti.Add(res.BPEXT);
                }
            }

            Console.WriteLine("Fine programma");
        }

        
        private static Docfica ComponiDocFica(Dfkkop recDfk, 
                                               MainEle recEle, 
                                               int row_id, 
                                               IMongoCollection<MainEle> cEle,
                                               IMongoCollection<MainGas> cGas)
        {
            var rdf = new Docfica();
            rdf.SRC_SYSTEM = recEle.SRC_SYSTEM;
            rdf.RUN_ID = recEle.RUN_ID;
            rdf.ROW_ID = row_id.ToString();
            rdf.BLART = recDfk.BLART;
            rdf.BLDAT = decode_data(recDfk.BLDAT);
            rdf.XBLNR = recDfk.XBLNR;
            rdf.BETRW = recDfk.BETRW;
            rdf.FAEDN = decode_data(recDfk.FAEDN);
            rdf.HKONT = recDfk.HKONT;
            rdf.VKONT = setVkont(recDfk.VKONT);   // String.Concat("100_", recDfk.VKONT);
            rdf.VTREF = setVtref(recDfk.VTREF); // String.Concat("100_", recDfk.VTREF);
            rdf.EXT_UI = getExtUi(rdf.VTREF, recDfk.SPART, cEle, cGas);
            rdf.OPTXT = recDfk.OPTXT;
            rdf.AUGST = recDfk.AUGST;
            rdf.AUGRD = recDfk.AUGRD;
            rdf.GSBER = recDfk.GSBER;
            rdf.MWSKZ = recDfk.MWSKZ;
            rdf.SCTAX = recDfk.SCTAX;
            rdf.BPEXT = setBpext(recDfk.GPART);
            var cfPi = GetCfPi(rdf.BPEXT, recDfk.SPART, cEle, cGas);
            rdf.CF_TAXNUM = cfPi.Item1;
            rdf.PI_TAXNUM = cfPi.Item2;
            return rdf;
        }

        private static Tuple<string, string> GetCfPi(string bPEXT, 
                                        string sPART,
                                        IMongoCollection<MainEle> cEle,
                                        IMongoCollection<MainGas> cGas)
        {
            var resE = cEle.AsQueryable<MainEle>().Where(p => p.BPEXT == bPEXT).FirstOrDefault();
            if (resE != null)
            {
                return Tuple.Create(resE.CF_TAXNUM, resE.PI_TAXNUM);
                
            } else
            {
                var resG = cGas.AsQueryable<MainGas>().Where(p => p.BPEXT == bPEXT).FirstOrDefault();
                if (resG != null)
                {
                    return Tuple.Create(resG.CF_TAXNUM, resG.PI_TAXNUM);
                } else
                {
                    Log.Logger.Error("BP mancante {0}", bPEXT);
                    return Tuple.Create("ERR","ERR");
                    
                }
            }
        }

        private static string setBpext(string gPART)
        {
            if (String.IsNullOrEmpty(gPART))
            {
                return gPART;
            }
            return String.Concat("100_", gPART.PadLeft(10, '0'));
        }

        private static string setVkont(string vKONT)
        {
            if (String.IsNullOrEmpty(vKONT))
            {
                return vKONT;
            }
            return String.Concat("100_", vKONT.PadLeft(12, '0'));
        }

        private static string setVtref(string vTREF)
        {
            if(String.IsNullOrEmpty(vTREF))
            {
                return vTREF;
            }
            return String.Concat("100_", vTREF.PadLeft(10, '0'));
        }

        private static string getExtUi(string vTREF, 
                                        string sPART, 
                                        IMongoCollection<MainEle> cEle,
                                        IMongoCollection<MainGas> cGas )
        {
            if(String.IsNullOrEmpty(vTREF))
            {
                return "IT999999999999";
            }
            if(sPART == "E8")
            {
                var res = cEle.AsQueryable<MainEle>().Where(p => p.VREFER == vTREF).FirstOrDefault();
                if(res is null)
                {
                    Log.Logger.Error("Contratto mancante {0}", vTREF);
                    return "IT999999999999";
                } else
                {
                    return res.EXT_UI;
                }
            } else if (sPART == "G1")
            {
                var res = cGas.AsQueryable<MainGas>().Where(p => p.VREFER == vTREF).FirstOrDefault();
                if (res is null)
                {
                    Log.Logger.Error("Contratto mancante {0}", vTREF);
                    return "IT999999999999";
                }
                else
                {
                    return res.EXT_UI;
                }
            } else
            {
                return "IT999999999999";
            }
        }

        private static string decode_data(string bLDAT)
        {
            //06.10.2016
            if (String.IsNullOrEmpty(bLDAT))
            {
                return "";
            }
            var anno = bLDAT.Substring(6, 4);
            var mese = bLDAT.Substring(3, 2);
            var giorno = bLDAT.Substring(0, 2);
            return String.Concat(anno, mese, giorno);
        }

        private static List<Dfkkop> ProcessPartite(CsvReader csvDfkkop)
        {
            try
            {
                var zGas = csvDfkkop.GetRecords<Dfkkop>().ToList();
                return zGas;
            }
            catch (Exception)
            {
                Console.WriteLine("Errore su file {0}", "MainGas");
                throw;
            }
        }
    }
    
}
