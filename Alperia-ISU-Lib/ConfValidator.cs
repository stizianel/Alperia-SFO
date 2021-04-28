using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Alperia_ISU_Lib
{
    public class ConfValidator : AbstractValidator<ConfEle>
    {
        public List<CompVend> lcomp { get; set; }
        public ConfValidator(string i_ab, List<CompVend> lcomp)
        {
            List<string> lovCompE = lcomp.Where(x => x.COMMODITY == "E").Select(x => x.COD_COMPONENTE).ToList();
            List<string> lovCompG = lcomp.Where(x => x.COMMODITY == "G").Select(x => x.COD_COMPONENTE).ToList();
            RuleFor(x => x.AB).Equal(i_ab).WithMessage("Data inizio componente non valida");
        }
    }
}
