using Alperia_ISU_Lib;
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

namespace IsuVerMainConf
{
    class Program
    {
        static void Main(string[] args)
        {
            var dt_cutoff = args[0];
            var dt_ab = args[1];

            Log.Logger = new LoggerConfiguration()
           .MinimumLevel.Debug()
           .WriteTo.Console()
           .WriteTo.File("E:\\work\\Alperia\\PRD\\log-MainConf.log", rollingInterval: RollingInterval.Minute)
           .CreateLogger();

            var readerGas = new StreamReader("E:\\work\\Alperia\\PRD\\100_20201022_CONFCOMM_ELE.csv");
            var csvGas = new CsvReader(readerGas, CultureInfo.InvariantCulture);
            csvGas.Configuration.Delimiter = ";";

            var r_compVend = new StreamReader("E:\\work\\Alperia\\PRD\\Alperia_componenti_di_vendita.csv");
            var csvCompVend = new CsvReader(r_compVend, CultureInfo.InvariantCulture);
            csvCompVend.Configuration.Delimiter = ";";

            List<CompVend> lCompVend = ProcessCompVend(csvCompVend);
            List<string> lovComp = lCompVend.Select(x => x.COD_COMPONENTE).Distinct().ToList();

            var lConfComm = new List<ConfEle>();
            Log.Logger.Information("Inizio LOG");
            lConfComm = ProcessConf(csvGas);
            var lpdr = lConfComm.Select(x => x.EXT_UI).Distinct().ToList();
            foreach (var extui in lpdr)
            {
                var conf = lConfComm.Where(x => x.EXT_UI == extui).ToList();
                var conf_comps = conf.Select(x => x.COD_COMPONENTE).ToList();
                var res_comp = conf_comps.Intersect(lovComp);
                if (res_comp == null)
                {
                    Log.Logger.Error($"Manca componente di vendita {extui}");
                }
            }
            foreach (var rec in lConfComm)
            {
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
                ConfValidator validator = new ConfValidator(dt_ab, lCompVend);
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
            Log.Logger.Information("Fine Log");
            Console.ReadKey();
        }

        private static List<ConfEle> ProcessConf(CsvReader csvGas)
        {
            try
            {
                var zGas = csvGas.GetRecords<ConfEle>().ToList();
                return zGas;
            }
            catch (Exception)
            {
                Console.WriteLine("Errore su file {0}", "MainConf");
                throw;
            }
        }

        private static List<CompVend> ProcessCompVend(CsvReader csv)
        {
            try
            {
                var zCompVend = csv.GetRecords<CompVend>().ToList();
                return zCompVend;
            }
            catch (Exception)
            {
                Console.WriteLine("Errore su file {0}", "CompVend");
                throw;
            }
        }
    }
}
