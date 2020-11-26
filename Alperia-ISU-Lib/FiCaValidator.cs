using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alperia_ISU_Lib
{
    public class FiCaValidator : AbstractValidator<Docfica>
    {
        public List<ValidSemplici> llov { get; set; }
        public FiCaValidator(List<ValidSemplici> llov)
        {
            List<string> lovBlart = llov.Where(x => x.Campo == "BLART").Select(x => x.Valore).ToList();
            List<string> lovMwskz = llov.Where(x => x.Campo == "MWSKZ").Select(x => x.Valore).ToList();
            List<string> lovGsber = llov.Where(x => x.Campo == "GSBER").Select(x => x.Valore).ToList();
            List<string> lovBlocco = llov.Where(x => x.Campo == "INTEREST_LOCK").Select(x => x.Valore).ToList();
            List<string> lovLockrSol = llov.Where(x => x.Campo == "LOCKR_SOL").Select(x => x.Valore).ToList();
            List<string> lovLockrPag = llov.Where(x => x.Campo == "LOCKR_PAG").Select(x => x.Valore).ToList();
            List<string> lovStep = llov.Where(x => x.Campo == "STEP").Select(x => x.Valore).ToList();
            List<string> lovAugrd = llov.Where(x => x.Campo == "AUGRD ").Select(x => x.Valore.PadLeft(2,'0')).ToList();

            RuleFor(x => x.BLART)
            .Must(x => lovBlart.Contains(x))
            .WithMessage("BLART errato {PropertyValue}");

            When(x => x.MWSKZ != "", () =>
            {
                RuleFor(x => x.MWSKZ)
                .Must(x => lovMwskz.Contains(x))
                .WithMessage("MWSKZ errato {PropertyValue}");
            });

            When(x => x.GSBER != "", () =>
            {
                RuleFor(x => x.GSBER)
               .Must(x => lovGsber.Contains(x))
               .WithMessage("GSBER errato {PropertyValue}");
            });

            When(x => x.BLOCCO_INTERESSI != "", () =>
            {
                RuleFor(x => x.BLOCCO_INTERESSI)
               .Must(x => lovBlocco.Contains(x))
               .WithMessage("BLOCCO INTERESSI errato {PropertyValue}");
            });

            When(x => x.LOCKR_SOL != "", () =>
            {
                RuleFor(x => x.LOCKR_SOL)
           .Must(x => lovLockrSol.Contains(x))
           .WithMessage("LOCKR_SOL errato {PropertyValue}");
            });

            When(x => x.LOCKR_PAG != "", () =>
            {
                RuleFor(x => x.LOCKR_PAG)
           .Must(x => lovLockrPag.Contains(x))
           .WithMessage("LOCKR_PAG errato {PropertyValue}");
            });

            When(x => x.STEP != "", () =>
            {
                RuleFor(x => x.STEP)
           .Must(x => lovStep.Contains(x))
           .WithMessage("STEP errato {PropertyValue}");
            });

            When(x => x.AUGRD != "", () =>
            {
                RuleFor(x => x.AUGRD)
           .Must(x => lovAugrd.Contains(x))
           .WithMessage("AUGRD errato {PropertyValue}");
            });
        }

    }
}
