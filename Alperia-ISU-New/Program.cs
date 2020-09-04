using Alperia_SFO;
using CsvHelper;
using Serilog;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Management.Instrumentation;
using System.Runtime.CompilerServices;

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

            var wfConfComm = new StreamWriter("E:\\work\\Alperia\\ConfComm.csv");
            var csvConfComm = new CsvWriter(wfConfComm, CultureInfo.InvariantCulture);
            csvConfComm.Configuration.Delimiter = ";";
            int wRownum = 0;

            var readerProdotti = new StreamReader("E:\\work\\Alperia\\comp-codProd-codcomp.csv");
            var csvProd = new CsvReader(readerProdotti, CultureInfo.InvariantCulture);
            csvProd.Configuration.Delimiter = ";";
            var lprodotti = new List<Zprodotti>();
            lprodotti = ProcessProdotti(csvProd);

            var readerMapping = new StreamReader("E:\\work\\Alperia\\mapping.csv");
            var csvMap = new CsvReader(readerMapping, CultureInfo.InvariantCulture);
            csvMap.Configuration.Delimiter = ";";
            var lmapping = new List<Zmapping>();
            lmapping = ProcessMapping(csvMap);

            string[] fileEntries = Directory.GetFiles(targetDirectory);
            var laccounts = new List<Account>();
            var lassets = new List<Assett>();
            var lbillings = new List<BillingProfile>();
            var lservice = new List<ServicePoint>();
            var ldirect = new List<DirectDebit>();
            List<MainEle> lmainele = new List<MainEle>();
            List<ConfComm> lmainConf = new List<ConfComm>();

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
                wMainele.SRC_SYSTEM = asset.SRC_SYSTEM;
                wMainele.ROW_ID = asset.ROW_ID;
                wMainele.RUN_ID = asset.RUN_ID;
                wMainele.ZMIGBP             = "1";
                wMainele.ZMIGCC             = "1";
                wMainele.ZMIGCTR            = "1";
                wMainele.ZMIGCO             = "1";
                wMainele.ZMIGINST           = "1";
                wMainele.ZMIGPOD            = "1";
                wMainele.ZMIGDEV            = "1";
                wMainele.ZMIGREA            = "1";
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


                wMainele.ZFREQ = wMainele.Decode_zfreq(asset.BILLINGFREQUENCY__C);
                wMainele.ZTENS = service.VOLTAGE__C.ToString();
                wMainele.IM_AB = "19500101";
                wMainele.Z_DISVENDITORE = "X";

                wMainele.OP_EU_STATOF = "Attiva";
                wMainele.OP_ER_TIPOUT_TF = Decode_tipo_uso(asset.CONTRACTTYPE__C);
                wMainele.OP_ED_POTDIS = service.AVAILABLEPOWER__C.ToString();
                wMainele.OP_ED_POTCON = service.ENGRAVEDPOWER__C.ToString();
                wMainele.OP_ER_LIVTEN_TF = service.VOLTAGELEVEL__C;
                wMainele.OP_ER_RESI_TF = Decode_bool(service.ISRESIDENTATSUPPLY__C);
                wMainele.OP_ER_OPZAEEG = Decode_opzaeeg(wMainele.OP_ER_TIPOUT_TF, wMainele.OP_ER_RESI_TF, wMainele.OP_ER_LIVTEN_TF);
                wMainele.EQ_COANC0 = service.ANNUALCONSUMPTION__C.ToString();
                Tuple<string, string> tarkond = Decode_tarkond(wMainele.OP_ER_TIPOUT_TF, wMainele.OP_ER_RESI_TF, wMainele.OP_ED_POTCON);
                wMainele.TARIFART_ER_ACC = tarkond.Item1;
                wMainele.KONDIGR_ER_ACC = tarkond.Item2;
                wMainele.EF_ESENZAC = "0.0";
                wMainele.EI_ACCAUFX = "0.0";
                Tuple<string, string> tarkond_opz = Decode_tarkond_opz(wMainele.OP_ER_OPZAEEG);
                wMainele.TARIFART_ER_OPZ = tarkond_opz.Item1;
                wMainele.KONDIGR_ER_OPZ = tarkond_opz.Item2;
                wMainele.DATE_FROM = "19500101";
                wMainele.EXT_UI = service.POD__C;
                wMainele.GRID_ID = wMainele.Decode_grid_id(service.DISTRIBUTOR__C);

                // Dati del misuratore - start
                wMainele.ZWGRUPPE = "ZLETF0";
                wMainele.EGERR_INFO = service.REGISTRATIONNUMBER__C;
                wMainele.MATNR = "ZLETF0";
                wMainele.KEYDATE = "20190101";
                wMainele.NCAP_STANZVOR = 9;
                wMainele.NCIR_STANZVOR = 9;
                wMainele.ZWFAKT_ATT_F0 = 1.0f;
                //wMainele.ZWFAKT_ATT_F1 = 1.0f;
                //wMainele.ZWFAKT_ATT_F2 = 1.0f;
                //wMainele.ZWFAKT_ATT_F3 = 1.0f;
                wMainele.ZWFAKT_REA_F0 = 1.0f;
                //wMainele.ZWFAKT_REA_F1 = 1.0f;
                //wMainele.ZWFAKT_REA_F2 = 1.0f;
                //wMainele.ZWFAKT_REA_F3 = 1.0f;
                wMainele.ZWFAKT_POT_F0 = 1.0f;
                //wMainele.ZWFAKT_POT_F1 = 1.0f;
                //wMainele.ZWFAKT_POT_F2 = 1.0f;
                //wMainele.ZWFAKT_POT_F3 = 1.0f;
                wMainele.NCPP_STANZVOR = 9;
                wMainele.EADAT = "20190101";
                // Dati del misuratore - end

                wMainele.EINZDAT = asset.NE__STARTDATE__C.ToString();
                wMainele.AUSZDAT = asset.NE__ENDDATE__C.ToString();
                wMainele.Z_MERCATO = wMainele.Decode_zmercato(asset.MARKETTYPE__C);
                wMainele.BUKRS = "12";
                wMainele.SPARTE = "E";
                wMainele.GEMFAKT = asset.INVOICEJOINTTYPE__C.ToString();
                wMainele.COKEY = Decode_cokey(wMainele.Z_MERCATO, wMainele.OP_ER_TIPOUT_TF);
                wMainele.KOFIZ = wMainele.CA_KOFIZ;
                wMainele.ZCANACQ = Decode_zcanacq(asset.CHANNEL__C);
                wMainele.ZTIP_OFF = Decode_ztipoff(wMainele.Z_MERCATO);
                wMainele.Z_PRODOTTO = asset.NAME;
                wMainele.Z_PRODOTTO_DESC = asset.PRODUCTDESCRIPTION__C;
                wMainele.Z_INIZIO = "20190101";
                wMainele.Z_FINE = "99991231";
                // dati della lettura

                wMainele.RLEA_PREL_F0 = "0.0";
                wMainele.RLREA_PREL_F0 = "0.0";
                wMainele.RLPOT_PREL_F0 = "0.0";


                lmainele.Add(wMainele);
                InsMongoSingle(wMainele, ctx);
                var lwConfComm = CreaConfComm(asset, lprodotti, lmapping);
                foreach (var conf in lwConfComm)
                {
                    conf.Row_id = wRownum;
                    lmainConf.Add(conf);
                    wRownum += 1;
                }

            }

            csvOut.WriteRecords<MainEle>(lmainele);
            csvConfComm.WriteRecords<ConfComm>(lmainConf);
        

            Log.Information("--------------------------------------");
            writer.Close();
            wfConfComm.Close();
            Console.WriteLine("Fine programma");
            Console.ReadKey();
            }

        private static List<ConfComm> CreaConfComm(Assett asset, List<Zprodotti> lprodotti, List<Zmapping> lmapping)
        {
            List<ConfComm> wlconf = new List<ConfComm>();
            Log.Information("crea conf comm");
            Log.Information($"Prodotto {asset.PRICE_LIST_CODE_D__C}");
            var iprodotto = asset.PRICE_LIST_CODE_D__C;
            var lcomp = lprodotti.Where(p => p.CodProd == iprodotto).ToList();
            foreach (var comp in lcomp)
            {
                var lmap = lmapping.Where(m => m.CodComp == comp.CodComp).ToList();
                foreach (var map in lmap)
                {
                    var conf = new ConfComm();
                    conf.Ext_ui = asset.PODPDRPDC__C;
                    conf.Ab = "20200101";
                    conf.Bis = "99991231";
                    conf.Cod_componente = comp.CodComp;
                    conf.Campo = map.Operando;
                    conf.Valore = CalcolaValore(map);
                    wlconf.Add(conf);

                }
            }
            return wlconf;

        }

        private static string CalcolaValore(Zmapping map)
        {
            if (map.TypOperando == "CHAR" && map.LenOperando == "1")
            {
                return "X";
            } else if (map.TypOperando == "CHAR" && map.LenOperando == "10")
            {
                return map.CostValue;
            }
            else
            {
                return "0.0";
            }
        }

        private static string Decode_zcanacq(string CHANNEL)
        {
           if (CHANNEL == "ENGYP")
            {
                return "SP";
            } else
            {
                return "EC";
            }
        }

        private static string Decode_ztipoff(string MERCATO)
        {
            if (MERCATO == "MT")
            {
                return "B2C";
            } else
            {
                return "B2B";
            }
        }

        private static string Decode_cokey(string MERCATO, string TIPOUT)
        {
            if (MERCATO == "MT" && TIPOUT == "DO") 
            {
                return "E_1T011";
            } else if (MERCATO == "MT" && (TIPOUT == "AU" || TIPOUT == "IP"))
            {
                return "E_1T012";
            } else if (MERCATO == "ML" && TIPOUT == "DO")
            {
                return "E_1L011";
            }
            else if (MERCATO == "ML" && (TIPOUT == "AU" || TIPOUT == "IP"))
            {
                return "E_1L012";
            } else
            {
                return "E_1L060";
            }
        }

        private static Tuple<string, string> Decode_tarkond_opz(string OPZAEEG)
        {
            if (OPZAEEG == "TDR")
            {
                return new Tuple<string, string>("E_TDR", "TDR");
            } else if (OPZAEEG == "TDNR")
            {
                return new Tuple<string, string>("E_TDNR", "TDNR");
            } else if (OPZAEEG == "BTA1")
            {
                return new Tuple<string, string>("E_BTA1", "BTA1");
            } else if (OPZAEEG == "BTIP")
            {
                return new Tuple<string, string>("E_BTIP", "BTIP");
            } else if (OPZAEEG == "MTIP")
            {
                return new Tuple<string, string>("E_MTIP", "MTIP");
            }
            else
            {
                return new Tuple<string, string>("E_AAT1", "AAT1");
            }
        }

        private static Tuple<string, string> Decode_tarkond(string TIPOUT, string RESI, string POTCON)
        {
            if(TIPOUT == "DO" && RESI == null)
            {
                return new Tuple<string, string>("E_ACDM", "ACCDNR");
            } else if (TIPOUT == "DO" && RESI == "X")
            {
                return new Tuple<string, string>("E_ACDM", "ACCDR2");
            } else if (TIPOUT == "IP")
            {
                return new Tuple<string, string>("E_ACAU", "ORD");
            }
            else
            {
                return new Tuple<string, string>("E_ACAU", "ORD");
            }
        }

        private static string Decode_opzaeeg(string TIPOUT, string RESI, string LIVTEN)
        {
            if (TIPOUT == "DO")
            {
                if (RESI == "X")
                {
                    return "TDR";
                } else
                {
                    return "TDNR";
                }
            }
            else if (TIPOUT == "IP" && LIVTEN == "BT")
            {
                return "BTIP";
            }
            else if (TIPOUT == "IP" && LIVTEN == "MT")
            {
                return "MTIP";
            }
            else if (TIPOUT == "AU" && LIVTEN == "BT")
            {
                return "BTA1";
            }
            else if (TIPOUT == "AU" && LIVTEN == "MT")
            {
                return "MTA1";
            }
            else
            {
                return "AAT1";
            }
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

        private static List<Zprodotti> ProcessProdotti(CsvReader csv)
        {
            try
            {
                var zprodotti = csv.GetRecords<Zprodotti>().ToList();
                return zprodotti;
            }
            catch (Exception)
            {
                Console.WriteLine("Errore su file {0}", "Zprodotti");
                throw;
            }
        }

        private static List<Zmapping> ProcessMapping(CsvReader csv)
        {
            try
            {
                var zmapping = csv.GetRecords<Zmapping>().ToList();
                return zmapping;
            }
            catch (Exception)
            {
                Console.WriteLine("Errore su file {0}", "Zmapping");
                throw;
            }
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
