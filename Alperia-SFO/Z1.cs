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
        public string B2WExtCat__PropertyId__c { get; set; }
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
        [BsonId]
        public string NE__Order_Item__c { get; set; }
        public string HolderCompanyName__c { get; set; }
        public string ContactPartnerCode__c { get; set; }
        public string AnnualConsumptionSmcYear__c { get; set; }
        [Ignore]
        public string fileName { get; set; }
        [Ignore]
        public List<Z1param> Parameters { get; set; }

    }

    public class Z1params
    {
        [BsonId]
        public string NE__Order_Item__c { get; set; }
        public List<Z1param> Parameters { get; set; }

    }

    public class Z1param
    {
        public Z1param(string b2WExtCat__PropertyId__c, string nE__Value__c)
        {
            B2WExtCat__PropertyId__c = b2WExtCat__PropertyId__c;
            NE__Value__c = nE__Value__c;
        }

        public string B2WExtCat__PropertyId__c { get; set; }
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
            if (!paymentMethod.ContainsKey(i_pay))
            {
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
                Log.Error("UsageType KO {0} {1}", i_ust, i_order);
            }

        }

        public static void CheckUsageCategory(string i_ust, string i_order)
        {
            if (!usage_categories.ContainsKey(i_ust))
            {
                Log.Error("UsageCategory KO {0} {1}", i_ust, i_order);
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

        public static void CheckB2WU__Tariff_Type_Gas__c(string i_cha, string i_order)
        {
            if (i_cha.Length > 0)
            {
                if (!B2WU__Tariff_Type_Gas__c.ContainsKey(i_cha))
                {
                    Log.Error("Tariff type gas KO {0} {1}", i_cha, i_order);
                }
            }
        }

        public static void CheckB2WU__Tariff_Type_Power__c(string i_cha, string i_order)
        {
            if (i_cha.Length > 0)
            {
                if (!B2WU__Tariff_Type_Power__c.ContainsKey(i_cha))
                {
                    Log.Error("Tariff type power KO {0} {1}", i_cha, i_order);
                }
            }
        }

        public static void CheckEngine_Code_D__c(string i_cha, string i_order)
        {
            if (i_cha.Length > 0)
            {
                if (!Engine_Code_D__c.ContainsKey(i_cha))
                {
                    Log.Error("Engine Code KO {0} {1}", i_cha, i_order);
                }
            }
        }

        public static void CheckExciseEle__c(string i_cha, string i_order)
        {
            if (i_cha.Length > 0)
            {
                if (!ExciseEle__c.ContainsKey(i_cha))
                {
                    Log.Error("Excise ELE KO {0} {1}", i_cha, i_order);
                }
            }
        }

        public static void CheckExciseGas__c(string i_cha, string i_order)
        {
            if (i_cha.Length > 0)
            {
                if (!ExciseGas__c.ContainsKey(i_cha))
                {
                    Log.Error("Excise GAS KO {0} {1}", i_cha, i_order);
                }
            }
        }

        public static void CheckCountry__c(string i_cha, string i_order)
        {
            if (i_cha.Length > 0)
            {
                if (!Countries__c.ContainsKey(i_cha))
                {
                    Log.Error("Country KO {0} {1}", i_cha, i_order);
                }
            }
        }

        public static void CheckCounty__c(string i_cha, string i_order)
        {
            if (i_cha.Length > 0)
            {
                if (!Counties__c.ContainsKey(i_cha))
                {
                    Log.Error("Provincia KO {0} {1}", i_cha, i_order);
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
            ["SwitchIn"] = "CICCIO"
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
            ["CNDM"] = "C",
            ["CMVT"] = "D",
        };

        private static readonly Dictionary<string, string> usage_type = new Dictionary<string, string>
        {
            ["0"] = "domestic",
            ["1"] = "condominio",
            ["2"] = "altri usi",
            ["3"] = "servizio pubblico"
        };

        private static readonly Dictionary<string, string> usage_categories = new Dictionary<string, string>
        {
            ["C1"] = ".",
            ["C2"] = ".",
            ["C3"] = ".",
            ["C4"] = ".",
            ["C5"] = ".",
            ["T1"] = ".",
            ["T2"] = "."
        };

        private static readonly Dictionary<string, string> channel_type = new Dictionary<string, string>
        {
            ["PRTNR"] = "domestic",
            ["WEB00"] = "condominio",
            ["ENGYC"] = "altri usi",
            ["ENGYP"] = "servizio pubblico",
            ["CLCNT"] = "call center inbd",
        };

        private static readonly Dictionary<string, string> B2WU__Tariff_Type_Gas__c = new Dictionary<string, string>
        {
            ["Single-Part"] = "domestic",
            ["Two-part"] = "condominio",
        };

        private static readonly Dictionary<string, string> B2WU__Tariff_Type_Power__c = new Dictionary<string, string>
        {
            ["Hours"] = "domestic",
            ["2 Bands"] = "condominio",
            ["3 Bands"] = "altri usi",
            ["Peak/Offpeak"] = "servizio pubblico",
            ["Single Rate"] = "call center inbd",
        };

        private static readonly Dictionary<string, string> ExciseEle__c = new Dictionary<string, string>
        {
            ["ACCDOM"] = ".",
            ["ACCORD"] = ".",
            ["ESGROS"] = ".",
            ["ESNCOLL"] = ".",
            ["ESNDIPL"] = ".",
            ["ESNEST"] = ".",
            ["ESNEUE"] = ".",
            ["ESNFER"] = ".",
            ["ESNFORARM"] = ".",
            ["ESNINT"] = ".",
            ["ESNMIN"] = ".",
            ["ESNOPI"] = ".",
            ["ESNPRODE"] = ".",
            ["ESNPRODF"] = ".",
            ["ESNRID"] = ".",
            ["ESNRIN"] = ".",
            ["ESNTRASPU"] = ".",
            ["ESNUE"] = ".",
            ["ESRES"] = ".",
            ["ACCRES"] = "."
        };

        private static readonly Dictionary<string, string> ExciseGas__c = new Dictionary<string, string>
        {
            [""] = ".",
            ["AUTOPROD"] = ".",
            ["AUTOTRAZ"] = ".",
            ["CIV"] = ".",
            ["ESGGROS"] = ".",
            ["ESN22"] = ".",
            ["ESNALL"] = ".",
            ["ESNALTOF"] = ".",
            ["ESNCHIM"] = ".",
            ["ESNDIPL"] = ".",
            ["ESNELE"] = ".",
            ["ESNESPO"] = ".",
            ["ESNEUE"] = ".",
            ["ESNINPDIV"] = ".",
            ["ESNMAGN"] = ".",
            ["ESNMETAL"] = ".",
            ["ESNMINERAL"] = ".",
            ["ESNSOL"] = ".",
            ["ESNUE"] = ".",
            ["ESRES"] = ".",
            ["FORARM"] = ".",
            ["IND"] = ".",
            ["PRM"] = ".",
            ["PRODELE"] = ".",
            ["USICANT"] = "."
        };

        private static readonly Dictionary<string, string> Engine_Code_D__c = new Dictionary<string, string>
        {
            ["ASS00001"] = ".",
            ["ASS00002"] = ".",
            ["ASS00003"] = ".",
            ["ASS00004"] = ".",
            ["ASS00005"] = ".",
            ["ASS00039"] = ".",
            ["ASS00006"] = ".",
            ["ASS00040"] = ".",
            ["ASS00007"] = ".",
            ["ASS00008"] = ".",
            ["ASS00009"] = ".",
            ["ASS00010"] = ".",
            ["ASS00011"] = ".",
            ["ASS00012"] = ".",
            ["ASS00013"] = ".",
            ["ASS00014"] = ".",
            ["ASS00015"] = ".",
            ["ASS00016"] = ".",
            ["ASS00017"] = ".",
            ["ASS00057"] = ".",
            ["ASS00018"] = ".",
            ["ASS00020"] = ".",
            ["ASS00021"] = ".",
            ["ASS00022"] = ".",
            ["ASS00023"] = ".",
            ["ASS00026"] = ".",
            ["ASS00027"] = ".",
            ["ASS00028"] = ".",
            ["ASS00029"] = ".",
            ["ASS00030"] = ".",
            ["ASS00031"] = ".",
            ["ASS00032"] = ".",
            ["ASS00069"] = ".",
            ["ASS00068"] = ".",
            ["ASS00024"] = ".",
            ["ASS00025"] = ".",
            ["ASS00035"] = ".",
            ["ASS00036"] = ".",
            ["ASS00037"] = ".",
            ["ASS00038"] = ".",
            ["ASUM00001"] = ".",
            ["ASUM00002"] = ".",
            ["ASUM00003"] = ".",
            ["ASUM00004"] = ".",
            ["ASUM00005"] = ".",
            ["TUGG00001"] = ".",
            ["TUGG00002"] = ".",
            ["TUGG00003"] = ".",
            ["TUGG00004"] = "."
        };

        private static readonly Dictionary<string, string> Countries__c = new Dictionary<string, string>
        {
            ["Italia"] = ".",
            ["Jamaica"] = ".",
            ["Japan"] = ".",
            ["Jersey"] = ".",
            ["Jordan"] = ".",
            ["Kazakhstan"] = ".",
            ["Kenya"] = ".",
            ["Kiribati"] = ".",
            ["Korea, Democratic People's Republic of"] = ".",
            ["Korea, Republic of"] = ".",
            ["Kuwait"] = ".",
            ["Kyrgyzstan"] = ".",
            ["Lao People's Democratic Republic"] = ".",
            ["Latvia"] = ".",
            ["Lebanon"] = ".",
            ["Lesotho"] = ".",
            ["Liberia"] = ".",
            ["Libya"] = ".",
            ["Liechtenstein"] = ".",
            ["Lithuania"] = ".",
            ["Luxembourg"] = ".",
            ["Macao"] = ".",
            ["Macedonia, the Former Yugoslav Republic of"] = ".",
            ["Madagascar"] = ".",
            ["Malawi"] = ".",
            ["Malaysia"] = ".",
            ["Maldives"] = ".",
            ["Mali"] = ".",
            ["Malta"] = ".",
            ["Marshall Islands"] = ".",
            ["Martinique"] = ".",
            ["Mauritania"] = ".",
            ["Mauritius"] = ".",
            ["Mayotte"] = ".",
            ["Mexico"] = ".",
            ["Micronesia, Federated States of"] = ".",
            ["Moldova, Republic of"] = ".",
            ["Monaco"] = ".",
            ["Mongolia"] = ".",
            ["Montenegro"] = ".",
            ["Montserrat"] = ".",
            ["Morocco"] = ".",
            ["Mozambique"] = ".",
            ["Myanmar"] = ".",
            ["Namibia"] = ".",
            ["Nauru"] = ".",
            ["Nepal"] = ".",
            ["Netherlands"] = ".",
            ["New Caledonia"] = ".",
            ["New Zealand"] = ".",
            ["Nicaragua"] = ".",
            ["Niger"] = ".",
            ["Nigeria"] = ".",
            ["Niue"] = ".",
            ["Norfolk Island"] = ".",
            ["Northern Mariana Islands"] = ".",
            ["Norway"] = ".",
            ["Oman"] = ".",
            ["Pakistan"] = ".",
            ["Palau"] = ".",
            ["Palestine, State of"] = ".",
            ["Panama"] = ".",
            ["Papua New Guinea"] = ".",
            ["Paraguay"] = ".",
            ["Peru"] = ".",
            ["Philippines"] = ".",
            ["Pitcairn"] = ".",
            ["Poland"] = ".",
            ["Portugal"] = ".",
            ["Puerto Rico"] = ".",
            ["Qatar"] = ".",
            ["Romania"] = ".",
            ["Russian Federation"] = ".",
            ["Rwanda"] = ".",
            ["RÃ©union"] = ".",
            ["Saint Barthélemy"] = ".",
            ["Saint Helena, Ascension and Tristan da Cunha"] = ".",
            ["Saint Kitts and Nevis"] = ".",
            ["Saint Lucia"] = ".",
            ["Saint Martin (French part)"] = ".",
            ["Saint Pierre and Miquelon"] = ".",
            ["Saint Vincent and the Grenadines"] = ".",
            ["Samoa"] = ".",
            ["San Marino"] = ".",
            ["Sao Tome and Principe"] = ".",
            ["Saudi Arabia"] = ".",
            ["Senegal"] = ".",
            ["Serbia"] = ".",
            ["Seychelles"] = ".",
            ["Sierra Leone"] = ".",
            ["Singapore"] = ".",
            ["Sint Maarten (Dutch part)"] = ".",
            ["Slovakia"] = ".",
            ["Slovenia"] = ".",
            ["Solomon Islands"] = ".",
            ["Somalia"] = ".",
            ["South Africa"] = ".",
            ["South Georgia and the South Sandwich Islands"] = ".",
            ["South Sudan"] = ".",
            ["Spain"] = ".",
            ["Sri Lanka"] = ".",
            ["Sudan"] = ".",
            ["Suriname"] = ".",
            ["Svalbard and Jan Mayen"] = ".",
            ["Svizzera"] = ".",
            ["Swaziland"] = ".",
            ["Sweden"] = ".",
            ["Syrian Arab Republic"] = ".",
            ["Taiwan, Province of China"] = ".",
            ["Tajikistan"] = ".",
            ["Tanzania, United Republic of"] = ".",
            ["Thailand"] = ".",
            ["Timor-Leste"] = ".",
            ["Togo"] = ".",
            ["Tokelau"] = ".",
            ["Tonga"] = ".",
            ["Trinidad and Tobago"] = ".",
            ["Tunisia"] = ".",
            ["Turkey"] = ".",
            ["Turkmenistan"] = ".",
            ["Turks and Caicos Islands"] = ".",
            ["Tuvalu"] = ".",
            ["Uganda"] = ".",
            ["Ukraine"] = ".",
            ["United Arab Emirates"] = ".",
            ["United Kingdom"] = ".",
            ["United States"] = ".",
            ["United States Minor Outlying Islands"] = ".",
            ["Uruguay"] = ".",
            ["Uzbekistan"] = ".",
            ["Vanuatu"] = ".",
            ["Venezuela, Bolivarian Republic of"] = ".",
            ["Viet Nam"] = ".",
            ["Virgin Islands, British"] = ".",
            ["Virgin Islands, U.S."] = ".",
            ["Wallis and Futuna"] = ".",
            ["Western Sahara"] = ".",
            ["Yemen"] = ".",
            ["Zambia"] = ".",
            ["Zimbabwe"] = ".",
            ["Åland Islands"] = ".",
            ["Afghanistan"] = ".",
            ["Albania"] = ".",
            ["Algeria"] = ".",
            ["American Samoa"] = ".",
            ["Andorra"] = ".",
            ["Angola"] = ".",
            ["Anguilla"] = ".",
            ["Antarctica"] = ".",
            ["Antigua and Barbuda"] = ".",
            ["Argentina"] = ".",
            ["Armenia"] = ".",
            ["Aruba"] = ".",
            ["Australia"] = ".",
            ["Austria"] = ".",
            ["Azerbaijan"] = ".",
            ["Bahamas"] = ".",
            ["Bahrain"] = ".",
            ["Bangladesh"] = ".",
            ["Barbados"] = ".",
            ["Belarus"] = ".",
            ["Belgium"] = ".",
            ["Belize"] = ".",
            ["Benin"] = ".",
            ["Bermuda"] = ".",
            ["Bhutan"] = ".",
            ["Bolivia, Plurinational State of"] = ".",
            ["Bonaire, Sint Eustatius and Saba"] = ".",
            ["Bosnia and Herzegovina"] = ".",
            ["Botswana"] = ".",
            ["Bouvet Island"] = ".",
            ["Brazil"] = ".",
            ["British Indian Ocean Territory"] = ".",
            ["Brunei Darussalam"] = ".",
            ["Bulgaria"] = ".",
            ["Burkina Faso"] = ".",
            ["Burundi"] = ".",
            ["Cambodia"] = ".",
            ["Cameroon"] = ".",
            ["Canada"] = ".",
            ["Cape Verde"] = ".",
            ["Cayman Islands"] = ".",
            ["Central African Republic"] = ".",
            ["Chad"] = ".",
            ["Chile"] = ".",
            ["China"] = ".",
            ["Christmas Island"] = ".",
            ["Cocos (Keeling) Islands"] = ".",
            ["Colombia"] = ".",
            ["Comoros"] = ".",
            ["Congo"] = ".",
            ["Congo, the Democratic Republic of the"] = ".",
            ["Cook Islands"] = ".",
            ["Costa Rica"] = ".",
            ["Croatia"] = ".",
            ["Cuba"] = ".",
            ["CuraÃ§ao"] = ".",
            ["Cyprus"] = ".",
            ["Czech Republic"] = ".",
            ["CÃ´te d'Ivoire"] = ".",
            ["Denmark"] = ".",
            ["Djibouti"] = ".",
            ["Dominica"] = ".",
            ["Dominican Republic"] = ".",
            ["Ecuador"] = ".",
            ["Egypt"] = ".",
            ["El Salvador"] = ".",
            ["Equatorial Guinea"] = ".",
            ["Eritrea"] = ".",
            ["Estonia"] = ".",
            ["Ethiopia"] = ".",
            ["Falkland Islands (Malvinas)"] = ".",
            ["Faroe Islands"] = ".",
            ["Fiji"] = ".",
            ["Finland"] = ".",
            ["France"] = ".",
            ["French Guiana"] = ".",
            ["French Polynesia"] = ".",
            ["French Southern Territories"] = ".",
            ["Gabon"] = ".",
            ["Gambia"] = ".",
            ["Georgia"] = ".",
            ["Germany"] = ".",
            ["Ghana"] = ".",
            ["Gibraltar"] = ".",
            ["Greece"] = ".",
            ["Greenland"] = ".",
            ["Grenada"] = ".",
            ["Guadeloupe"] = ".",
            ["Guam"] = ".",
            ["Guatemala"] = ".",
            ["Guernsey"] = ".",
            ["Guinea"] = ".",
            ["Guinea-Bissau"] = ".",
            ["Guyana"] = ".",
            ["Haiti"] = ".",
            ["Heard Island and McDonald Islands"] = ".",
            ["Holy See (Vatican City State)"] = ".",
            ["Honduras"] = ".",
            ["Hong Kong"] = ".",
            ["Hungary"] = ".",
            ["Iceland"] = ".",
            ["India"] = ".",
            ["Indonesia"] = ".",
            ["Iran, Islamic Republic of"] = ".",
            ["Iraq"] = ".",
            ["Ireland"] = ".",
            ["Isle of Man"] = ".",
            ["Israel"] = "."
        };

        private static readonly Dictionary<string, string> Counties__c = new Dictionary<string, string>
        {
            ["AG"] = ".",
            ["AL"] = ".",
            ["AN"] = ".",
            ["AO"] = ".",
            ["AP"] = ".",
            ["AQ"] = ".",
            ["AR"] = ".",
            ["AT"] = ".",
            ["AV"] = ".",
            ["BA"] = ".",
            ["BG"] = ".",
            ["BI"] = ".",
            ["BL"] = ".",
            ["BN"] = ".",
            ["BO"] = ".",
            ["BR"] = ".",
            ["BS"] = ".",
            ["BT"] = ".",
            ["BZ"] = ".",
            ["CA"] = ".",
            ["CB"] = ".",
            ["CE"] = ".",
            ["CH"] = ".",
            ["CL"] = ".",
            ["CN"] = ".",
            ["CO"] = ".",
            ["CR"] = ".",
            ["CS"] = ".",
            ["CT"] = ".",
            ["CZ"] = ".",
            ["EE"] = ".",
            ["EN"] = ".",
            ["FC"] = ".",
            ["FE"] = ".",
            ["FG"] = ".",
            ["FI"] = ".",
            ["FM"] = ".",
            ["FR"] = ".",
            ["FU"] = ".",
            ["GE"] = ".",
            ["GO"] = ".",
            ["GR"] = ".",
            ["IM"] = ".",
            ["IS"] = ".",
            ["KR"] = ".",
            ["LC"] = ".",
            ["LE"] = ".",
            ["LI"] = ".",
            ["LO"] = ".",
            ["LT"] = ".",
            ["LU"] = ".",
            ["MB"] = ".",
            ["MC"] = ".",
            ["ME"] = ".",
            ["MI"] = ".",
            ["MN"] = ".",
            ["MO"] = ".",
            ["MS"] = ".",
            ["MT"] = ".",
            ["NA"] = ".",
            ["NO"] = ".",
            ["NU"] = ".",
            ["OR"] = ".",
            ["PA"] = ".",
            ["PC"] = ".",
            ["PD"] = ".",
            ["PE"] = ".",
            ["PG"] = ".",
            ["PI"] = ".",
            ["PL"] = ".",
            ["PN"] = ".",
            ["PO"] = ".",
            ["PR"] = ".",
            ["PT"] = ".",
            ["PU"] = ".",
            ["PV"] = ".",
            ["PZ"] = ".",
            ["RA"] = ".",
            ["RC"] = ".",
            ["RE"] = ".",
            ["RG"] = ".",
            ["RI"] = ".",
            ["RM"] = ".",
            ["RN"] = ".",
            ["RO"] = ".",
            ["SA"] = ".",
            ["SI"] = ".",
            ["SO"] = ".",
            ["SP"] = ".",
            ["SR"] = ".",
            ["SS"] = ".",
            ["SU"] = ".",
            ["SV"] = ".",
            ["TA"] = ".",
            ["TE"] = ".",
            ["TN"] = ".",
            ["TO"] = ".",
            ["TP"] = ".",
            ["TR"] = ".",
            ["TS"] = ".",
            ["TV"] = ".",
            ["UD"] = ".",
            ["VA"] = ".",
            ["VB"] = ".",
            ["VC"] = ".",
            ["VE"] = ".",
            ["VI"] = ".",
            ["VR"] = ".",
            ["VT"] = ".",
            ["VV"] = ".",
            ["ZA"] = ".",
            ["ZZ"] = "."
        };

    }

}
