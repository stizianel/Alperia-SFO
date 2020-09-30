using FluentValidation;

    public class GasValidator : AbstractValidator<MainGas>
    {
        public GasValidator()
        {
            RuleFor(x => x.CF_TAXNUM).NotEmpty().When(x => x.BU_TYPE == "1").WithMessage("TAXNUM assente in BU_TYPE 1");
            RuleFor(x => x.PI_TAXNUM).NotEmpty().When(x => x.BU_TYPE == "2").WithMessage("PIVA assente in BU_TYPE 2");

        }
    }

