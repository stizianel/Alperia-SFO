using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alperia_ISU_Lib;
using CsvHelper;
using MongoDB.Bson;
using MongoDB.Driver;
using Serilog;

namespace Isu_ver_fica
{
    class Program
    {
        static void Main(string[] args)
        {
            var ctx = new DocficaContext();

            Dictionary<string, string> Dmwskz = new Dictionary<string, string>();
            Dmwskz.Add("01", "1A");
            Dmwskz.Add("1X", "2A");
            Dmwskz.Add("1Y", "3A");
            Dmwskz.Add("2X", "4A");
            Dmwskz.Add("2Y", "3A");
            Dmwskz.Add("2Z", "6A");
            Dmwskz.Add("99", "A1");
            Dmwskz.Add("A2", "7A");
            Dmwskz.Add("A3", "8A");
            Dmwskz.Add("AA", "9A");
            Dmwskz.Add("AZ", "1B");
            Dmwskz.Add("C1", "2B");
            Dmwskz.Add("C5", "3B");
            Dmwskz.Add("C6", "4B");
            Dmwskz.Add("C8", "5B");
            Dmwskz.Add("C9", "6B");
            Dmwskz.Add("CB", "7B");
            Dmwskz.Add("CC", "8B");
            Dmwskz.Add("CD", "9B");
            Dmwskz.Add("CE", "1C");
            Dmwskz.Add("CG", "2C");
            Dmwskz.Add("CH", "3C");
            Dmwskz.Add("CI", "4C");
            Dmwskz.Add("CJ", "5C");
            Dmwskz.Add("CK", "6C");
            Dmwskz.Add("CL", "7C");
            Dmwskz.Add("CR", "8C");
            Dmwskz.Add("D0", "9C");
            Dmwskz.Add("D1", "1D");
            Dmwskz.Add("D4", "2D");
            Dmwskz.Add("DA", "3D");
            Dmwskz.Add("F7", "4D");
            Dmwskz.Add("F8", "5D");
            Dmwskz.Add("M2", "A2");
            Dmwskz.Add("M3", "A3");
            Dmwskz.Add("M5", "A4");
            Dmwskz.Add("M8", "A5");
            Dmwskz.Add("M9", "A6");
            Dmwskz.Add("MA", "A7");
            Dmwskz.Add("MB", "A8");
            Dmwskz.Add("MC", "A9");
            Dmwskz.Add("MI", "B1");
            Dmwskz.Add("MN", "B2");
            Dmwskz.Add("MZ", "B3");
            Dmwskz.Add("N0", "B4");
            Dmwskz.Add("N1", "B5");
            Dmwskz.Add("N2", "B6");
            Dmwskz.Add("N5", "B7");
            Dmwskz.Add("N6", "B8");
            Dmwskz.Add("N7", "B9");
            Dmwskz.Add("N8", "C1");
            Dmwskz.Add("N9", "C2");
            Dmwskz.Add("NA", "C3");
            Dmwskz.Add("NB", "C4");
            Dmwskz.Add("NG", "C5");
            Dmwskz.Add("NH", "C6");
            Dmwskz.Add("NJ", "C7");
            Dmwskz.Add("NK", "C8");
            Dmwskz.Add("NL", "C9");
            Dmwskz.Add("NM", "D1");
            Dmwskz.Add("O1", "D2");
            Dmwskz.Add("O3", "D3");
            Dmwskz.Add("OL", "D5");
            Dmwskz.Add("P1", "D6");
            Dmwskz.Add("Q1", "D7");
            Dmwskz.Add("Q8", "D8");
            Dmwskz.Add("Q9", "D9");
            Dmwskz.Add("R1", "E1");
            Dmwskz.Add("S1", "E2");
            Dmwskz.Add("T0", "E3");
            Dmwskz.Add("T3", "E4");
            Dmwskz.Add("T5", "E5");
            Dmwskz.Add("TA", "E6");
            Dmwskz.Add("TC", "E7");
            Dmwskz.Add("TI", "E8");
            Dmwskz.Add("TJ", "E9");
            Dmwskz.Add("TK", "F1");
            Dmwskz.Add("TL", "F2");
            Dmwskz.Add("TZ", "F3");
            Dmwskz.Add("X0", "F4");
            Dmwskz.Add("X1", "F5");
            Dmwskz.Add("XA", "F6");

            Dictionary<string, string> Dgsber = new Dictionary<string, string>();
            Dgsber.Add("AA70", "AD00");
            Dgsber.Add("CA80", "SC09");
            Dgsber.Add("CA81", "NA00");
            Dgsber.Add("CA82", "SC10");
            Dgsber.Add("CA83", "SC11");
            Dgsber.Add("CT11", "SC08");
            Dgsber.Add("CT90", "SC03");
            Dgsber.Add("CT91", "SC01");
            Dgsber.Add("CT92", "SC02");
            Dgsber.Add("CT93", "SC07");
            Dgsber.Add("CT94", "SC04");
            Dgsber.Add("CT95", "SC05");
            Dgsber.Add("CT97", "FC05");
            Dgsber.Add("ED40", "ED01");
            Dgsber.Add("ED41", "ED02");
            Dgsber.Add("ED42", "ED03");
            Dgsber.Add("ED44", "ED04");
            Dgsber.Add("ED46", "ED05");
            Dgsber.Add("ED47", "ED06");
            Dgsber.Add("ED48", "ED07");
            Dgsber.Add("ED49", "ED09");
            Dgsber.Add("EM43", "EM09");
            Dgsber.Add("EM44", "EM08");
            Dgsber.Add("EM45", "EM07");
            Dgsber.Add("EM46", "EM06");
            Dgsber.Add("EM47", "EM01");
            Dgsber.Add("EM48", "EM10");
            Dgsber.Add("EM49", "EM05");
            Dgsber.Add("EM50", "EM11");
            Dgsber.Add("EM51", "EM02");
            Dgsber.Add("EM52", "EM03");
            Dgsber.Add("EM53", "EM04");
            Dgsber.Add("EP10", "EP01");
            Dgsber.Add("EP12", "EP03");
            Dgsber.Add("EP14", "EP02");
            Dgsber.Add("EP16", "EP04");
            Dgsber.Add("ET15", "ET02");
            Dgsber.Add("ET17", "ET01");
            Dgsber.Add("EV49", "EVL1");
            Dgsber.Add("EV50", "EVL2");
            Dgsber.Add("EV53", "EVI1");
            Dgsber.Add("EV60", "EVT1");
            Dgsber.Add("EV61", "EVT2");
            Dgsber.Add("FC10", "FC03");
            Dgsber.Add("FC14", "FC02");
            Dgsber.Add("FC20", "FC04");
            Dgsber.Add("FT01", "FC06");
            Dgsber.Add("GC40", "GVI1");
            Dgsber.Add("GD20", "GD01");
            Dgsber.Add("GD22", "GD02");
            Dgsber.Add("GD34", "GD07");
            Dgsber.Add("GD46", "GD03");
            Dgsber.Add("GD50", "GD04");
            Dgsber.Add("GD51", "GD05");
            Dgsber.Add("GD52", "GD06");
            Dgsber.Add("GM18", "GM09");
            Dgsber.Add("GM30", "GM01");
            Dgsber.Add("GV02", "GVT2");
            Dgsber.Add("GV10", "GVT1");
            Dgsber.Add("GV11", "GVL1");
            Dgsber.Add("GV12", "GVL3");
            Dgsber.Add("GV13", "GVL2");
            Dgsber.Add("SM10", "SM00");
            Dgsber.Add("TF01", "TF00");
            Dgsber.Add("TLC1", "AD00");

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("E:\\work\\Alperia\\PRD\\log-fica.log", rollingInterval: RollingInterval.Minute)
            .CreateLogger();
            Log.Logger.Information("Inizio trasformazione");
            var readerFica = new StreamReader("E:\\work\\Alperia\\PRD\\20201129_1.csv");
            var csvFica = new CsvReader(readerFica, CultureInfo.InvariantCulture);
            csvFica.Configuration.Delimiter = ";";
            csvFica.Configuration.HeaderValidated = null;
            csvFica.Configuration.MissingFieldFound = null;
            var lfica = ProcessFica(csvFica);

            var fValid = new StreamReader("E:\\work\\Alperia\\PRD\\validaz-semplici.CSV");
            var csvValid = new CsvReader(fValid, CultureInfo.InvariantCulture);
            csvValid.Configuration.Delimiter = ";";
            List<ValidSemplici> lvalid = ValidSemplici.LoadValidSemplici(csvValid);

            var writerFica = new StreamWriter("E:\\work\\Alperia\\PRD\\20201129-FICA-out.csv");
            var outFica = new CsvWriter(writerFica, CultureInfo.InvariantCulture);
            outFica.Configuration.Delimiter = ";";
            var recFica = new Docfica();
            var lOutFica = new List<Docfica>();
            var lblarts = lfica.Select(x => x.BLART).Distinct().ToList();
            var lmwskzs = lfica.Select(x => x.MWSKZ).Distinct().ToList();
            var lgsbers = lfica.Select(x => x.GSBER).Distinct().ToList();
            var laugrds = lfica.Select(x => x.AUGRD).Distinct().ToList();
            File.WriteAllLines("E:\\work\\Alperia\\PRD\\blart.txt", lblarts);
            File.WriteAllLines("E:\\work\\Alperia\\PRD\\mwskz.txt", lmwskzs);
            File.WriteAllLines("E:\\work\\Alperia\\PRD\\gsber.txt", lgsbers);
            File.WriteAllLines("E:\\work\\Alperia\\PRD\\augrd.txt", laugrds);
            foreach (var fica in lfica)
            {
                recFica = new Docfica();
                recFica.SRC_SYSTEM  = fica.SRC_SYSTEM;
                recFica.RUN_ID = fica.RUN_ID;
                recFica.ROW_ID = fica.ROW_ID;
                recFica.KEY_EXT = fica.KEY_EXT;
                recFica.EXT_UI = decode_ext_ui(fica.EXT_UI);
                recFica.BPEXT = decode_bpext(fica.BPEXT);
                recFica.CF_TAXNUM = fica.CF_TAXNUM;
                recFica.PI_TAXNUM = fica.PI_TAXNUM;
                recFica.FLG_REC_IVA = fica.FL_NORIFCONTRA;
                recFica.BLART = decode_blart(fica.BLART);
                recFica.BLDAT = fica.BLDAT;
                recFica.XBLNR = decode_xblnr(fica.XBLNR);
                recFica.BETRW = decode_betrw(fica.BETRW);
                recFica.FAEDN = fica.FAEDN;
                recFica.HKONT = decode_hkont(fica.HKONT);
                recFica.VKONT = fica.VKONT;
                recFica.VTREF = fica.VTREF;
                //recFica.PRCTR = fica.PRCTR;
                recFica.OPTXT = fica.OPTXT;
                recFica.VALUT = fica.VALUT;
                recFica.RFZAS = fica.RFZAS;
                recFica.CL_RFZAS = fica.CL_RFZAS;
                recFica.BLOCCO_INTERESSI = decode_blocco_interessi(fica.BLOCCO_INTERESSI);
                recFica.AB_COMPE = fica.AB_COMPE;
                recFica.BIS_COMPE = fica.BIS_COMPE;
                recFica.AUGST = fica.AUGST;
                recFica.LOCKR_SOL = decode_lockr_sol(fica.LOCKR_SOL);
                recFica.TDATE_SOL = fica.TDATE_SOL;
                recFica.LOCKR_PAG = decode_lockr_pag(fica.LOCKR_PAG);
                recFica.TDATE_PAG = fica.TDATE_PAG;
                recFica.LOCKR_PAR = fica.LOCKR_PAR;
                recFica.TDATE_PAR = fica.TDATE_PAR;
                recFica.AUGRD = decode_augrd(fica.AUGRD);
                recFica.ADD_REFOBJ = fica.ADD_REFOBJ;
                recFica.GSBER = decode_gsber(fica.GSBER, Dgsber);
                recFica.ZNUM_PIANO = fica.ZNUM_PIANO;
                recFica.ZSCAD_RATA = fica.ZSCAD_RATA;
                recFica.AUSDT = fica.AUSDT;
                recFica.STEP = decode_step(fica.STEP);
                recFica.ID_SOLLECITO = fica.ID_SOLLECITO;
                recFica.LOTTO_AFFIDO = fica.LOTTO_AFFIDO;
                recFica.ID_STATO = fica.ID_STATO;
                recFica.FISC_AFFIDO = fica.FISC_AFFIDO;
                recFica.IMPORTO_IVA = fica.IMPORTO_IVA;
                recFica.SBASH = fica.SBASH;
                recFica.SCTAX = fica.SCTAX;
                recFica.MWSKZ = decode_mwskz(fica.MWSKZ, Dmwskz);
                recFica.FLG_REC_IVA = fica.FLG_REC_IVA;
                recFica.SPERZ = fica.SPERZ;
                recFica.STUDT = fica.STUDT;
                lOutFica.Add(recFica);
            }
            outFica.WriteRecords<Docfica>(lOutFica);
            writerFica.Close();
            Log.Logger.Information("Fine trasformazione");
            Log.Logger.Information("Inizio verifica");
            foreach (var rec in lOutFica)
            {
                //Console.WriteLine($"{rec.ROW_ID}");
                ValidationContext context = new ValidationContext(rec, null, null);
                List<ValidationResult> validationResults = new List<ValidationResult>();
                bool valid = Validator.TryValidateObject(rec, context, validationResults, true);
                if (!valid)
                {
                    foreach (ValidationResult validationResult in validationResults)
                    {
                        Console.WriteLine("Riga {1} - {0}", validationResult.ErrorMessage, rec.EXT_UI);
                        Log.Logger.Error("Riga {1} - {0}", validationResult.ErrorMessage, rec.EXT_UI);
                    }
                }
                FiCaValidator validator = new FiCaValidator(lvalid);
                FluentValidation.Results.ValidationResult results = validator.Validate(rec);
                if (!results.IsValid)
                {
                    foreach (var failure in results.Errors)
                    {
                        Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                        Log.Logger.Error("Riga {0} - {1} - {2}", rec.ROW_ID, failure.PropertyName, failure.ErrorMessage);
                    }
                }
            }
            Log.Logger.Information("Fine Verifica");
            
            InsMongoMulti(lOutFica, ctx);
            Console.ReadKey();
        }

