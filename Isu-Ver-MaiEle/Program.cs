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
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("E:\\work\\Alperia\\log-MainEle.log", rollingInterval: RollingInterval.Hour)
            .CreateLogger();

            var readerEle = new StreamReader("E:\\work\\Alperia\\QUA\\100_20200930_MAIN_ELE.csv");
            var csvEle = new CsvReader(readerEle, CultureInfo.InvariantCulture);
            csvEle.Configuration.Delimiter = ";";
            var lEle = new List<MainEle>();
            lEle = ProcessEle(csvEle);
            foreach (var rec in lEle)
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
                EleValidator validator = new EleValidator();
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
