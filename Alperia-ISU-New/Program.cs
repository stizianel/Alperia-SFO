using CsvHelper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Alperia_ISU_New
{
    class Program
    {
        private static readonly string targetDirectory = "E:\\work\\Alperia\\Migrazione IS-U\\";

         [STAThread]
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("E:\\work\\Alperia\\log-ISU.log", rollingInterval: RollingInterval.Minute)
            .CreateLogger();

            string[] fileEntries = Directory.GetFiles(targetDirectory);

            foreach (string fileName in fileEntries)
            {
                var reader = new StreamReader(fileName);
                var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                csv.Configuration.Delimiter = ";";

                var realName = fileName.Split('_')[2];
                if (realName == "ACCOUNT")
                {
                    var laccounts = ProcessAccounts(csv);
                }
                else if (realName == "ASSET")
                {
                    var lassetts = ProcessAssetts(csv);
                }
                else if (realName == "BILLING")
                {
                    var lbilling = ProcessBillings(csv);
                }
                
            }
            
            Log.Information("--------------------------------------");
            Console.WriteLine("Fine programma");
            Console.ReadKey();
            }

        private static List<Account> ProcessAccounts(CsvReader csv)
        {
            try
            {
                var accounts = csv.GetRecords<Account>();
                var laccounts = accounts.ToList();
                return laccounts;
            }
            catch (Exception)
            {
                Console.WriteLine("Errore su file {0}", "Account");
                throw;
            }
        }

        private static List<Assett> ProcessAssetts(CsvReader csv)
        {
            try
            {
                var assetts = csv.GetRecords<Assett>();
                var lassetts = assetts.ToList();
                return lassetts;
            }
            catch (Exception)
            {
                Console.WriteLine("Errore su file {0}", "Assett");
                throw;
            }
        }

        private static List<BillingProfile> ProcessBillings(CsvReader csv)
        {
            try
            {
                var billings = csv.GetRecords<BillingProfile>();
                var lbillings= billings.ToList();
                return lbillings;
            }
            catch (Exception)
            {
                Console.WriteLine("Errore su file {0}", "Billing Profile");
                throw;
            }
        }
    }
    }
