using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CsvHelper;
using System.Globalization;
using MongoDB.Bson;
using Serilog;

namespace Alperia_SFO
{
    
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("E:\\work\\Alperia\\my_log.log", rollingInterval: RollingInterval.Day)
            .CreateLogger();

            OpenFileDialog ofd = new OpenFileDialog();
            ofd.ShowDialog();
            var fileInput = ofd.FileName;
            Log.Information("The global logger has been configured");
            Log.Information("Reading z1.csv");
            
            var ctx = new Z1Context();
            var fileOutAlperia = "E:\\work\\Alperia\\outAlperia.csv";
            var fileOutSum = "E:\\work\\Alperia\\outSum.csv"; ;

            using (var reader = new StreamReader(fileInput))
            using (var walperia = new StreamWriter(fileOutAlperia))
            using (var wsum = new StreamWriter(fileOutSum))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            using (var alpCsv = new CsvWriter(walperia, CultureInfo.InvariantCulture))
            using (var sumCsv = new CsvWriter(wsum, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = ";";
                csv.Configuration.HeaderValidated = null;
                csv.Configuration.MissingFieldFound = null;
                alpCsv.Configuration.Delimiter = ";";
                sumCsv.Configuration.Delimiter = ";";
                try
                {
                    var records = csv.GetRecords<Z1>();

                    var lZ1 = records.ToList();

                    var elenco = lZ1.Select(p => p.NE__Order_Item__c);
                    var toProc = elenco.Distinct();
                    

                    foreach (var k in toProc)
                    {
                        List<bool> lerrori = new List<bool>();
                        var rec = lZ1.Where(p => p.NE__Order_Item__c == k).First();
                        bool status = true;
                        status = Z1Test.CheckCommodityType(rec.Commodity_Type__c, rec.NE__Order_Item__c);
                        lerrori.Add(status);
                        if (!status)
                        {
                            continue;
                        }
                        status = Z1Test.CheckPaymentMethod(rec.SAP_PaymentMethod__c, rec.NE__Order_Item__c );
                        lerrori.Add(status);
                        status = Z1Test.CheckProcessType(rec.Process__c, rec.NE__Order_Item__c);
                        lerrori.Add(status);
                        status = Z1Test.CheckHoldingType(rec.HoldingType__c, rec.NE__Order_Item__c);
                        lerrori.Add(status);
                        status = Z1Test.CheckSubjectSubtype(rec.SubjectSubtype__c, rec.NE__Order_Item__c);
                        lerrori.Add(status);
                        if (rec.Process__c != "ChangeOffer") {
                            Z1Test.CheckUsageType(rec.UsageType__c, rec.NE__Order_Item__c);
                            status = Z1Test.CheckUsageCategory(rec.Usage_Category__c, rec.NE__Order_Item__c);
                            lerrori.Add(status);
                            Z1Test.CheckTargetMarket(rec.TargetMarket__c, rec.NE__Order_Item__c);
                            short number;
                            Int16.TryParse(rec.VatRate__c, out number);
                            status = Z1Test.CheckVatCode(number, rec.NE__Order_Item__c);
                            lerrori.Add(status);
                            status = Z1Test.CheckAccountCustomerType(rec.AccountCustomerType__c, rec.NE__Order_Item__c);
                            lerrori.Add(status);
                        }
                        status = Z1Test.CheckChannelType(rec.Channel__c, rec.NE__Order_Item__c);
                        lerrori.Add(status);
                        status = Z1Test.CheckEngine_Code_D__c(rec.Engine_Code_D__c, rec.NE__Order_Item__c);
                        lerrori.Add(status);
                        Z1Test.CheckCountry__c(rec.BillingCountry__c, rec.NE__Order_Item__c);
                        Z1Test.CheckCounty__c(rec.BillingProvince__c, rec.NE__Order_Item__c);
                        if (rec.Commodity_Type__c == "ELE")
                        {
                            Z1Test.CheckExciseEle__c(rec.ExciseEle__c, rec.NE__Order_Item__c);
                            Z1Test.CheckB2WU__Tariff_Type_Power__c(rec.B2WU__Tariff_Type_Power__c, rec.NE__Order_Item__c);
                        }
                        if (rec.Commodity_Type__c == "GAS")
                        {
                            Z1Test.CheckB2WU__Tariff_Type_Gas__c(rec.B2WU__Tariff_Type_Gas__c, rec.NE__Order_Item__c);
                            Z1Test.CheckWitdrawal__c(rec.WithdrawalClass__c, rec.NE__Order_Item__c);
                            Z1Test.CheckExciseGas__c(rec.ExciseGas__c, rec.NE__Order_Item__c);
                        };

                        //var brec = rec.ToBson();
                        var w_params = lZ1.Where(p => p.NE__Order_Item__c == k).Select(f => new Z1param(f.B2WExtCat__PropertyId__c, f.NE__Value__c)).ToList();
                        rec.Parameters = w_params;
                        if (!lerrori.Contains(false))
                        {
                            InsMongoSingle(rec, ctx, fileInput);
                            var recs = lZ1.Where(p => p.NE__Order_Item__c == k);
                            if (rec.Division__c == "ASMS")
                            {                               
                                alpCsv.WriteRecords<Z1>(recs);
                            } else
                            {
                                sumCsv.WriteRecord<Z1>(rec);
                            }
                            
                        }
                    }
                }
                catch (Exception)
                {
                    throw;
                };
            }

            Log.Information("Fine programma");
            Console.WriteLine("Fine programma");
            Console.ReadKey();
        }
        private static void InsMongoSingle(Z1 doc, Z1Context ctx, string fname)
        {
            try
            {
                doc.fileName = fname;
                ctx.Z1s.InsertOne(doc);
            }
            catch (Exception)
            {

                Log.Information("record duplicato {0}", doc.NE__OrderId__c);
            }
            
        }   
        //private static List<Z1> ProcessFileInput(string path)
        //{
        //    //return
        //    //File.ReadAllLines(path)
        //    //    .Skip(1)
        //    //    .Where(line => line.Length > 1)
        //    //    .Select(Z1.ParseFromCsv)
        //    //    .ToList();
        //}
    }


}
