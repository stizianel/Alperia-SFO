using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Alperia_ISU_Lib
{
    public class ConfEle
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
    public class ConfEleContext
    {
        private readonly IMongoDatabase _db;

        public ConfEleContext()
        {
            MongoClient client = new MongoClient();
            _db = client.GetDatabase("Alperia");
            _db.GetCollection<ConfEle>("ConfEle");
        }

        public IMongoCollection<ConfEle> ConfEleCollection => _db.GetCollection<ConfEle>("ConfEle");
    }
}
