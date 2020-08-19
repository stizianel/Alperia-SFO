using CsvHelper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;

namespace Alperia_ISU_New
{
    class Program
    {
        private static readonly string targetDirectory = "E:\\work\\Alperia\\Migrazione IS-U\\dati\\";

         [STAThread]
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .WriteTo.Console()
            .WriteTo.File("E:\\work\\Alperia\\log-ISU.log", rollingInterval: RollingInterval.Minute)
            .CreateLogger();

            var writer = new StreamWriter("E:\\work\\Alperia\\MainEle.csv");
            var csvOut = new CsvWriter(writer, CultureInfo.InvariantCulture);
            csvOut.Configuration.Delimiter = ";";

            string[] fileEntries = Directory.GetFiles(targetDirectory);
            var laccounts = new List<Account>();
            var lassets = new List<Assett>();
            var lbillings = new List<BillingProfile>();
            var lservice = new List<ServicePoint>();
            var ldirect = new List<DirectDebit>();
            List<MainEle> lmainele = new List<MainEle>();

            var ctx = new MainEleContext();

            foreach (var fileName in fileEntries)
            {
                var reader = new StreamReader(fileName);
                var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
                csv.Configuration.Delimiter = ";";

                var realName = fileName.Split('_')[2];
                switch (realName)
                {
                    case "ACCOUNT":
                        laccounts = ProcessAccounts(csv);
                        break;
                    case "ASSET":
                        lassets = ProcessAssets(csv);
                        break;
                    case "BILLING":
                    {
                        lbillings = ProcessBillings(csv);
                        break;
                    }
                    case "SERVICE":
                    {
                        lservice = ProcessServices(csv);
                        break;
                    }
                    case "DIRECT":
                    {
                         ldirect = ProcessDebits(csv);
                        break;
                    }
                }

            }

