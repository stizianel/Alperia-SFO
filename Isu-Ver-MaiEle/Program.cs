using CsvHelper;
using Serilog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isu_Ver_MaiEle
{
    class Program
    {
        static void Main(string[] args)
        {
            var dt_cutoff = args[0];

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("E:\\work\\Alperia\\PRD\\log-MainEle.log", rollingInterval: RollingInterval.Minute)
            .CreateLogger();

            var fValid = new StreamReader("E:\\work\\Alperia\\PRD\\validaz-semplici.CSV");
            var csvValid = new CsvReader(fValid, CultureInfo.InvariantCulture);
            csvValid.Configuration.Delimiter = ";";
            List<ValidSemplici> lvalid = ValidSemplici.LoadValidSemplici(csvValid);

            var readerEle = new StreamReader("E:\\work\\Alperia\\PRD\\100_20201106_MAIN_ELE.csv");
            var csvEle = new CsvReader(readerEle, CultureInfo.InvariantCulture);
            csvEle.Configuration.Delimiter = ";";
            csvEle.Configuration.BadDataFound = null;

            List<MainEle> lEle = ProcessEle(csvEle);
            var lEleTemp = lEle.Skip(2000).Take(100);

            var wrBps = new StreamWriter("E:\\work\\Alperia\\PRD\\BpEle.txt");
            var csvBps = new CsvWriter(wrBps, CultureInfo.InvariantCulture);
            csvBps.Configuration.Delimiter = ";";
            csvBps.Configuration.HasHeaderRecord = false;
            
            Log.Logger.Information("Inizio validazione");
            //Log.Logger.Information("Inizio Estrazione elenco BP");

            //List<string> lBps = lEle.Select(p => p.BPEXT).Distinct().ToList();
            //var Bpcode = "";
            //foreach (var item in lBps)
            //{
            //    var item_splitted = item.Split('_');
            //    if(item_splitted.Length == 2)
            //    {
            //        Bpcode = item_splitted[1];
            //    }
            //    wrBps.WriteLine(Bpcode);
            //}
            //wrBps.Close();
            //Log.Logger.Information("Fine Estrazione elenco BP");

            foreach (var rec in lEleTemp)
            {
                ValidationContext context = new ValidationContext(rec, null, null);
                List<ValidationResult> validationResults = new List<ValidationResult>();
                bool valid = Validator.TryValidateObject(rec, context, validationResults, true);
                if (!valid)
                {
                    foreach (ValidationResult validationResult in validationResults)
                    {
                        Console.WriteLine("Riga {1} - {0}", validationResult.ErrorMessage, rec.ROW_ID);
                        Log.Logger.Error("Riga {1} - {0}", validationResult.ErrorMessage, rec.ROW_ID);
                    }
                }
                EleValidator validator = new EleValidator(lvalid, dt_cutoff);
                //Console.WriteLine($"{rec.OP_ER_OPZAEEG} - {rec.OP_ER_TIPOUT_TF} - {rec.OP_ER_RESI_TF} - {rec.OP_ER_LIVTEN_TF} - {rec.ZTENS} - {rec.OP_ED_POTDIS}- {rec.OP_ED_POTCON}");
                FluentValidation.Results.ValidationResult results = validator.Validate(rec);
                if (!results.IsValid)
                {
                    foreach (var failure in results.Errors)
                    {
                        Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                        Log.Logger.Error("Riga {0} - {1} - {2}", rec.ROW_ID, failure.PropertyName, failure.ErrorMessage);
                    }
                }
                bool aeeg = validator.IsValidOpzAeeg(rec.OP_ER_OPZAEEG,
                                    rec.OP_ER_TIPOUT_TF,
                                    rec.OP_ER_RESI_TF,
                                    rec.OP_ER_LIVTEN_TF,
                                    int.Parse(rec.ZTENS),
                                    float.Parse(rec.OP_ED_POTDIS, CultureInfo.InvariantCulture.NumberFormat),
                                    float.Parse(rec.OP_ED_POTCON, CultureInfo.InvariantCulture.NumberFormat));
                if (!aeeg)
                {
                    Log.Logger.Error($"errore opzione aeeg riga {rec.ROW_ID} opzione {rec.OP_ER_OPZAEEG}");
                }
            }

            Log.Logger.Information("Fine validazione");
            
            Console.ReadKey();
        }

        private static List<MainEle> ProcessEle(CsvReader csvEle)
        {
            try
            {
                var zEle = csvEle.GetRecords<MainEle>().ToList();
                return zEle;
            }
            catch (Exception)
            {
                Console.WriteLine("Errore su file {0}", "MainEle");
                throw;
            }
        }
    }
}
