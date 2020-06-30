using CsvHelper.Configuration.Attributes;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alperia_SFO

    
{
    public class Z1
    {
        [BsonIgnore]
        public string Name { get; set; }
        [BsonIgnore]
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
        [BsonId]
        public string NE__Order_Item__c { get; set; }
        public string HolderCompanyName__c { get; set; }
        public string ContactPartnerCode__c { get; set; }
        public string AnnualConsumptionSmcYear__c { get; set; }
        [Ignore]
        public string fileName { get; set; }

    }

    public class Z1params
    {
        [BsonId]
        public string NE__Order_Item__c { get; set; }
        public List<Z1param> Parameters { get; set; }

    }

    public class Z1param
    {
        public string Name { get; set; }
        public string NE__Value__c { get; set; }
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

    public class Z1Test
    {
        public static void CheckPaymentMethod(string i_pay, string i_order)
        {
                if (!paymentMethod.ContainsKey(i_pay) ) {
                       Console.WriteLine("Pay KO {0} {1}", i_pay, i_order);
               }

        }
        public static void CheckVatCode(string i_vat, string i_order)
        {
            if (!vatCode.ContainsKey(i_vat))
            {
                Log.Error("Vat KO {0} {1}", i_vat, i_order);
            }

        }
        public static void CheckProcessType(string i_proc, string i_order)
        {
            if (!process_type.ContainsKey(i_proc))
            {
                Log.Error("Process_Type KO {0} {1}", i_proc, i_order);
            }

        }
        public static void CheckTargetMarket(string i_tma, string i_order)
        {
            if (!target_market.ContainsKey(i_tma))
            {
                Log.Error("TargetMarket KO {0} {1}", i_tma, i_order);
            }

        }
        public static void CheckAccountCustomerType(string i_act, string i_order)
        {
            if (!account_customer_type.ContainsKey(i_act))
            {
                Log.Error("AccountCustomerType KO {0} {1}", i_act, i_order);
            }

        }

        public static void CheckHoldingType(string i_hot, string i_order)
        {
            if (!holding_type.ContainsKey(i_hot))
            {
                Log.Error("HoldingType KO {0} {1}", i_hot, i_order);
            }

        }

        public static void CheckUsageType(string i_ust, string i_order)
        {
            if (!usage_type.ContainsKey(i_ust))
            {
                Log.Error("UsageType KO {0} {1}",i_ust, i_order);
            }

        }

        public static void CheckSubjectSubtype(string i_sst, string i_order)
        {
            if (i_sst.Length > 0)
            {
                if (!subject_subtype.ContainsKey(i_sst))
                {
                    Log.Error("SubjectSubtype KO {0} {1}", i_sst, i_order);
                }
            }
            

        }

        public static void CheckChannelType(string i_cha, string i_order)
        {
            if (i_cha.Length > 0)
            {
                if (!channel_type.ContainsKey(i_cha))
                {
                    Log.Error("Channel type KO {0} {1}", i_cha, i_order);
                }
            }
        }

        private static readonly Dictionary<string, string> paymentMethod = new Dictionary<string, string>
        {
           ["BNK"] = "BON",
           ["CCD"] = "CICCIO",
           ["MAV"] = "MAV",
           ["PST"] = "PST",
           ["RAV"] = "RAV",
           ["SDD"] = "SDD"
        };

        private static readonly Dictionary<string, string> vatCode = new Dictionary<string, string>
        {
            ["1B"] = "BON",
            ["2A"] = "CICCIO",
            ["2B"] = "MAV",
            ["3A"] = "PST",
            ["3B"] = "RAV",
            ["5B"] = "SDD",
            ["5C"] = "SDD",
            ["6B"] = "SDD",
            ["9A"] = "SDD"
        };

        private static readonly Dictionary<string, string> process_type = new Dictionary<string, string>
        {
            ["ChangeOffer"] = "BON",
            ["SwitchIn"] = "CICCIO",
            ["Renewal"] = "CICCIO",
            ["SimpleTransfer"] = "CICCIO",
            ["Takeover"] = "CICCIO"
        };

        private static readonly Dictionary<string, string> target_market = new Dictionary<string, string>
        {
            ["L"] = "L",
            ["T"] = "T",
            ["S"] = "S"
        };

        private static readonly Dictionary<string, string> holding_type = new Dictionary<string, string>
        {
            ["FOCT001"] = "L",
            ["FOCT002"] = "T",
            ["FOCT004"] = "S",
            ["FOCT005"] = "S",
            ["FOCT006"] = "S"
        };

        private static readonly Dictionary<string, string> account_customer_type = new Dictionary<string, string>
        {
            ["RSDN"] = "L",
            ["SMEN"] = "T",
            ["CNDM"] = "S",
            ["ASSC"] = "S"
        };

        private static readonly Dictionary<string, string> subject_subtype = new Dictionary<string, string>
        {
            ["LBPF"] = "L",
            ["DTIN"] = "T",
            ["PSGR"] = "S",
            ["CNDM"] = "C"
        };

        private static readonly Dictionary<string, string> usage_type = new Dictionary<string, string>
        {
            ["0"] = "domestic",
            ["1"] = "condominio",
            ["2"] = "altri usi",
            ["3"] = "servizio pubblico"
        };

        private static readonly Dictionary<string, string> channel_type = new Dictionary<string, string>
        {
            ["PRTNR"] = "domestic",
            ["WEB00"] = "condominio",
            ["SPRTL"] = "altri usi",
            ["ECORN"] = "servizio pubblico",
            ["CLCNT"] = "call center inbd",
        };
    }

}
