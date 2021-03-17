using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alperia_ISU_Lib
{
    public class Zs4hAccise
    {
        public string MANDT { get; set; }
        public string SPARTE { get; set; }
        public string POD { get; set; }
        public string BP { get; set; }
        public string ANLAGE { get; set; }
        public string COD_ESENZIONE { get; set; }
        public string BIS { get; set; }
        public string AB { get; set; }
        public string VERTRAG { get; set; }
        public string CITY_CODE { get; set; }
        public string NAME_ORG1 { get; set; }
        public string GRID_ID { get; set; }
        public string COD_ACC_OBBL { get; set; }
        public string PIVA { get; set; }
        public string PERC_ESENZ { get; set; }
        public string QUAN_ESENZ { get; set; }
        public string ASS_ACC { get; set; }
        public string ESE_ACC { get; set; }
        public string ASS_AAC { get; set; }
        public string ESE_AAC { get; set; }
        public string ASS_IEC { get; set; }
        public string ESE_IEC { get; set; }
        public string ASA_IEC { get; set; }
        public string ERDAT { get; set; }
        public string ERTIM { get; set; }
        public string ERNAM { get; set; }


    }
    public class Zs4hAcciseContext
    {
        private readonly IMongoDatabase _db;

        public Zs4hAcciseContext()
        {
            MongoClient client = new MongoClient();
            _db = client.GetDatabase("Alperia");
            _db.GetCollection<Zs4hAccise>("Zs4hAccise");
        }

        public IMongoCollection<Zs4hAccise> AcciseCollection => _db.GetCollection<Zs4hAccise>("Zs4hAccise");
    }
}
