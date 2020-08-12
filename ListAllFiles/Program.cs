using CsvHelper;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using CsvHelper.Configuration.Attributes;

namespace ListAllFiles
{
    class Program
    {
        private static readonly string targetDirectory = "E:\\work\\Alperia\\filerich\\";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            List<Z1> recsTotal = new List<Z1>();
            List<Z1> recs = new List<Z1>();

            var fileOutAlperia = "E:\\work\\Alperia\\listRequests.csv";
            var fileUat = "E:\\work\\Alperia\\podUat.csv";

            using (var walperia = new StreamWriter(fileOutAlperia))
            using (var reqsCsv = new CsvWriter(walperia, CultureInfo.InvariantCulture))
            using (var rfileUat = new StreamReader(fileUat)) 
            using (var podUat = new CsvReader(rfileUat, CultureInfo.InvariantCulture))
            {
                reqsCsv.Configuration.Delimiter = ";";
                podUat.Configuration.Delimiter = ";";

                var recUat = podUat.GetRecords<Uat>();
                var lUat = recUat.ToList();

                string[] fileEntries = Directory.GetFiles(targetDirectory);
                foreach (string fileName in fileEntries)
                {
                    recs = ProcessFile(fileName, lUat);
                    reqsCsv.WriteRecords<Z1>(recs);
                }
            }
            Console.WriteLine("Programma terminato");
            Console.ReadKey();

        }

        private static List<Z1> ProcessFile(string fileName, List<Uat> lUat)
        {
            List<Z1> outrec = new List<Z1>();
            using (var reader = new StreamReader(fileName))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                csv.Configuration.Delimiter = ";";
                csv.Configuration.HeaderValidated = null;
                csv.Configuration.MissingFieldFound = null;
                //csv.Configuration.BadDataFound = null;
                try
                {
                    var records = csv.GetRecords<Z1>();

                    var lZ1 = records.ToList();

                    var elenco = lZ1.Select(p => p.NE__Order_Item__c);
                    var toProc = elenco.Distinct();

                    foreach (var k in toProc)
                    {
                        var rec = lZ1.Where(p => p.NE__Order_Item__c == k).First();
                        rec.fileName = fileName;
                        var res = lUat.Find(p => p.Pod_pdr == rec.PodPdrPdc__c);
                        if (res != null)
                        {
                            rec.Scenario = res.Scenario;
                        }
                        outrec.Add(rec);
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                return outrec;
            }
        }
    }
    public class Z1
    {
        [Ignore]
        public string B2WExtCat__PropertyId__c { get; set; }
        [Ignore]
        public string NE__Value__c { get; set; }
        public string Usage_Category__c { get; set; }
        public string WithdrawalClass__c { get; set; }
        public string AnnualConsumption__c { get; set; }
        public string SAP_PaymentMethod__c { get; set; }
        public string Division__c { get; set; }
        public string Channel__c { get; set; }
        public string RequestReceptionDate__c { get; set; }
        public string Commodity_Type__c { get; set; }
        public string CreatedDate { get; set; }
        public string AtecoCode__c { get; set; }
        public string VatRate__c { get; set; }
        public string PodPdrPdc__c { get; set; }
        public string BusinessStatus__c { get; set; }
        public string NE__StartDate__c { get; set; }
        public string AssetStreet__c { get; set; }
        public string AssetStreetNumber__c { get; set; }
        public string AssetZipCode__c { get; set; }
        public string AssetCity__c { get; set; }
        public string AssetProvince__c { get; set; }
        public string AssetCountry__c { get; set; }
        public string NE__Activation_Date__c { get; set; }
        public string UsageType__c { get; set; }
        public string Process__c { get; set; }
        public string TargetMarket__c { get; set; }
        public string AccountCustomerType__c { get; set; }
        public string InvoiceJointType__c { get; set; }
        public string LastName__c { get; set; }
        public string FirstName__c { get; set; }
        public string CompanyName__c { get; set; }
        public string FiscalCode__c { get; set; }
        public string VAT__c { get; set; }
        public string Email__c { get; set; }
        public string Phone__c { get; set; }
        public string PreferredLanguage__c { get; set; }
        public string SAP_AccountLegalForm__c { get; set; }
        public string HoldingType__c { get; set; }
        public string isResidentAtSupply__c { get; set; }
        public string BillingCountry__c { get; set; }
        public string BillingProvince__c { get; set; }
        public string BillingStreetName__c { get; set; }
        public string BillingStreetNumber__c { get; set; }
        public string BillingFlatNumber__c { get; set; }
        public string BillingCity__c { get; set; }
        public string BillingPostalCode__c { get; set; }
        public string ShippingShipTo__c { get; set; }
        public string ShippingCountry__c { get; set; }
        public string ShippingProvince__c { get; set; }
        public string ShippingStreetName__c { get; set; }
        public string ShippingStreetNumber__c { get; set; }
        public string ShippingFlatNumber__c { get; set; }
        public string ShippingCity__c { get; set; }
        public string ShippingPostalCode__c { get; set; }
        public string HolderLastName__c { get; set; }
        public string HolderFirstName__c { get; set; }
        public string HolderFiscalCode__c { get; set; }
        public string NE__Iban__c { get; set; }
        public string ContractType__c { get; set; }
        public string Engine_Code_D__c { get; set; }
        public string L1__c { get; set; }
        public string L2__c { get; set; }
        public string L3__c { get; set; }
        public string B2WU__Tariff_Type_Power__c { get; set; }
        public string B2WU__Tariff_Type_Gas__c { get; set; }
        public string ConsumptionSizeDiscount__c { get; set; }
        public string Regime__c { get; set; }
        public string CodeSDI__c { get; set; }
        public string BillingPEC__c { get; set; }
        public string ExciseEle__c { get; set; }
        public string ExciseGas__c { get; set; }
        public string SubjectSubtype__c { get; set; }
        public string SddDuty__c { get; set; }
        public string Source__c { get; set; }
        public string Origin__c { get; set; }
        public string Branch__c { get; set; }
        public string ZappKeyBusinessPartner__c { get; set; }
        public string ZappKeyContractAccount__c { get; set; }
        public string ZappKeyPlant__c { get; set; }
        public string WebRequestCode__c { get; set; }
        public string NE_CountryDE__c { get; set; }
        public string NE_CityDE__c { get; set; }
        public string NE_StreetDE__c { get; set; }
        public string BillingStreetDE__c { get; set; }
        public string BillingCityDE__c { get; set; }
        public string BillingCountryDE__c { get; set; }
        public string ShippingStreetNameDE__c { get; set; }
        public string ShippingCityDE__c { get; set; }
        public string ShippingCountryDE__c { get; set; }
        public string NE__OrderId__c { get; set; }
        public string ProfilingConsent__c { get; set; }
        public string Note__c { get; set; }
        public string ZappKeyContract__c { get; set; }
        public string NE__Order_Item__c { get; set; }
        public string HolderCompanyName__c { get; set; }
        public string ContactPartnerCode__c { get; set; }
        public string AnnualConsumptionSmcYear__c { get; set; }
        public string ZappKeyDirectDebit__c { get; set; }
        public string fileName { get; set; }
        public string Scenario { get; set; }
    }

    class Uat
    {
        public string Scenario { get; set; }
        public string Pod_pdr { get; set; }
    }
}
