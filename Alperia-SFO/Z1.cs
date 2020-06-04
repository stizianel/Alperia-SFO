using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alperia_SFO

    
{
    public class Z1
    {
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
        public string MyProperty { get; set; }
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
        public string Energia_Verde_Power_PMI__c { get; set; }
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
        public string NE_CityDE__c  { get; set; }
        public string NE_StreetDE__c { get; set; }
        public string BillingStreetDE__c { get; set; }
        public string BillingCityDE__c { get; set; }
        public string BillingCountryDE__c { get; set; }
        public string ShippingStreetNameDE__c { get; set; }
        public string ShippingCityDE__c { get; set; }
        public string ShippingCountryDE__c { get; set; }
        public string NE__OrderId__c { get; set; }
        public string ProfilingConsent__c { get; set; }
        public string Note__c  { get; set; }
        public string ZappKeyContract__c { get; set; }
        public string NE__Order_Item__c { get; set; }
        public string HolderFullName__c { get; set; }

        public static Z1 ParseFromCsv(string line)
        {
            var columns = line.Split(';');

            return new Z1
            {
                Usage_Category__c = columns[2],
                WithdrawalClass__c = columns[3],
                AnnualConsumption__c = columns[4],
                SAP_PaymentMethod__c = columns[5],

                NE__Order_Item__c = columns[90]
            };
        }
    }

    public class Z1Context
    {
        private readonly IMongoDatabase _db;

        public Z1Context()
        {
            MongoClient client = new MongoClient();
            _db = client.GetDatabase("Alperia");
            _db.GetCollection<Z1>("Z1");
        }

        public IMongoCollection<Z1> Z1s => _db.GetCollection<Z1>("Z1");
    }

    class Z1Test
    {
        readonly Dictionary<string, string> mydic = new Dictionary<string, string>
        {
            ["BNK"] = "BON",
            ["CCD"] = "CICCIO",
            ["MAV"] = "MAV",
            ["PST"] = "PST",
            ["RAV"] = "RAV",
            ["SDD"] = "SDD"
        };
        
    }
}
