using FluentValidation;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

public class EleValidator : AbstractValidator<MainEle>
    {
    public List<ValidSemplici> llov { get; set; }
    public EleValidator(List<ValidSemplici> llov, string i_cutoff)
        {
        List<string> lovGridId = llov.Where(x => x.Campo == "GRID_ID").Select(x => x.Valore).ToList();
        List<string> lovZModinv = llov.Where(x => x.Campo == "Z_MODINV").Select(x => x.Valore).ToList();
        List<string> lovBpkind = llov.Where(x => x.Campo == "BPKIND").Select(x => x.Valore).ToList();
        List<string> lovZfreq = llov.Where(x => x.Campo == "ZFREQ").Select(x => x.Valore).ToList();
        List<string> lovCodiceAccise = llov.Where(x => x.Campo == "CODICE_ACCISE").Select(x => x.Valore).ToList();
        List<string> lovMatnr = llov.Where(x => x.Campo == "MATNR").Select(x => x.Valore).ToList();
        List<string> lovZprodotto = llov.Where(x => x.Campo == "Z_PRODOTTO").Select(x => x.Valore).ToList();
        
        RuleFor(x => x.Z_CUTOFF).Equal(i_cutoff).WithMessage("Z_CUTOFF errata");

        RuleFor(x => x.CF_TAXNUM).NotEmpty().When(x => x.BU_TYPE == "1").WithMessage("TAXNUM assente in BU_TYPE 1");
        RuleFor(x => x.PI_TAXNUM).NotEmpty().When(x => x.BU_TYPE == "2" && (x.CF_TAXNUM[0] != '8' && x.CF_TAXNUM[0] != '9')).WithMessage("PIVA assente in BU_TYPE 2 e non Organizzazione");
        RuleFor(x => x.NAME_FIRST).NotEmpty().When(x => x.BU_TYPE == "1").WithMessage("NAME_FIRST assente in BU_TYPE 1");
        RuleFor(x => x.NAME_LAST).NotEmpty().When(x => x.BU_TYPE == "1").WithMessage("NAME_LAST assente in BU_TYPE 1");
        RuleFor(x => x.NAME_ORG1).NotEmpty().When(x => x.BU_TYPE == "2").WithMessage("NAME_ORG1 assente in BU_TYPE 2");
        RuleFor(x => x.IPA_CODE).NotEmpty().When(x => x.BPKIND == "Z005").WithMessage("IPA_CODE assente in cliente PA");

        RuleFor(x => x.CRM_ACCOUNT).NotEmpty().When(x => x.VKONA != null).WithMessage("Manca chiave SFDC - Account");

        When(x => x.TEL_NUMBER1 != "", () =>
        {
            RuleFor(x => x.TEL_NUMBER1)
           .Must(x => HasValidTelnumber(x))
           .WithMessage("Campo TEL_NUMBER1 con valori non previsti {PropertyValue}");
        });


        When(x => x.TEL_NUMBER2 != "", () =>
        {
            RuleFor(x => x.TEL_NUMBER2)
           .Must(x => HasValidTelnumber(x))
           .WithMessage("Campo TEL_NUMBER2 con valori non previsti {PropertyValue}");
        });

        RuleFor(x => x.IPA_BEGDA).NotEmpty().When(x => x.BPKIND == "Z005").WithMessage("IPA_BEGDA assente in cliente PA");
        RuleFor(x => x.BPKIND)
        .Must(x => lovBpkind.Contains(x))
        .WithMessage("BPKIND errato {PropertyValue}");

        RuleFor(x => x.Z_MODINV)
        .Must(x => lovZModinv.Contains(x))
        .WithMessage("Z_MODINV errato {PropertyValue}");

        RuleFor(x => x.ZFREQ)
       .Must(x => lovZfreq.Contains(x))
       .WithMessage("ZFREQ errato {PropertyValue}");

        RuleFor(x => x.DATASOTT).NotEmpty().When(x => x.EZAWE == "S").WithMessage("Data sottoscrizione non valorizzato");
        RuleFor(x => x.BANKL).NotEmpty().When(x => x.EZAWE == "S").WithMessage("Chiave Banca non valorizzato");
        RuleFor(x => x.BANKN).NotEmpty().When(x => x.EZAWE == "S").WithMessage("Conto non valorizzato");
        RuleFor(x => x.BKONT).NotEmpty().When(x => x.EZAWE == "S").WithMessage("Chiave Conto non valorizzato");
        RuleFor(x => x.IBAN).NotEmpty().When(x => x.EZAWE == "S").WithMessage("IBAN non valorizzato");

        //RuleFor(x => x.OP_ER_RESI_TF).NotEmpty().When(x => x.OP_ER_TIPOUT_TF == "DO").WithMessage("Residente non valorizzato");
        
        RuleFor(x => x.GRID_ID)
        .Must(x => lovGridId.Contains(x))
        .Must(x => x[1] == 'E')
        .WithMessage("GRID_ID errato {PropertyValue}");

        RuleFor(x => x.OP_ER_OPZAEEG).Equal("TDR")
            .When(x => x.OP_ER_TIPOUT_TF == "DO")
            .When(x => x.OP_ER_RESI_TF == "Y")
            .WithMessage("TDR errato");

        RuleFor(x => x.OP_ER_OPZAEEG).Equal("TDNR")
            .When(x => x.OP_ER_TIPOUT_TF == "DO")
            .When(x => x.OP_ER_RESI_TF == "N")
            .WithMessage("TDNR errato");

        RuleFor(x => x.OP_ER_OPZAEEG).Equal("BTA1")
            .When(x => x.OP_ER_TIPOUT_TF == "AU")
            .When(x => float.Parse(x.OP_ED_POTDIS) <= 16.5)
            .When(x => float.Parse(x.OP_ED_POTCON) > 1.5)
            .WithMessage("BTA1 errato");

        RuleFor(x => x.OP_ER_OPZAEEG).Equal("BTA2")
            .When(x => x.OP_ER_TIPOUT_TF == "AU")
            .When(x => x.OP_ER_LIVTEN_TF == "BT")
            .When(x => float.Parse(x.OP_ED_POTDIS) <= 16.5)
            .When(x => float.Parse(x.OP_ED_POTCON) > 1.5 && float.Parse(x.OP_ED_POTCON) <= 3.0)
            .WithMessage("BTA2 errato");

        RuleFor(x => x.OP_ER_OPZAEEG).Equal("BTA3")
            .When(x => x.OP_ER_TIPOUT_TF == "AU")
            .When(x => x.OP_ER_LIVTEN_TF == "BT")
            .When(x => float.Parse(x.OP_ED_POTDIS) <= 16.5)
            .When(x => float.Parse(x.OP_ED_POTCON) > 3.0 && float.Parse(x.OP_ED_POTCON) <= 6.0)
            .WithMessage("BTA3 errato");

        RuleFor(x => x.OP_ER_OPZAEEG).Equal("BTA4")
           .When(x => x.OP_ER_TIPOUT_TF == "AU")
           .When(x => x.OP_ER_LIVTEN_TF == "BT")
           .When(x => float.Parse(x.OP_ED_POTDIS) <= 16.5)
           .When(x => float.Parse(x.OP_ED_POTCON) > 6.0 && float.Parse(x.OP_ED_POTCON) <= 10.0)
           .WithMessage("BTA4 errato");

        RuleFor(x => x.OP_ER_OPZAEEG).Equal("BTA5")
           .When(x => x.OP_ER_TIPOUT_TF == "AU")
           .When(x => x.OP_ER_LIVTEN_TF == "BT")
           .When(x => float.Parse(x.OP_ED_POTDIS) <= 16.5)
           .When(x => float.Parse(x.OP_ED_POTCON) > 10.0 && float.Parse(x.OP_ED_POTCON) <= 16.5)
           .WithMessage("BTA5 errato");

        RuleFor(x => x.OP_ER_OPZAEEG).Equal("BTA6")
           .When(x => x.OP_ER_TIPOUT_TF == "AU")
           .When(x => x.OP_ER_LIVTEN_TF == "BT")
           .When(x => float.Parse(x.OP_ED_POTDIS) > 16.5)
           .WithMessage(x => $"BTA6 errata: POTDIS: {x.OP_ED_POTDIS} POTCON: {x.OP_ED_POTCON}");

        RuleFor(x => x.OP_ER_OPZAEEG).Equal("BTIP")
            .When(x => x.OP_ER_TIPOUT_TF == "IP")
            .When(x => x.OP_ER_LIVTEN_TF == "BT")
            .WithMessage("BTIP errato");

        RuleFor(x => x.OP_ER_OPZAEEG).Equal("MTIP")
            .When(x => x.OP_ER_TIPOUT_TF == "IP")
            .When(x => x.OP_ER_LIVTEN_TF == "MT")
            .WithMessage("MTIP errato");

        RuleFor(x => x.OP_ER_OPZAEEG).Equal("MTA1")
            .When(x => x.OP_ER_TIPOUT_TF == "AU")
            .When(x => x.OP_ER_LIVTEN_TF == "MT")
            .When(x => int.Parse(x.ZTENS) > 1000)
            .When(x => float.Parse(x.OP_ED_POTDIS) <= 100)
            .WithMessage("MTA1 errato");

        RuleFor(x => x.OP_ER_OPZAEEG).Equal("MTA2")
            .When(x => x.OP_ER_TIPOUT_TF == "AU")
            .When(x => x.OP_ER_LIVTEN_TF == "MT")
            .When(x => int.Parse(x.ZTENS) > 1000)
            .When(x => float.Parse(x.OP_ED_POTDIS) > 100 && float.Parse(x.OP_ED_POTDIS) <= 500)
            .WithMessage("MTA2 errato");

        RuleFor(x => x.OP_ER_OPZAEEG).Equal("MTA3")
            .When(x => x.OP_ER_TIPOUT_TF == "AU")
            .When(x => x.OP_ER_LIVTEN_TF == "MT")
            .When(x => int.Parse(x.ZTENS) > 1000)
            .When(x => float.Parse(x.OP_ED_POTDIS) > 500)
            .WithMessage("MTA3 errato");

        RuleFor(x => x.OP_ER_OPZAEEG).Equal("ALTA")
            .When(x => x.OP_ER_TIPOUT_TF == "AU")
            .When(x => x.OP_ER_LIVTEN_TF == "AT")
            .When(x => int.Parse(x.ZTENS) > 30000 && int.Parse(x.ZTENS) <= 219999)
            .WithMessage("ALTA errato");

        RuleFor(x => x.OP_ER_OPZAEEG).Equal("AAT1")
            .When(x => x.OP_ER_TIPOUT_TF == "AU")
            .When(x => x.OP_ER_LIVTEN_TF == "AAT1")
            .When(x => int.Parse(x.ZTENS) >= 380000)
            .WithMessage("AAT2 errato");

        RuleFor(x => x.OP_ER_OPZAEEG).Equal("AAT2")
            .When(x => x.OP_ER_TIPOUT_TF == "AU")
            .When(x => x.OP_ER_LIVTEN_TF == "AAT2")
            .When(x => int.Parse(x.ZTENS) >= 220000 && int.Parse(x.ZTENS) <= 379999)
            .WithMessage("AAT1 errato");

        RuleFor(x => x.KONDIGR_ER_ACC).Equal("ACCDR1")
            .When(x => x.OP_ER_TIPOUT_TF == "DO")
            .When(x => float.Parse(x.OP_ED_POTCON) <= 1.5)
            .When(x => x.OP_ER_RESI_TF == "Y")
            .When(x => x.TARIFART_ER_ACC == "E_ACDM")
            .When(x => x.CODICE_ACCISE == "ACCDOM")
            .WithMessage("ACCDR1 errato {PropertyValue}");

        RuleFor(x => x.KONDIGR_ER_ACC).Equal("ACCDR2")
            .When(x => x.OP_ER_TIPOUT_TF == "DO")
            .When(x => float.Parse(x.OP_ED_POTCON) > 1.5 && float.Parse(x.OP_ED_POTCON) <= 3.0)
            .When(x => x.OP_ER_RESI_TF == "Y")
            .When(x => x.TARIFART_ER_ACC == "E_ACDM")
            .When(x => x.CODICE_ACCISE == "ACCDOM")
            .WithMessage("ACCDR2 errato {PropertyValue}");

        RuleFor(x => x.KONDIGR_ER_ACC).Equal("ACCDR3")
            .When(x => x.OP_ER_TIPOUT_TF == "DO")
            .When(x => float.Parse(x.OP_ED_POTCON) > 1.5 && float.Parse(x.OP_ED_POTCON) <= 3.0)
            .When(x => x.OP_ER_RESI_TF == "Y")
            .When(x => x.TARIFART_ER_ACC == "E_ACDM")
            .When(x => x.CODICE_ACCISE == "ACCDOM")
            .WithMessage("ACCDR3 errato {PropertyValue}");

        RuleFor(x => x.KONDIGR_ER_ACC).Equal("ACCDNR")
            .When(x => x.OP_ER_TIPOUT_TF == "DO")
            .When(x => x.OP_ER_RESI_TF == "N")
            .When(x => x.TARIFART_ER_ACC == "E_ACDM")
            .When(x => x.CODICE_ACCISE == "ACCDOM")
            .WithMessage("ACCDNR errato {PropertyValue}");

        RuleFor(x => x.KONDIGR_ER_ACC).Equal("ORD")
            .When(x => x.OP_ER_TIPOUT_TF == "IP")
            .When(x => x.TARIFART_ER_ACC == "E_ACAU")
            .When(x => x.CODICE_ACCISE == "ACCORD")
            .WithMessage("ORD errato {PropertyValue}");

        RuleFor(x => x.KONDIGR_ER_ACC).Equal("ORD")
            .When(x => x.OP_ER_TIPOUT_TF == "AU")
            .When(x => x.TARIFART_ER_ACC == "E_ACAU")
            .When(x => x.CODICE_ACCISE == "ACCORD")
            .WithMessage("ORD errato {PropertyValue}");

        RuleFor(x => x.CODICE_ACCISE)
        .Must(x => lovCodiceAccise.Contains(x))
        .WithMessage("CODICE_ACCISE errato {PropertyValue}");

        RuleFor(x => x.MATNR)
      .Must(x => lovMatnr.Contains(x))
      .WithMessage("MATNR errato {PropertyValue}");

        RuleFor(x => x.Z_PRODOTTO)
       .Must(x => lovZprodotto.Contains(x))
       .WithMessage("Z_PRODOTTO errato {PropertyValue}");

        //RuleFor(x => DateTime.ParseExact(x.IM_AB, "yyyyMMdd", CultureInfo.InvariantCulture)).LessThan(x => DateTime.ParseExact(x.EINZDAT, "yyyyMMdd", CultureInfo.InvariantCulture)).WithMessage("Data contratto inferiore data impianto");

        RuleFor(x => DateTime.ParseExact(x.EINZDAT, "yyyyMMdd", CultureInfo.InvariantCulture))
            .GreaterThanOrEqualTo(x => DateTime.ParseExact(x.IM_AB, "yyyyMMdd", CultureInfo.InvariantCulture))
            .WithMessage("EINZDAT minore di IM_AB");

        RuleFor(x => x.IPA_CODE).Empty().When(x => x.BPKIND != "Z005").WithMessage("IPA_CODE valorizzato per NON PA");
        RuleFor(x => x.IPA_BEGDA).Equal("00000000").When(x => x.BPKIND != "Z005").WithMessage("IPA_BEGDA valorizzato per NON PA");

        RuleFor(x => x.NCAP_STANZVOR).NotEmpty().When(x => x.FLAG_ATTF0 == "X").WithMessage("NCAP_STANZVOR assente con flag_attf0");
        RuleFor(x => x.NCIR_STANZVOR).NotEmpty().When(x => x.FLAG_REAF0 == "X").WithMessage("NCIR_STANZVOR assente con flag_reaf0");
        RuleFor(x => x.NCPP_STANZVOR).NotEmpty().When(x => x.FLAG_POTF0 == "X").WithMessage("NCPP_STANZVOR assente con flag_potf0");

        //RuleFor(x => x.RLPOT_PREL_F1).NotEmpty().When(x => x.ZWGRUPPE == "ZLETF3").WithMessage("ZWGRUPPE='ZLETF3' e RLPOT_PREL_F1 vuoto");
        //RuleFor(x => x.RLPOT_PREL_F2).NotEmpty().When(x => x.ZWGRUPPE == "ZLETF3").WithMessage("ZWGRUPPE='ZLETF3' e RLPOT_PREL_F2 vuoto");
        //RuleFor(x => x.RLPOT_PREL_F3).NotEmpty().When(x => x.ZWGRUPPE == "ZLETF3").WithMessage("ZWGRUPPE='ZLETF3' e RLPOT_PREL_F3 vuoto");
        //RuleFor(x => x.RLREA_PREL_F1).NotEmpty().When(x => x.ZWGRUPPE == "ZLETF3").WithMessage("ZWGRUPPE='ZLETF3' e RLREA_PREL_F1 vuoto");
        //RuleFor(x => x.RLREA_PREL_F2).NotEmpty().When(x => x.ZWGRUPPE == "ZLETF3").WithMessage("ZWGRUPPE='ZLETF3' e RLREA_PREL_F2 vuoto");
        //RuleFor(x => x.RLREA_PREL_F3).NotEmpty().When(x => x.ZWGRUPPE == "ZLETF3").WithMessage("ZWGRUPPE='ZLETF3' e RLREA_PREL_F3 vuoto");
    }

    private bool HasValidTelnumber(string tn)
    {
        var lowercase = new Regex("[a-z]+");
        var uppercase = new Regex("[A-Z]+");
        var digit = new Regex("^[0-9]*$");
        var acn = new Regex("^[[:digit:]]+$");

        return (digit.IsMatch(tn));
    }

}

