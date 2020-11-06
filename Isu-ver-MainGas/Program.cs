using CsvHelper;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Isu_ver_MainGas
{
    class Program
    {
        static void Main(string[] args)
        {

            var dt_cutoff = args[0];

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("E:\\work\\Alperia\\PRD\\log-MainGas.log", rollingInterval: RollingInterval.Minute)
            //.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
            //{
            //    AutoRegisterTemplate = true,
            //})
            .CreateLogger();

            var fValid = new StreamReader("E:\\work\\Alperia\\PRD\\validaz-semplici.CSV");
            var csvValid = new CsvReader(fValid, CultureInfo.InvariantCulture);
            csvValid.Configuration.Delimiter = ";";
            List<ValidSemplici> lvalid = ValidSemplici.LoadValidSemplici(csvValid);

            var readerGas = new StreamReader("E:\\work\\Alperia\\PRD\\100_20201106_MAIN_GAS.csv");
            var csvGas = new CsvReader(readerGas, CultureInfo.InvariantCulture);
            csvGas.Configuration.Delimiter = ";";
            csvGas.Configuration.BadDataFound = null;

            var wrBps = new StreamWriter("E:\\work\\Alperia\\PRD\\BpGas.txt");

            var lGas = new List<MainGas>();

            Log.Logger.Information("Inizio LOG");
            

            lGas = ProcessGas(csvGas);
            var lGasTemp = lGas.Skip(2000).Take(100);

            //Log.Logger.Information("Inizio Estrazione elenco BP");
            //List<string> lBps = lGasTemp.Select(p => p.BPEXT).Distinct().ToList();
            //foreach (var item in lBps)
            //{
            //    var Bpcode = item.Split('_')[1];
            //    wrBps.WriteLine(Bpcode);
            //}
            //wrBps.Close();
            //Log.Logger.Information("Fine Estrazione elenco BP");

            foreach (var rec in lGasTemp)
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
                GasValidator validator = new GasValidator(lvalid, dt_cutoff);
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

        private static List<MainGas> ProcessGas(CsvReader csvGas)
        {
            try
            {
                var zGas = csvGas.GetRecords<MainGas>().ToList();
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
