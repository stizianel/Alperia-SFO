using FluentValidation;
using System.Collections.Generic;
using System.Linq;

public class EleValidator : AbstractValidator<MainEle>
    {
    public List<ValidSemplici> llov { get; set; }
    public EleValidator(List<ValidSemplici> llov)
        {
        List<string> lovGridId = llov.Where(x => x.Campo == "GRID_ID").Select(x => x.Valore).ToList();
        List<string> lovZModinv = llov.Where(x => x.Campo == "Z_MODINV").Select(x => x.Valore).ToList();
        List<string> lovBpkind = llov.Where(x => x.Campo == "BPKIND").Select(x => x.Valore).ToList();

        RuleFor(x => x.CF_TAXNUM).NotEmpty().When(x => x.BU_TYPE == "1").WithMessage("TAXNUM assente in BU_TYPE 1");
        RuleFor(x => x.PI_TAXNUM).NotEmpty().When(x => x.BU_TYPE == "2" && (x.CF_TAXNUM[0] != '8' && x.CF_TAXNUM[0] != '9')).WithMessage("PIVA assente in BU_TYPE 2 e non Organizzazione");
        RuleFor(x => x.NAME_FIRST).NotEmpty().When(x => x.BU_TYPE == "1").WithMessage("NAME_FIRST assente in BU_TYPE 1");
        RuleFor(x => x.NAME_LAST).NotEmpty().When(x => x.BU_TYPE == "1").WithMessage("NAME_LAST assente in BU_TYPE 1");
        RuleFor(x => x.NAME_ORG1).NotEmpty().When(x => x.BU_TYPE == "2").WithMessage("NAME_ORG1 assente in BU_TYPE 2");
        RuleFor(x => x.IPA_CODE).NotEmpty().When(x => x.BPKIND == "Z005").WithMessage("IPA_CODE assente in cliente PA");

        RuleFor(x => x.IPA_BEGDA).NotEmpty().When(x => x.BPKIND == "Z005").WithMessage("IPA_BEGDA assente in cliente PA");
        RuleFor(x => x.BPKIND)
        .Must(x => lovBpkind.Contains(x))
        .WithMessage("BPKIND errato {PropertyValue}");

        RuleFor(x => x.Z_MODINV)
        .Must(x => lovZModinv.Contains(x))
        .WithMessage("Z_MODINV errato {PropertyValue}");

        RuleFor(x => x.DATASOTT).NotEmpty().When(x => x.EZAWE == "S").WithMessage("Data sottoscrizione non valorizzato");
        RuleFor(x => x.BANKL).NotEmpty().When(x => x.EZAWE == "S").WithMessage("Chiave Banca non valorizzato");
        RuleFor(x => x.BANKN).NotEmpty().When(x => x.EZAWE == "S").WithMessage("Conto non valorizzato");
        RuleFor(x => x.BKONT).NotEmpty().When(x => x.EZAWE == "S").WithMessage("Chiave Conto non valorizzato");
        RuleFor(x => x.IBAN).NotEmpty().When(x => x.EZAWE == "S").WithMessage("IBAN non valorizzato");

        RuleFor(x => x.OP_ER_RESI_TF).NotEmpty().When(x => x.OP_ER_TIPOUT_TF == "DO").WithMessage("Residente non valorizzato");
        
        RuleFor(x => x.GRID_ID)
        .Must(x => lovGridId.Contains(x))
        .WithMessage("GRID_ID errato {PropertyValue}");

        RuleFor(x => x.NCAP_STANZVOR).NotEmpty().When(x => x.FLAG_ATTF0 == "X").WithMessage("NCAP_STANZVOR assente con flag_attf0");
        RuleFor(x => x.NCIR_STANZVOR).NotEmpty().When(x => x.FLAG_REAF0 == "X").WithMessage("NCIR_STANZVOR assente con flag_reaf0");
        RuleFor(x => x.NCPP_STANZVOR).NotEmpty().When(x => x.FLAG_POTF0 == "X").WithMessage("NCPP_STANZVOR assente con flag_potf0");
    }

    }