            foreach (var asset in lassets)
            {
                var wMainele = new MainEle();
                var account = laccounts.Where(p => p.ZAPPKEYBUSINESSPARTNER__C == asset.ACCOUNTID).First();
                var bilpro = lbillings.Where(p => p.ZAPPKEYCONTRACTACCOUNT__C == asset.NE__BILLINGPROF__C).First();
                var service = lservice.Where(p => p.ZAPPKEYPLANT__C == asset.NE__SERVICE_POINT__C).First();
                var debit = ldirect.Where(p => p.IBAN__C == bilpro.NE__IBAN__C).FirstOrDefault<DirectDebit>();
                wMainele.ZMIGBP             = "X";
                wMainele.ZMIGCC             = "X";
                wMainele.BU_TYPE            = account.CUSTOMERTYPE__C;
                wMainele.NAME_FIRST         = account.FIRSTNAME;
                wMainele.NAME_LAST          = account.LASTNAME;
                wMainele.NAME_ORG1          = account.NAME;
                wMainele.LEGAL_ENTY         = account.Z_ACC_LEGAL_FORM__C;
                wMainele.CITY1_ESAZ         = bilpro.SHIPPINGCITY__C;
                wMainele.COUNTRY_ESAZ       = decode_country(bilpro.SHIPPINGCOUNTRY__C);
                wMainele.HOUSE_NUM1_ESAZ    = bilpro.SHIPPINGSTREETNUMBER__C;
                wMainele.NAME_CO_ESAZ       = bilpro.SHIPPINGSHIPTO__C;
                wMainele.POST_CODE1_ESAZ    = bilpro.SHIPPINGPOSTALCODE__C.ToString();
                wMainele.REGION_ESAZ        = bilpro.SHIPPINGPROVINCE__C;
                wMainele.STREET_ESAZ        = bilpro.SHIPPINGSTREETNAME__C;
                wMainele.CITY1_CLIENTE      = account.BILLINGCITY;
                wMainele.COUNTRY_CLIENTE    = decode_country(account.BILLINGCOUNTRY);
                wMainele.HOUSE_NUM1_CLIENTE = account.BILLINGSTREETNUMBER__C;
                wMainele.POST_CODE1_CLIENTE = account.BILLINGPOSTALCODE.ToString();
                wMainele.REGION_CLIENTE     = account.BILLINGSTATE;
                wMainele.STREET_CLIENTE     = account.BILLINGSTREET;
                wMainele.ZBP_ISTATLOC_CLIENTE = account.BILLINGISTAT__C.ToString();
                wMainele.BPEXT              = account.ZAPPKEYBUSINESSPARTNER__C;
                wMainele.AUGRP              = "12"; 
                wMainele.BPKIND             = wMainele.Decode_bpkind(account.CUSTOMERTYPE__C);
                wMainele.CF_TAXNUM          = account.NE__FISCAL_CODE__C;
                wMainele.PI_TAXNUM          = account.NE__VAT__C.ToString();
                wMainele.ZPUBB              = "Y";
                wMainele.Z_MERCRIE          = "MT";
                wMainele.TEL_NUMBER1 = account.PHONE.ToString();
                wMainele.FAX_NUMBER = account.FAX;
                wMainele.SMTP_ADDR = account.NE__E_MAIL__C;
                wMainele.PEC_SMTP_ADDR = account.Z_ACC_PEC__C;
                wMainele.TEL_MOBILE = account.PERSONMOBILEPHONE;

                wMainele.BANKS = Val_dati_banks(bilpro.NE__IBAN__C);
                wMainele.BANKL = Val_dati_bankl(bilpro.NE__IBAN__C);
                wMainele.BANKN = Val_dati_bankn(bilpro.NE__IBAN__C);
                wMainele.BKONT = Val_dati_bkont(bilpro.NE__IBAN__C);
                wMainele.IBAN =  bilpro.NE__IBAN__C;

                wMainele.EZAWE = "P";

                if (debit != null)
                {
                    wMainele.MAND_SEPA = debit.ZAPPKEYDIRECTDEBIT__C;
                    wMainele.STAT_MAND = "1";
                    wMainele.COD_CUCSIA = debit.CUCSIA__C;
                    wMainele.CREDITORID = debit.CUSTOMERCODE__C;
                    wMainele.DATASOTT = debit.STARTDATE__C.ToString();
                    wMainele.B2B = "X";
                    wMainele.CODFISCFIRMA = debit.HOLDERVATFISCALCODE__C;
                    wMainele.NOMEFIRMA = debit.HOLDERFIRSTNAME__C;
                    wMainele.COGNOMEFIRMA = debit.HOLDERLASTNAME__C;
                    wMainele.AB_MANDATO = debit.STARTDATE__C.ToString();
                    wMainele.BIS_MANDATO = debit.ENDDATE__C.ToString();
                    wMainele.EZAWE = "S";
                }

                wMainele.FDGRP = null;
                wMainele.OPBUK = "12";
                wMainele.STDBK = "12";
                wMainele.IKEY = "Z3";
                wMainele.ZAHLKOND = "CM30";
                wMainele.CA_KOFIZ = asset.VATRATE__C.ToString();
                wMainele.Z_MODINV = wMainele.Decode_zmodiv(bilpro.SHIPPINGMETHOD__C);
                wMainele.Z_SINTDETT = "D";
                wMainele.Z_CODDES = bilpro.CODESDI__C.ToString();
                wMainele.IPA_CODE = bilpro.IPACODE__C.ToString();
                wMainele.IPA_BEGDA = bilpro.IPASTARTDATE__C.ToString();
                wMainele.Z_OU = "ND";
                wMainele.STRAT = wMainele.Decode_strat(account.CUSTOMERTYPE__C);
                

                wMainele.CITY1_FORN = service.NE__CITY__C;
                wMainele.COUNTRY_FORN = decode_country(service.NE__COUNTRY__C);
                wMainele.HOUSE_NUM1_FORN = service.NE_STREET_NUMBER__C;
                wMainele.NAME_CO_FORN = service.ASSETSHIPTO__C;
                wMainele.POST_CODE1_FORN = service.NE__POSTAL_CODE__C.ToString();
                wMainele.REGION_FORN = service.NE__PROVINCE__C;
                wMainele.STREET_FORN = service.NE__STREET__C;
                wMainele.ZOGAL_ISTATLOC = service.ASSETISTAT__C.ToString();


                wMainele.ZFREQ = asset.BILLINGFREQUENCY__C;
                wMainele.ZTENS = service.VOLTAGE__C.ToString();
                wMainele.IM_AB = "19500101";
                wMainele.OP_ER_TIPOUT_TF = Decode_tipo_uso(asset.CONTRACTTYPE__C);
                wMainele.OP_ED_POTDIS = service.AVAILABLEPOWER__C.ToString();
                wMainele.OP_ED_POTCON = service.APPLIEDPOWER__C.ToString();
                wMainele.OP_ER_LIVTEN_TF = service.VOLTAGELEVEL__C;
                wMainele.OP_ER_RESI_TF = Decode_bool(service.ISRESIDENTATSUPPLY__C);
                //TODO: OP_ER_OPZAEEG
                wMainele.EQ_COANC0 = service.ANNUALCONSUMPTION__C.ToString();


                wMainele.Z_PRODOTTO = asset.NAME;
                wMainele.Z_PRODOTTO_DESC = asset.PRODUCTDESCRIPTION__C;
                wMainele.Z_INIZIO = "20190101";
                wMainele.Z_FINE = "99991231";
                lmainele.Add(wMainele);
                InsMongoSingle(wMainele, ctx);

            }