        private static string decode_ext_ui(string i_ext_ui)
        {
            if(string.IsNullOrEmpty(i_ext_ui))
            {
                return "IT999999999999";
            } else
            {
                return i_ext_ui;
            }
        }

        private static string decode_bpext(string i_bpext)
        {
            return i_bpext.Replace('-', '_');
        }

        private static string decode_augrd(string i_augrd)
        {
            if (i_augrd == "Z1" || i_augrd == "Z3")
            {
                return "M1";
            } else if (i_augrd == "05")
            {
                return "04";
            } else
            {
                return i_augrd;
            }
        }

        private static string decode_step(string i_step)
        {
            if (i_step == "00")
            {
                return "C010";
            } else if (i_step == "01" || i_step == "06")
            {
                return "C030";
            } else if (i_step == "10")
            {
                return "C060";
            } else
            {
                return i_step;
            }

        }

        private static string decode_mwskz(string i_mwskz, Dictionary<string, string> Dmwskz)
        {
            if (Dmwskz.ContainsKey(i_mwskz))
            {
                return Dmwskz[i_mwskz];
            } else
            {
                return "";
            }
        }

        private static string decode_lockr_pag(string i_lockr_pag)
        {
            if(i_lockr_pag == "2" || i_lockr_pag == "S" || i_lockr_pag == "L")
            {
                return "T";
            } else
            {
                return i_lockr_pag;
            }
        }

