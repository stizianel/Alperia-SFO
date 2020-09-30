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
using Serilog;

namespace Isu_ver_fica
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("E:\\work\\Alperia\\log-fica.log", rollingInterval: RollingInterval.Minute)
            .CreateLogger();

            var readerFica = new StreamReader("E:\\work\\Alperia\\Docfica.csv");
            var csvFica = new CsvReader(readerFica, CultureInfo.InvariantCulture);
            csvFica.Configuration.Delimiter = ";";
            csvFica.Configuration.HeaderValidated = null;
            csvFica.Configuration.MissingFieldFound = null;
            var lfica = new List<Docfica>();
            lfica = ProcessFica(csvFica);
            foreach (var fica in lfica)
            {
                ValidationContext context = new ValidationContext(fica, null, null);
                List<ValidationResult> validationResults = new List<ValidationResult>();
                bool valid = Validator.TryValidateObject(fica, context, validationResults, true);
                if (!valid)
                {
                    foreach (ValidationResult validationResult in validationResults)
                    {
                        Console.WriteLine("{0}", validationResult.ErrorMessage);
                        Log.Logger.Error("{0}", validationResult.ErrorMessage);
                    }
                }
            }
            Console.ReadKey();
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
    }
}