            csvOut.WriteRecords<MainEle>(lmainele);

            Log.Information("--------------------------------------");
            writer.Close();
            Console.WriteLine("Fine programma");
            Console.ReadKey();
            }


        private static void InsMongoSingle(MainEle doc, MainEleContext ctx)
        {
            try
            {
                ctx.MainEleCollection.InsertOne(doc);
            }
            catch (Exception)
            {

                Log.Information("errore scrittura");
            }
        }
        private static string decode_country(string NE__COUNTRY__C)
        {
            switch (NE__COUNTRY__C)
            {
                case "Italia":
                    return "IT";
                default:
                    return null;
            }
        }

        private static string Decode_tipo_uso(int contractType)
        {
            switch (contractType)
            {
                case 1:
                    return "DO";
                case 2:
                    return "DO";
                case 3:
                    return "AU";
                case 4:
                    return "IP";
                default:
                    return null;
            }
        }

        private static string Decode_bool(bool iSRESIDENTATSUPPLY__C)
        {
            if (iSRESIDENTATSUPPLY__C)
            {
                return "X";
            } else
            {
                return null;
            }
        }

        private static string Val_dati_banks(string iban)
        {
            if (!string.IsNullOrEmpty(iban))
            {
                return iban.Substring(0, 2);
            }
            return null;
        }
        private static string Val_dati_bankl(string iban)
        {
            if (!string.IsNullOrEmpty(iban))
            {
                return iban.Substring(5, 10);
            }
            return null;
        }
        private static string Val_dati_bankn(string iban)
        {
            if (!string.IsNullOrEmpty(iban))
            {
                return iban.Substring(15, 12);
            }
            return null;
        }

        private static string Val_dati_bkont(string iban)
        {
            if (!string.IsNullOrEmpty(iban))
            {
                return iban.Substring(4, 1);
            }
            return null;
        }

        private static List<Account> ProcessAccounts(CsvReader csv)
        {
            try
            {
                var accounts = csv.GetRecords<Account>().ToList();
                return accounts;
            }
            catch (Exception)
            {
                Console.WriteLine("Errore su file {0}", "Account");
                throw;
            }
        }

        private static List<Assett> ProcessAssets(CsvReader csv)
        {
            try
            {
                var assets = csv.GetRecords<Assett>().ToList();
                return assets;
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
                var billings = csv.GetRecords<BillingProfile>().ToList();
               
                return billings;
            }
            catch (Exception)
            {
                Console.WriteLine("Errore su file {0}", "Billing Profile");
                throw;
            }
        }

        private static List<ServicePoint> ProcessServices(CsvReader csv)
        {
            try
            {
                var services = csv.GetRecords<ServicePoint>().ToList();
                
                return services;
            }
            catch (Exception)
            {
                Console.WriteLine("Errore su file {0}", "Services");
                throw;
            }
        }

        private static List<DirectDebit> ProcessDebits(CsvReader csv)
        {
            try
            {
                var debits = csv.GetRecords<DirectDebit>().ToList();

                return debits;
            }
            catch (Exception)
            {
                Console.WriteLine("Errore su file {0}", "DirectDebits");
                throw;
            }
        }
    }
    }
