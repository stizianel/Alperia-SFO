using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Alperia_ISU_Lib
{
    public class MainConf
    {   [Required]
        public string ROW_ID { get; set; }
        [Required]
        public string EXT_UI { get; set; }
        [Required]
        public string AB { get; set; }
        [Required]
        public string BIS { get; set; }
        public string COD_COMPONENTE { get; set; }
        [Required]
        public string CAMPO { get; set; }
        [Required]
        public string VALORE { get; set; }
    }
}
