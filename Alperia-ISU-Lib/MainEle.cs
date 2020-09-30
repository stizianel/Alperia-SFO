using System;
using System.Runtime.CompilerServices;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;
using MongoDB.Driver;

public class MainEle
{
        public string SRC_SYSTEM { get; set; }
        public string RUN_ID { get; set; }
        public int ROW_ID { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string ZMIGBP { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string ZMIGCC { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string ZMIGCTR { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string ZMIGCO { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string ZMIGINST { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string ZMIGPOD { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string ZMIGDEV { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string ZMIGREA { get; set; }
        public string CRM_PARTNER { get; set; }
        public string CRM_ACCOUNT { get; set; }
        public string CRM_CONTRATTO { get; set; }
        public string CRM_IMPIANTO { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string TIPO_OPERAZIONE { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string BU_TYPE { get; set; }
        public string NAME_FIRST { get; set; }
        public string NAME_LAST { get; set; }
        public string NAME_ORG1 { get; set; }
        public string NAME_ORG2 { get; set; }
        public string NAME_ORG3 { get; set; }
        public string NAME_ORG4 { get; set; }
        public string TITLE { get; set; }
        public string LEGAL_ENTY { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string CITY1_ESAZ { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string COUNTRY_ESAZ { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string HOUSE_NUM1_ESAZ { get; set; }
        public string NAME_CO_ESAZ { get; set; }
        public string FLOOR_ESAZ { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string POST_CODE1_ESAZ { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string REGION_ESAZ { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string STREET_ESAZ { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string ZBP_ISTATLOC { get; set; }
        public string HOUSE_NUM2_ESAZ { get; set; }
        public string ROOMNUMBER_ESAZ { get; set; }
        public string STR_SUPPL1_ESAZ { get; set; }
        public string STR_SUPPL2_ESAZ { get; set; }
        public string STR_SUPPL3_ESAZ { get; set; }
        public string BUILDING_ESAZ { get; set; }
        public string PO_BOX_ESAZ { get; set; }
        public string PO_BOX_CTY_ESAZ { get; set; }
        public string PO_BOX_LOBBY_ESAZ { get; set; }
        public string PO_BOX_LOC_ESAZ { get; set; }
        public string PO_BOX_NUM_ESAZ { get; set; }
        public string POST_CODE2_ESAZ { get; set; }
        public string CITY2_ESAZ { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string CITY1_CLIENTE { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string COUNTRY_CLIENTE { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string HOUSE_NUM1_CLIENTE { get; set; }
        public string NAME_CO_CLIENTE { get; set; }
        public string FLOOR_CLIENTE { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string POST_CODE1_CLIENTE { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string REGION_CLIENTE { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string STREET_CLIENTE { get; set; }
        public string HOUSE_NUM2_CLIENTE { get; set; }
        public string ROOMNUMBER_CLIENTE { get; set; }
        public string STR_SUPPL1_CLIENTE { get; set; }
        public string STR_SUPPL2_CLIENTE { get; set; }
        public string STR_SUPPL3_CLIENTE { get; set; }
        public string BUILDING_CLIENTE { get; set; }
        public string CITY2_CLIENTE { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string ZBP_ISTATLOC_CLIENTE { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string BPEXT { get; set; }
    public string ZBPEXT_AGGR { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string AUGRP { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string BPKIND { get; set; }
    public string CF_TAXNUM { get; set; }
    public string PI_TAXNUM { get; set; }
        public string Z_MERCRIE { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string ZPUBB { get; set; }
        public string TEL_NUMBER1 { get; set; }
        public string TEL_NUMBER2 { get; set; }
        public string TEL_NUMBER3 { get; set; }
        public string FAX_NUMBER { get; set; }
        public string SMTP_ADDR { get; set; }
        public string PEC_SMTP_ADDR { get; set; }
        public string FAX_NUMBER2 { get; set; }
        public string SMTP_ADDR2 { get; set; }
        public string TEL_NUMBER4 { get; set; }
        public string TEL_NUMBER5 { get; set; }
        public string TEL_MOBILE { get; set; }
        public string Z_TEL_NUMBER { get; set; }
        public string BANKS { get; set; }
        public string BANKL { get; set; }
        public string BANKN { get; set; }
        public string BKONT { get; set; }
        public string IBAN { get; set; }
        public string MAND_SEPA { get; set; }
        public string STAT_MAND { get; set; }
        public string COD_CUCSIA { get; set; }
        public string CREDITORID { get; set; }
        public string DATASOTT { get; set; }
        public string B2B { get; set; }
        public string CODFISCFIRMA { get; set; }
        public string NOMEFIRMA { get; set; }
        public string COGNOMEFIRMA { get; set; }
        public string AB_MANDATO { get; set; }
        public string BIS_MANDATO { get; set; }
        public string ZNRIL_IVA { get; set; }
        public string ZGRUPIVA { get; set; }
        public string ZAB_GR_IVA { get; set; }
        public string ZBIS_GR_IVA { get; set; }
        public string VKONA { get; set; }
        public string VBUND { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string OPBUK { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string STDBK { get; set; }
        public string Z_COBRAND { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string IKEY { get; set; }
        public string AUSGRUP_IN { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string ZAHLKOND { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string CA_KOFIZ { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string Z_MODINV { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string Z_SINTDETT { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string Z_CODDES { get; set; }
        public string IPA_CODE { get; set; }
        public string IPA_BEGDA { get; set; }
        public string Z_OU { get; set; }
        public string Z_IOU { get; set; }
        public string Z_CONSIP { get; set; }
        public string Z_RIFAMM { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string EZAWE { get; set; }
        public string PROTNUM { get; set; }
        public string MWSKZ { get; set; }
        public string EXDFR { get; set; }
        public string EXDTO { get; set; }
        public string EXNUM { get; set; }
        public string EXRAT { get; set; }
        public string Z_PLAFOND { get; set; }
        public string Z_EROSO { get; set; }
        public string INTEREST_LOCK { get; set; }
        public string TDATE { get; set; }
        public string INV_REASON { get; set; }
        public string EZASP { get; set; }
        public string AZASP { get; set; }
        public string LOTTO_AGGREGA { get; set; }
        public string VKONT_PADRE { get; set; }
        public string VKONT_FOGLIA { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string STRAT { get; set; }
        public string CCNUM { get; set; }
        public string DATA_INIZIO_CCNUM { get; set; }
        public string DATA_FINE_CCNUM { get; set; }
        public string ZCA_RES { get; set; }
        public string ZCOD_RES { get; set; }
        public string ZCA_TOP { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string CITY1_FORN { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string COUNTRY_FORN { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string HOUSE_NUM1_FORN { get; set; }
        public string NAME_CO_FORN { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string POST_CODE1_FORN { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string REGION_FORN { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string STREET_FORN { get; set; }
        public string HOUSE_NUM2_FORN { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string ZOGAL_ISTATLOC { get; set; }
        public string ZOGAL_CITY2 { get; set; }
        public string LGZUSATZ { get; set; }
        public string VBSART { get; set; }
        public string PR_ROOMNUMBER { get; set; }
        public string PR_FLOOR_1 { get; set; }
        public string ZTIF { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string ZFREQ { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string IM_AB { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string IM_SPEBENE { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string Z_DISVENDITORE { get; set; }
        public string BRANCHE { get; set; }
        public string Z_CUTOFF { get; set; }
        public string Z_ULTFATT { get; set; }
        public string Z_TPULTFATT { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string ZTENS { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string OP_EU_STATOF { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string OP_ER_TIPOUT_TF { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string OP_ED_POTDIS { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string OP_ED_POTCON { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string OP_ER_LIVTEN_TF { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string OP_ER_RESI_TF { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string OP_ER_OPZAEEG { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string EQ_COANC0 { get; set; }
        public string EQ_COAND0 { get; set; }
        public string EQ_COAND1 { get; set; }
        public string EQ_COAND2 { get; set; }
        public string EQ_COAND3 { get; set; }
        public string OP_EG_DISDIS { get; set; }
        public string OP_EG_INTPRO { get; set; }
        public string OP_EG_MISLIM { get; set; }
        public string OP_EG_ENERG { get; set; }
        public string OP_EG_AUSIL { get; set; }
        public string OP_EG_DOMD1 { get; set; }
        public string Z_MISGTW { get; set; }
        public string OP_EG_MIS2G { get; set; }
        public string DATA_2G { get; set; }
        public string OP_EU_CLESEN { get; set; }
        public string CODICE_ACCISE { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string TARIFART_ER_ACC { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string KONDIGR_ER_ACC { get; set; }
        public string EF_ESENZAC { get; set; }
        public string EI_ACCAUFX { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string TARIFART_ER_OPZ { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string KONDIGR_ER_OPZ { get; set; }
        public string IMPIANTO_OLD { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string ZTIP_OFF { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string DATE_FROM { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string EXT_UI { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string GRID_ID { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string ZWGRUPPE { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string EGERR_INFO { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string MATNR { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string KEYDATE { get; set; }
        public string APP_BIS { get; set; }
        public int NCAP_STANZVOR { get; set; }
        public int NCIR_STANZVOR { get; set; }
    public string ZWFAKT_ATT_F0 { get; set; }
        public string ZWFAKT_ATT_F1 { get; set; }
        public string ZWFAKT_ATT_F2 { get; set; }
        public string ZWFAKT_ATT_F3 { get; set; }
    public string ZWFAKT_REA_F0 { get; set; }
        public string ZWFAKT_REA_F1{ get; set; }
        public string ZWFAKT_REA_F2 { get; set; }
        public string ZWFAKT_REA_F3 { get; set; }
    public string ZWFAKT_POT_F0 { get; set; }
        public string ZWFAKT_POT_F1 { get; set; }
        public string ZWFAKT_POT_F2 { get; set; }
        public string ZWFAKT_POT_F3 { get; set; }
        public int NCPP_STANZVOR { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string EADAT { get; set; }
        public string FLAG_ATTF0 { get; set; }
        public string FLAG_ATTF1 { get; set; }
        public string FLAG_ATTF2 { get; set; }
        public string FLAG_ATTF3 { get; set; }
        public string FLAG_REAF0 { get; set; }
        public string FLAG_REAF1 { get; set; }
        public string FLAG_REAF2 { get; set; }
        public string FLAG_REAF3 { get; set; }
        public string FLAG_POTF0 { get; set; }
        public string FLAG_POTF1 { get; set; }
        public string FLAG_POTF2 { get; set; }
        public string FLAG_POTF3 { get; set; }
        public string FLAG_EDMAT { get; set; }
        public string FLAG_EDMRT { get; set; }
        public int NEDMAT_STANZVOR { get; set; }
        public int NEDMRE_STANZVOR { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string EINZDAT { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string AUSZDAT { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string Z_MERCATO { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string BUKRS { get; set; }
        public string VBEZ  { get; set; }
        public string VREFER { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string SPARTE { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string GEMFAKT { get; set; }
        public string ABRSPERR { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string COKEY { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string KOFIZ { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string ZCANACQ { get; set; }
        public string Z_KAM { get; set; }
        public string Z_UTF { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string Z_PRODOTTO { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string Z_PRODOTTO_DESC { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string Z_INIZIO { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string Z_FINE  { get; set; }
        public string Z_LISTINO { get; set; }
        public string Z_DEPOSITO { get; set; }
        public string ZIMP_DEPOSITO { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string Z_TIPODEP { get; set; }
        public string Z_OFF_VERDE  { get; set; }
        public string Z_MOT_CESS { get; set; }
        public string Z_MOT_ATT { get; set; }
        public string Z_CUP { get; set; }
        public string Z_CIG { get; set; }
        public string ZCIGF { get; set; }
        public string Z_NOTE { get; set; }
        public string RLEA_PREL_F0 { get; set; }
        public string RLEA_PREL_F1 { get; set; }
        public string RLEA_PREL_F2 { get; set; }
        public string RLEA_PREL_F3 { get; set; }
        public string RLREA_PREL_F0 { get; set; }
        public string RLREA_PREL_F1{ get; set; }
        public string RLREA_PREL_F2 { get; set; }
        public string RLREA_PREL_F3 { get; set; }
        public string RLPOT_PREL_F0 { get; set; }
        public string RLPOT_PREL_F1 { get; set; }
        public string RLPOT_PREL_F2 { get; set; }
        public string RLPOT_PREL_F3 { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string REA_ADAT { get; set; }
    [Required(ErrorMessage = "{0} is required")]
    public string REA_ISTABLART { get; set; }

    public string Decode_bpkind(string iBpkind)
    {
        switch (iBpkind)
        {
            case "RSDN":
                return "Z001";
            case "SMEN":
                return "Z004";
            case "PUAM":
                return "Z005";
            case "CORP":
                return "Z003";
            case "RSLR":
                return "Z002";
            case "CNDM":
                return "Z006";
            case "ASSC":
                return "Z007";
            default:
                return null;
        }
    }

    public string Decode_zmercato(string imerc)
    {
        switch (imerc)
        {
            case "T":
                return "MT";
            case "L":
                return "ML";
            default:
                return null;
        }
    }

    public string Decode_grid_id(string iDist)
    {
        switch (iDist)
        {
            case "AE-EW-EE":
                return "ZE_D03255";
            case "EDYNA":
                return "ZE_D03255";
            case "SELNET":
                return "ZE_D01039";
            case "ENEL-D":
                return "ZE_D00435";
            default:
                return "ZE_D03255";
        }
    }

    public string Decode_strat(string iBpkind)
    {
        switch (iBpkind)
        {
            case "RSDN":
                return "Z1";
            case "SMEN":
                return "Z2";
            case "PUAM":
                return "Z2";
            case "CORP":
                return "Z2";
            case "RSLR":
                return "Z2";
            case "CNDM":
                return "Z2";
            case "ASSC":
                return "Z2";
            default:
                return null;
        }
    }

    public string Decode_zfreq(string ifreq)
    {
        switch (ifreq)
        {
            case "Mensile":
                return "MENS";
            case "Bimestrale":
                return "BIME";
            default:
                return null;
        }
    }

    public string Decode_zmodiv(string ishipmet, string bpkind)
    {
        if(bpkind == "Z005")
        {
            return "FEPA";
        } else
        {
            switch (ishipmet)
            {
                case "EML":
                    return "ZD";
                case "PST":
                    return "ZC";
                default:
                    return null;
            }
        }
        
    }
}

public class MainEleContext
{
    private readonly IMongoDatabase _db;

    public MainEleContext()
    {
        MongoClient client = new MongoClient();
        _db = client.GetDatabase("Alperia");
        _db.GetCollection<MainEle>("MainEle");
    }

    public IMongoCollection<MainEle> MainEleCollection => _db.GetCollection<MainEle>("MainEle");
}

