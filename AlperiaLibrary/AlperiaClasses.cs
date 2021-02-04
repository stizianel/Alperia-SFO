using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlperiaLibrary
{
    public class MainEle
    {
        public ObjectId Id { get; set; }
        public string SRC_SYSTEM { get; set; }
        public string RUN_ID { get; set; }
        public int ROW_ID { get; set; }
        public string ZMIGBP { get; set; }
        public string ZMIGCC { get; set; }
        public string ZMIGCTR { get; set; }
        public string ZMIGCO { get; set; }
        public string ZMIGINST { get; set; }
        public string ZMIGPOD { get; set; }
        public string ZMIGDEV { get; set; }
        public string ZMIGREA { get; set; }
        public string CRM_PARTNER { get; set; }
        public string CRM_ACCOUNT { get; set; }
        public string CRM_CONTRATTO { get; set; }
        public string CRM_IMPIANTO { get; set; }
        public string TIPO_OPERAZIONE { get; set; }
        public string BU_TYPE { get; set; }
        public string NAME_FIRST { get; set; }
        public string NAME_LAST { get; set; }
        public string NAME_ORG1 { get; set; }
        public string NAME_ORG2 { get; set; }
        public string NAME_ORG3 { get; set; }
        public string NAME_ORG4 { get; set; }
        public string TITLE { get; set; }
        public string LEGAL_ENTY { get; set; }
        public string CITY1_ESAZ { get; set; }
        public string COUNTRY_ESAZ { get; set; }
        public string HOUSE_NUM1_ESAZ { get; set; }
        public string NAME_CO_ESAZ { get; set; }
        public string FLOOR_ESAZ { get; set; }
        public string POST_CODE1_ESAZ { get; set; }
        public string REGION_ESAZ { get; set; }
        public string STREET_ESAZ { get; set; }
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
        public string CITY1_CLIENTE { get; set; }
        public string COUNTRY_CLIENTE { get; set; }
        public string HOUSE_NUM1_CLIENTE { get; set; }
        public string NAME_CO_CLIENTE { get; set; }
        public string FLOOR_CLIENTE { get; set; }
        public string POST_CODE1_CLIENTE { get; set; }
        public string REGION_CLIENTE { get; set; }
        public string STREET_CLIENTE { get; set; }
        public string HOUSE_NUM2_CLIENTE { get; set; }
        public string ROOMNUMBER_CLIENTE { get; set; }
        public string STR_SUPPL1_CLIENTE { get; set; }
        public string STR_SUPPL2_CLIENTE { get; set; }
        public string STR_SUPPL3_CLIENTE { get; set; }
        public string BUILDING_CLIENTE { get; set; }
        public string CITY2_CLIENTE { get; set; }
        public string ZBP_ISTATLOC_CLIENTE { get; set; }
        public string BPEXT { get; set; }
        public string ZBPEXT_AGGR { get; set; }
        public string AUGRP { get; set; }
        public string BPKIND { get; set; }
        public string CF_TAXNUM { get; set; }
        public string PI_TAXNUM { get; set; }
        public string Z_MERCRIE { get; set; }
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
        public string Z_LINGUA { get; set; }
        public string ZNRIL_IVA { get; set; }
        public string ZGRUPIVA { get; set; }
        public string ZAB_GR_IVA { get; set; }
        public string ZBIS_GR_IVA { get; set; }
        public string VKONA { get; set; }
        public string VBUND { get; set; }
        public string OPBUK { get; set; }
        public string STDBK { get; set; }
        public string Z_COBRAND { get; set; }
        public string IKEY { get; set; }
        //        public string AUSGRUP_IN { get; set; }
        public string ZAHLKOND { get; set; }
        public string CA_KOFIZ { get; set; }
        public string Z_MODINV { get; set; }
        public string Z_SINTDETT { get; set; }
        public string Z_CODDES { get; set; }
        public string IPA_CODE { get; set; }
        public string IPA_BEGDA { get; set; }
        public string Z_OU { get; set; }
        public string Z_IOU { get; set; }
        public string Z_CONSIP { get; set; }
        public string Z_RIFAMM { get; set; }
        public string EZAWE { get; set; }
        public string PROTNUM { get; set; }
        public string MWSKZ { get; set; }
        public string EXDFR { get; set; }
        public string EXDTO { get; set; }
        public string EXNUM { get; set; }
        public string EXRAT { get; set; }
        public string Z_PLAFOND { get; set; }
        public string Z_EROSO { get; set; }
        public string ZEXABIT { get; set; }
        public string INTEREST_LOCK { get; set; }
        public string TDATE { get; set; }
        public string INV_REASON { get; set; }
        public string EZASP { get; set; }
        public string AZASP { get; set; }
        public string LOTTO_AGGREGA { get; set; }
        public string VKONT_PADRE { get; set; }
        public string VKONT_FOGLIA { get; set; }
        public string STRAT { get; set; }
        public string CCNUM { get; set; }
        public string DATA_INIZIO_CCNUM { get; set; }
        public string DATA_FINE_CCNUM { get; set; }
        public string ZCA_RES { get; set; }
        public string ZCOD_RES { get; set; }
        public string ZCA_TOP { get; set; }
        public string CITY1_FORN { get; set; }
        public string COUNTRY_FORN { get; set; }
        public string HOUSE_NUM1_FORN { get; set; }
        public string NAME_CO_FORN { get; set; }
        public string POST_CODE1_FORN { get; set; }
        public string REGION_FORN { get; set; }
        public string STREET_FORN { get; set; }
        public string HOUSE_NUM2_FORN { get; set; }
        public string ZOGAL_ISTATLOC { get; set; }
        public string ZOGAL_CITY2 { get; set; }
        public string LGZUSATZ { get; set; }
        public string VBSART { get; set; }
        public string PR_ROOMNUMBER { get; set; }
        public string PR_FLOOR_1 { get; set; }
        public string ZTIF { get; set; }
        public string ZFREQ { get; set; }
        public string IM_AB { get; set; }
        public string IM_SPEBENE { get; set; }
        public string Z_DISVENDITORE { get; set; }
        public string BRANCHE { get; set; }
        public string Z_CUTOFF { get; set; }
        public string Z_ULTFATT { get; set; }
        public string Z_TPULTFATT { get; set; }
        public string ZTENS { get; set; }
        public string OP_EU_STATOF { get; set; }
        public string OP_ER_TIPOUT_TF { get; set; }
        public string OP_ED_POTDIS { get; set; }
        public string OP_ED_POTCON { get; set; }
        public string OP_ER_LIVTEN_TF { get; set; }
        public string OP_ER_RESI_TF { get; set; }
        public string OP_ER_OPZAEEG { get; set; }
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
        public string TARIFART_ER_ACC { get; set; }
        public string KONDIGR_ER_ACC { get; set; }
        public string EF_ESENZAC { get; set; }
        public string EI_ACCAUFX { get; set; }
        public string TARIFART_ER_OPZ { get; set; }
        public string KONDIGR_ER_OPZ { get; set; }
        public string EF_SALVAG { get; set; }
        public string IMPIANTO_OLD { get; set; }
        public string ZTIP_OFF { get; set; }
        public string DATE_FROM { get; set; }
        public string EXT_UI { get; set; }
        public string GRID_ID { get; set; }
        public string ZWGRUPPE { get; set; }
        public string EGERR_INFO { get; set; }
        public string MATNR { get; set; }
        public string KEYDATE { get; set; }
        public string APP_BIS { get; set; }
        public string NCAP_STANZVOR { get; set; }
        public string NCIR_STANZVOR { get; set; }
        public string ZWFAKT_ATT_F0 { get; set; }
        public string ZWFAKT_ATT_F1 { get; set; }
        public string ZWFAKT_ATT_F2 { get; set; }
        public string ZWFAKT_ATT_F3 { get; set; }
        public string ZWFAKT_REA_F0 { get; set; }
        public string ZWFAKT_REA_F1 { get; set; }
        public string ZWFAKT_REA_F2 { get; set; }
        public string ZWFAKT_REA_F3 { get; set; }
        public string ZWFAKT_POT_F0 { get; set; }
        public string ZWFAKT_POT_F1 { get; set; }
        public string ZWFAKT_POT_F2 { get; set; }
        public string ZWFAKT_POT_F3 { get; set; }
        public string NCPP_STANZVOR { get; set; }
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
        public string NEDMAT_STANZVOR { get; set; }
        public string NEDMRE_STANZVOR { get; set; }
        public string EINZDAT { get; set; }
        public string AUSZDAT { get; set; }
        public string Z_MERCATO { get; set; }
        public string BUKRS { get; set; }
        public string VBEZ { get; set; }
        public string VREFER { get; set; }
        public string SPARTE { get; set; }
        public string GEMFAKT { get; set; }
        public string ABRSPERR { get; set; }
        public string KOFIZ { get; set; }
        public string ZCANACQ { get; set; }
        public string Z_KAM { get; set; }
        public string Z_UTF { get; set; }
        public string Z_PRODOTTO { get; set; }
        public string Z_PRODOTTO_DESC { get; set; }
        public string Z_INIZIO { get; set; }
        public string Z_FINE { get; set; }
        public string Z_LISTINO { get; set; }
        public string Z_DEPOSITO { get; set; }
        public string ZIMP_DEPOSITO { get; set; }
        public string Z_TIPODEP { get; set; }
        public string Z_OFF_VERDE { get; set; }
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
        public string RLREA_PREL_F1 { get; set; }
        public string RLREA_PREL_F2 { get; set; }
        public string RLREA_PREL_F3 { get; set; }
        public string RLPOT_PREL_F0 { get; set; }
        public string RLPOT_PREL_F1 { get; set; }
        public string RLPOT_PREL_F2 { get; set; }
        public string RLPOT_PREL_F3 { get; set; }
        public string REA_ADAT { get; set; }

     }

    public class MainGas
    {
        public ObjectId Id { get; set; }
        public string SRC_SYSTEM { get; set; }
        public string RUN_ID { get; set; }
        public int ROW_ID { get; set; }
        public string ZMIGBP { get; set; }
        public string ZMIGCC { get; set; }
        public string ZMIGCTR { get; set; }
        public string ZMIGCO { get; set; }
        public string ZMIGINST { get; set; }
        public string ZMIGPDR { get; set; }
        public string ZMIGDEV { get; set; }
        public string ZMIGREA { get; set; }
        public string CRM_PARTNER { get; set; }
        public string CRM_ACCOUNT { get; set; }
        public string CRM_CONTRATTO { get; set; }
        public string CRM_IMPIANTO { get; set; }
        public string TIPO_OPERAZIONE { get; set; }
        public string BU_TYPE { get; set; }
        public string NAME_FIRST { get; set; }
        public string NAME_LAST { get; set; }
        public string NAME_ORG1 { get; set; }
        public string NAME_ORG2 { get; set; }
        public string NAME_ORG3 { get; set; }
        public string NAME_ORG4 { get; set; }
        public string TITLE { get; set; }
        public string LEGAL_ENTY { get; set; }
        public string CITY1_ESAZ { get; set; }
        public string COUNTRY_ESAZ { get; set; }
        public string HOUSE_NUM1_ESAZ { get; set; }
        public string NAME_CO_ESAZ { get; set; }
        public string FLOOR_ESAZ { get; set; }
        public string POST_CODE1_ESAZ { get; set; }
        public string REGION_ESAZ { get; set; }
        public string STREET_ESAZ { get; set; }
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
        public string CITY1_CLIENTE { get; set; }
        public string COUNTRY_CLIENTE { get; set; }
        public string HOUSE_NUM1_CLIENTE { get; set; }
        public string NAME_CO_CLIENTE { get; set; }
        public string FLOOR_CLIENTE { get; set; }
        public string POST_CODE1_CLIENTE { get; set; }
        public string REGION_CLIENTE { get; set; }
        public string STREET_CLIENTE { get; set; }
        public string HOUSE_NUM2_CLIENTE { get; set; }
        public string ROOMNUMBER_CLIENTE { get; set; }
        public string STR_SUPPL1_CLIENTE { get; set; }
        public string STR_SUPPL2_CLIENTE { get; set; }
        public string STR_SUPPL3_CLIENTE { get; set; }
        public string BUILDING_CLIENTE { get; set; }
        public string CITY2_CLIENTE { get; set; }
        public string ZBP_ISTATLOC_CLIENTE { get; set; }
        public string BPEXT { get; set; }
        public string AUGRP { get; set; }
        public string BPKIND { get; set; }
        public string CF_TAXNUM { get; set; }
        public string PI_TAXNUM { get; set; }
        public string Z_MERCRIE { get; set; }
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

        public string OPBUK { get; set; }

        public string STDBK { get; set; }
        public string Z_COBRAND { get; set; }

        public string IKEY { get; set; }
        //public string AUSGRUP_IN { get; set; }

        public string ZAHLKOND { get; set; }

        public string CA_KOFIZ { get; set; }

        public string Z_MODINV { get; set; }

        public string Z_SINTDETT { get; set; }

        public string Z_CODDES { get; set; }
        public string IPA_CODE { get; set; }
        public string IPA_BEGDA { get; set; }
        public string Z_OU { get; set; }
        public string Z_IOU { get; set; }
        public string Z_CONSIP { get; set; }
        public string Z_RIFAMM { get; set; }

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

        public string STRAT { get; set; }
        public string CCNUM { get; set; }
        public string DATA_INIZIO_CCNUM { get; set; }
        public string DATA_FINE_CCNUM { get; set; }
        public string ZCA_RES { get; set; }
        public string ZCOD_RES { get; set; }
        public string ZCA_TOP { get; set; }

        public string CITY1_FORN { get; set; }

        public string COUNTRY_FORN { get; set; }

        public string HOUSE_NUM1_FORN { get; set; }
        public string NAME_CO_FORN { get; set; }

        public string POST_CODE1_FORN { get; set; }

        public string REGION_FORN { get; set; }

        public string STREET_FORN { get; set; }
        public string HOUSE_NUM2_FORN { get; set; }

        public string ZOGAL_ISTATLOC { get; set; }
        public string ZOGAL_CITY2 { get; set; }
        public string LGZUSATZ { get; set; }
        public string VBSART { get; set; }
        public string PR_ROOMNUMBER { get; set; }
        public string PR_FLOOR_1 { get; set; }
        public string ZTIF { get; set; }

        public string ZFREQ { get; set; }

        public string IM_AB { get; set; }

        public string Z_DISVENDITORE { get; set; }

        public string SERVICE { get; set; }
        public string BRANCHE { get; set; }
        public string Z_CUTOFF { get; set; }
        public string Z_ULTFATT { get; set; }
        public string Z_TPULTFATT { get; set; }

        public string ZZONA_CLIMA { get; set; }
        public string OP_GG_AUSIL { get; set; }
        public string OP_GU_CATUSO { get; set; }
        public string OP_GU_STATOF { get; set; }

        public string OP_GU_CLAPRE { get; set; }
        public string OP_GU_AAEG { get; set; }
        public string OP_GQ_PROGAN { get; set; }
        public string OP_GF_COEFFC { get; set; }

        public string OP_GR_TIPPDR { get; set; }

        public string OP_GR_COREMI_1 { get; set; }
        public string OP_GR_COREMI_2 { get; set; }

        public string OP_GU_CLAMIS { get; set; }
        public string GU_CLIMP { get; set; }

        public string GQ_COANC0 { get; set; }
        public string GQ_COANDI { get; set; }
        public string OP_GG_DISDIS { get; set; }
        public string OP_GF_PCSZERO { get; set; }
        public string OP_GR_COL_TF { get; set; }
        public string OP_GR_COL_GR { get; set; }

        public string CASSA_MEZZOGIORNO { get; set; }
        public string OP_GG_MISGIOR { get; set; }
        public string OP_GG_DIRETTO { get; set; }
        public string IMPIANTO_OLD { get; set; }
        public string OP_GG_ACCMIS { get; set; }
        public string VA_GF_CG { get; set; }

        public string ZTIP_OFF { get; set; }

        public string DATE_FROM { get; set; }

        public string EXT_UI { get; set; }

        public string GRID_ID { get; set; }
        public string ZWGRUPPE { get; set; }
        public string EGERR_INFO { get; set; }
        public string MATNR { get; set; }
        public string KEYDATE { get; set; }
        public string APP_BIS { get; set; }
        public string ZEGERR_INFO { get; set; }
        public string NCAP_STANZVOR { get; set; }
        public string ZWFAKT_MIS { get; set; }
        public string EADAT_MIS { get; set; }
        public string NCAP_STANZVOR_CF1 { get; set; }
        public string ZWFAKT_CORF1 { get; set; }
        public string NCAP_STANZVOR_F2 { get; set; }
        public string ZWFAKT_CORF2 { get; set; }
        public string EADAT_COR { get; set; }

        public string EINZDAT { get; set; }

        public string AUSZDAT { get; set; }

        public string Z_MERCATO { get; set; }

        public string BUKRS { get; set; }
        public string VBEZ { get; set; }
        public string VREFER { get; set; }

        public string SPARTE { get; set; }

        public string GEMFAKT { get; set; }
        public string ABRSPERR { get; set; }
        //Required(ErrorMessage = "{0} is required")]
        // public string COKEY { get; set; }

        public string KOFIZ { get; set; }

        public string ZCANACQ { get; set; }
        public string Z_KAM { get; set; }
        public string Z_UTF { get; set; }

        public string Z_PRODOTTO { get; set; }

        public string Z_PRODOTTO_DESC { get; set; }

        public string Z_INIZIO { get; set; }

        public string Z_FINE { get; set; }
        public string Z_LISTINO { get; set; }
        public string Z_DEPOSITO { get; set; }
        public string ZIMP_DEPOSITO { get; set; }

        public string Z_TIPODEP { get; set; }
        public string Z_OFF_VERDE { get; set; }
        public string Z_MOT_CESS { get; set; }
        public string Z_MOT_ATT { get; set; }
        public string Z_CUP { get; set; }
        public string Z_CIG { get; set; }
        public string ZCIGF { get; set; }
        public string Z_NOTE { get; set; }
        public string RLEA_MIS { get; set; }
        public string REA_ADAT { get; set; }
        public string REA_ISTABLART { get; set; }
        public string RLEA_COR_F1 { get; set; }
        public string RLEA_COR_F2 { get; set; }
        public string REA_ADAT_COR { get; set; }
        public string REA_ISTABLART_COR { get; set; }
    }

    public class Util
    {
        public List<MainEle> LEle = new List<MainEle>();
        List<MainEle> prendi()
        {
            LEle.Take<MainEle>(1000);
            return LEle;
        }
    }
}
