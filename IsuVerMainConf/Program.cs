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

            var readerGas = new StreamReader("E:\\work\\Alperia\\PRD\\100_20201019_CONFCOMM_GAS.csv");
            var csvGas = new CsvReader(readerGas, CultureInfo.InvariantCulture);
            csvGas.Configuration.Delimiter = ";";

            var lGas = new List<MainConf>();
            Log.Logger.Information("Inizio LOG");
            lGas = ProcessConf(csvGas);
            foreach (var rec in lGas)
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
                ConfValidator validator = new ConfValidator(dt_ab);
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

        private static List<MainConf> ProcessConf(CsvReader csvGas)
        {
            try
            {
                var zGas = csvGas.GetRecords<MainConf>().ToList();
                return zGas;
            }
            catch (Exception)
            {
                Console.WriteLine("Errore su file {0}", "MainConf");
                throw;
            }
        }
    }
}
