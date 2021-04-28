using System;
using System.Collections.Generic;
using System.Text;

namespace Alperia_ISU_Lib
{
    public class GasBonus
    {
        public string COD_BONUS { get; set; }
        public string TIPO_COMUNICAZIONE { get; set; }
        public string COD_CAUSALE { get; set; }
        public string COD_PDR { get; set; }
        public string CF { get; set; }
        public string REGIME_COMPENSAZIONE { get; set; }
        public string ANNO_VALIDITA { get; set; }
        public string DATA_INIZIO { get; set; }
        public string DATA_FINE { get; set; }
        public string DATA_CESSAZIONE { get; set; }

        public static implicit operator GasBonus(XlsBonus xls)
        {
            GasBonus bonus = new GasBonus();
            bonus.COD_BONUS = xls.COD_BONUS;
            bonus.TIPO_COMUNICAZIONE = xls.TIPO_COMUNICAZIONE;
            bonus.REGIME_COMPENSAZIONE = xls.REGIME_COMPENSAZIONE;
            bonus.ANNO_VALIDITA = xls.ANNO_VALIDITA;
            bonus.COD_PDR = xls.COD_POD;
            bonus.COD_CAUSALE = xls.COD_CAUSALE;
            bonus.CF = xls.CF;
            bonus.DATA_CESSAZIONE = xls.DATA_CESSAZIONE;
            bonus.DATA_INIZIO = xls.DATA_INIZIO;
            bonus.DATA_FINE = xls.DATA_FINE;
            return bonus;
        }
    }

}