        private static string decode_lockr_sol(string i_lockr_sol)
        {
            if (i_lockr_sol == "T" || i_lockr_sol == "2" || i_lockr_sol == "6" || i_lockr_sol == "9")
            {
                return "G";
            }
            else
            {
                return i_lockr_sol;
            }
        }

        private static string decode_gsber(string i_gsber, Dictionary<string, string> Dgsber)
        {
            if (Dgsber.ContainsKey(i_gsber))
            {
                return Dgsber[i_gsber];
            }
            else
            {
                return "";
            }

        }
        private static string decode_blocco_interessi(string i_blocco_interessi)
        {
            if (i_blocco_interessi == "R" || i_blocco_interessi == "Z")
            {
                return "A";
            } else
            {
                return i_blocco_interessi;
            }
        }

        private static string decode_hkont(string hkont)
        {
            return "22100010";
        }

        private static string decode_xblnr(string i_xblnr)
        {
            if (String.IsNullOrEmpty(i_xblnr))
            {
                return "999999";
            } else
            {
                return i_xblnr;
            }
        }

        private static string decode_betrw(string betrw)
        {
            return betrw;
        }

        private static string decode_blart(string i_blart)
        {
            if (i_blart == "BA" || i_blart == "BO" || i_blart == "BP" || i_blart == "CB" || i_blart == "CC" || i_blart == "CR" || i_blart == "RI")
            {
                return "MB";
            } else if (i_blart == "CA")
            {
                return "MC";
            } else if (i_blart == "DC")
            {
                return "MD";
            } else if (i_blart == "IM")
            {
                return "MI";
            } else if (i_blart == "RA")
            {
                return "MP";
            }
            return "MG";
        }

        private static List<Docfica> ProcessFica(CsvReader csvFica)
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
        private static void InsMongoSingle(Docfica doc, DocficaContext ctx)
        {
            try
            {
                ctx.DocficaCollection.InsertOne(doc);
            }
            catch (Exception)
            {

                Log.Information("errore insert Docfica");
            }
        }

        private static void InsMongoMulti(List<Docfica> docs, DocficaContext ctx)
        {
            try
            {
                Console.WriteLine("Wait for DB insert");
                ctx.DocficaCollection.InsertMany(docs);
                Console.WriteLine("DB insert executed");
            }
            catch (Exception)
            {

                Log.Information("errore insert multi Docfica");
            }
        }
    }
}
