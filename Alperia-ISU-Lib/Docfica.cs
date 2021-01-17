using MongoDB.Driver;
using System;
using System.ComponentModel.DataAnnotations;

namespace Alperia_ISU_Lib
{
    public class Docfica
    {
        [Required(ErrorMessage = "{0} is required")]
        public string SRC_SYSTEM { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string RUN_ID { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string ROW_ID { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string KEY_EXT { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string EXT_UI { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string BPEXT { get; set; }
        public string CF_TAXNUM { get; set; }
        public string PI_TAXNUM { get; set; }
        public string FL_NORIFCONTRA { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string BLART { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string BLDAT { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string XBLNR { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string BETRW { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string FAEDN { get; set; }
        [Required(ErrorMessage = "{0} is required")]
        public string HKONT { get; set; }
        public string VKONT { get; set; }
        public string VTREF { get; set; }
        public string PRCTR { get; set; }
        public string OPTXT { get; set; }
        public string VALUT { get; set; }
        public string RFZAS { get; set; }
        public string CL_RFZAS { get; set; }
        public string BLOCCO_INTERESSI { get; set; }
        public string AB_COMPE { get; set; }
        public string BIS_COMPE { get; set; }
        public string AUGST { get; set; }
        public string LOCKR_SOL { get; set; }
        public string TDATE_SOL { get; set; }
        public string LOCKR_PAG { get; set; }
        public string TDATE_PAG { get; set; }
        public string LOCKR_PAR { get; set; }
        public string TDATE_PAR { get; set; }
        public string AUGRD { get; set; }
        public string ADD_REFOBJ { get; set; }
        public string PYMET { get; set; }
        public string GSBER { get; set; }
        public string ZNUM_PIANO { get; set; }
        public string ZSCAD_RATA { get; set; }
        public string AUSDT { get; set; }
        public string STEP { get; set; }
        public string ID_SOLLECITO { get; set; }
        public string LOTTO_AFFIDO { get; set; }
        public string ID_STATO { get; set; }
        public string FISC_AFFIDO { get; set; }
        public string IMPORTO_IVA { get; set; }
        public string SBASH { get; set; }
        public string SCTAX { get; set; }
        public string MWSKZ { get; set; }
        public string FLG_REC_IVA { get; set; }
        public string SPERZ { get; set; }
        public string STUDT { get; set; }
    }

    public class DocficaContext
    {
        private readonly IMongoDatabase _db;

        public DocficaContext()
        {
            MongoClient client = new MongoClient();
            _db = client.GetDatabase("Alperia");
            _db.GetCollection<MainEle>("Docfica");
        }

        public IMongoCollection<Docfica> DocficaCollection => _db.GetCollection<Docfica>("Docfica");
    }
}
