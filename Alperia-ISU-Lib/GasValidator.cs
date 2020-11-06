using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

public class GasValidator : AbstractValidator<MainGas>
    {
    public List<ValidSemplici> llov { get; set; }
    public GasValidator(List<ValidSemplici> llov, string i_cutoff)
        {
        List<string> lovGridId = llov.Where(x => x.Campo == "GRID_ID").Select(x => x.Valore).ToList();
        List<string> lovZModinv = llov.Where(x => x.Campo == "Z_MODINV").Select(x => x.Valore).ToList();
        List<string> lovBpkind = llov.Where(x => x.Campo == "BPKIND").Select(x => x.Valore).ToList();
        List<string> lovService = llov.Where(x => x.Campo == "SERVICE").Select(x => x.Valore).ToList();
        List<string> lovZfreq = llov.Where(x => x.Campo == "ZFREQ").Select(x => x.Valore).ToList();
        List<string> lovMatnr = llov.Where(x => x.Campo == "MATNR").Select(x => x.Valore).ToList();
        List<string> lovZprodotto = llov.Where(x => x.Campo == "Z_PRODOTTO").Select(x => x.Valore).ToList();

        RuleFor(x => x.Z_CUTOFF).Equal(i_cutoff).WithMessage("Z_CUTOFF errata");

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

        RuleFor(x => x.CF_TAXNUM).NotEmpty().When(x => x.BU_TYPE == "1").WithMessage("TAXNUM assente in BU_TYPE 1");
        RuleFor(x => x.PI_TAXNUM).NotEmpty().When(x => x.BU_TYPE == "2" && (x.CF_TAXNUM[0] != '8' && x.CF_TAXNUM[0] != '9')).WithMessage("PIVA assente in BU_TYPE 2 e non Organizzazione");
        RuleFor(x => x.NAME_FIRST).NotEmpty().When(x => x.BU_TYPE == "1").WithMessage("NAME_FIRST assente in BU_TYPE 1");
        RuleFor(x => x.NAME_LAST).NotEmpty().When(x => x.BU_TYPE == "1").WithMessage("NAME_LAST assente in BU_TYPE 1");
        RuleFor(x => x.NAME_ORG1).NotEmpty().When(x => x.BU_TYPE == "2").WithMessage("NAME_ORG1 assente in BU_TYPE 2");
        RuleFor(x => x.IPA_CODE).NotEmpty().When(x => x.BPKIND == "Z005").WithMessage("IPA_CODE assente in cliente PA");
        RuleFor(x => x.IPA_BEGDA).NotEmpty().When(x => x.BPKIND == "Z005").WithMessage("IPA_BEGDA assente in cliente PA");

        RuleFor(x => x.CRM_ACCOUNT).NotEmpty().When(x => x.VKONA != null).WithMessage("Manca chiave SFDC - Account");

        RuleFor(x => x.BPKIND)
        .Must(x => lovBpkind.Contains(x))
        .WithMessage("BPKIND errato {PropertyValue}");

        RuleFor(x => x.Z_MODINV)
        .Must(x => lovZModinv.Contains(x))
        .WithMessage("Z_MODINV errato {PropertyValue}");

        RuleFor(x => x.IPA_CODE).Empty().When(x => x.BPKIND != "Z005").WithMessage("IPA_CODE valorizzato per NON PA");
        RuleFor(x => x.IPA_BEGDA).Equal("00000000").When(x => x.BPKIND != "Z005").WithMessage("IPA_BEGDA valorizzato per NON PA");

        RuleFor(x => x.DATASOTT).NotEmpty().When(x => x.EZAWE == "S").WithMessage("Data sottoscrizione non valorizzato");
        RuleFor(x => x.BANKL).NotEmpty().When(x => x.EZAWE == "S").WithMessage("Chiave Banca non valorizzato");
        RuleFor(x => x.BANKN).NotEmpty().When(x => x.EZAWE == "S").WithMessage("Conto non valorizzato");
        RuleFor(x => x.BKONT).NotEmpty().When(x => x.EZAWE == "S").WithMessage("Chiave Conto non valorizzato");
        RuleFor(x => x.IBAN).NotEmpty().When(x => x.EZAWE == "S").WithMessage("IBAN non valorizzato");

        //RuleFor(x => x.GRID_ID)
        //.Must(x => lovGridId.Contains(x))
        //.Must(x => x[1] == 'G')
        //.WithMessage("GRID_ID errato {PropertyValue}");

        RuleFor(x => x.SERVICE)
        .Must(x => lovService.Contains(x))
        .WithMessage("SERVICE errato {PropertyValue}");

        RuleFor(x => x.ZFREQ)
       .Must(x => lovZfreq.Contains(x))
       .WithMessage("ZFREQ errato {PropertyValue}");

        //RuleFor(x => DateTime.ParseExact(x.EINZDAT, "yyyyMMdd", CultureInfo.InvariantCulture))
        //    .GreaterThanOrEqualTo(x => DateTime.ParseExact(x.IM_AB, "yyyyMMdd", CultureInfo.InvariantCulture))
        //    .WithMessage("EINZDAT minore di IM_AB");

        //RuleFor(x => DateTime.ParseExact(x.EADAT_MIS, "yyyyMMdd", CultureInfo.InvariantCulture))
        //    .GreaterThanOrEqualTo(x => DateTime.ParseExact(x.IM_AB, "yyyyMMdd", CultureInfo.InvariantCulture))
        //    .WithMessage("EADAT_MIS minore di IM_AB");

        RuleFor(x => x.MATNR)
       .Must(x => lovMatnr.Contains(x))
       .WithMessage("MATNR errato {PropertyValue}");

        RuleFor(x => x.Z_PRODOTTO)
       .Must(x => lovZprodotto.Contains(x))
       .WithMessage("Z_PRODOTTO errato {PropertyValue}");

        RuleFor(x => x.ZEGERR_INFO).NotEmpty().When(x => x.ZWGRUPPE == "ZGCORR").WithMessage("ZEGERR_INFO assente per ZGCORR");
        RuleFor(x => x.NCAP_STANZVOR_CF1).NotEmpty().When(x => x.ZWGRUPPE == "ZGCORR").WithMessage("NCAP_STANZVOR_CF1 assente per ZGCORR");
        RuleFor(x => x.NCAP_STANZVOR_F2).NotEmpty().When(x => x.ZWGRUPPE == "ZGCORR").WithMessage("NCAP_STANZVOR_F2 assente per ZGCORR");
        RuleFor(x => x.ZWFAKT_CORF1).NotEmpty().When(x => x.ZWGRUPPE == "ZGCORR").WithMessage("ZWFAKT_CORF1 assente per ZGCORR");
        RuleFor(x => x.ZWFAKT_CORF2).NotEmpty().When(x => x.ZWGRUPPE == "ZGCORR").WithMessage("ZWFAKT_CORF2 assente per ZGCORR");
        RuleFor(x => x.EADAT_COR).NotEmpty().When(x => x.ZWGRUPPE == "ZGCORR").WithMessage("EADAT_COR assente per ZGCORR");
        RuleFor(x => x.RLEA_COR_F1).NotEmpty().When(x => x.ZWGRUPPE == "ZGCORR").WithMessage("RLEA_COR_F1 assente per ZGCORR");
        RuleFor(x => x.RLEA_COR_F2).NotEmpty().When(x => x.ZWGRUPPE == "ZGCORR").WithMessage("RLEA_COR_F2 assente per ZGCORR");
        RuleFor(x => x.REA_ADAT_COR).NotEmpty().When(x => x.ZWGRUPPE == "ZGCORR").WithMessage("REA_ADAT_COR assente per ZGCORR");
        RuleFor(x => x.REA_ISTABLART_COR).NotEmpty().When(x => x.ZWGRUPPE == "ZGCORR").WithMessage("REA_ISTABLART_COR assente per ZGCORR");
        RuleFor(x => x.NCAP_STANZVOR).NotEmpty().When(x => x.ZWGRUPPE == "ZGMIS" ||
                                                           x.ZWGRUPPE == "ZGPINT" ||
                                                           x.ZWGRUPPE == "ZGTINT" ||
                                                           x.ZWGRUPPE == "ZGCONS").WithMessage("NCAP_STANZVOR assente");
        RuleFor(x => x.ZWFAKT_MIS).NotNull().When(x => x.ZWGRUPPE == "ZGMIS" ||
                                                           x.ZWGRUPPE == "ZGPINT" ||
                                                           x.ZWGRUPPE == "ZGTINT" ||
                                                           x.ZWGRUPPE == "ZGCONS").WithMessage($"ZWFAKT_MIS assente");
        RuleFor(x => x.EADAT_MIS).NotEmpty().When(x => x.ZWGRUPPE == "ZGMIS" ||
                                                           x.ZWGRUPPE == "ZGPINT" ||
                                                           x.ZWGRUPPE == "ZGTINT" ||
                                                           x.ZWGRUPPE == "ZGCONS").WithMessage($"EADAT_MIS assente");
        RuleFor(x => x.RLEA_MIS).NotEmpty().When(x => x.ZWGRUPPE == "ZGMIS" ||
                                                           x.ZWGRUPPE == "ZGPINT" ||
                                                           x.ZWGRUPPE == "ZGTINT").WithMessage($"RLEA_MIS assente");
        RuleFor(x => x.REA_ADAT).NotEmpty().When(x => x.ZWGRUPPE == "ZGMIS" ||
                                                           x.ZWGRUPPE == "ZGPINT" ||
                                                           x.ZWGRUPPE == "ZGTINT").WithMessage($"REA_ADAT assente");
        RuleFor(x => x.REA_ISTABLART).NotEmpty().When(x => x.ZWGRUPPE == "ZGMIS" ||
                                                           x.ZWGRUPPE == "ZGPINT" ||
                                                           x.ZWGRUPPE == "ZGTINT").WithMessage($"REA_ISTABLART assente");
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

