using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alperia_ISU_Lib
{
    public class ConfValidator : AbstractValidator<MainConf>
    {
        public ConfValidator(string i_ab)
        {
            RuleFor(x => x.AB).Equal(i_ab).WithMessage("Data inizio componente non valida");
        }
    }
}
