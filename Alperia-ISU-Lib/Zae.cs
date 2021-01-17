using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Alperia_ISU_Lib
{
    public class Zae
    {
        public ObjectId Id { get; set; }
        public string Mandt { get; set; }
        public string Vtref { get; set; }
        public string Gpart { get; set; }
        public string CF { get; set; }
        public string PI { get; set; }
        public string ExtUi { get; set; }
        public string Auszdat { get; set; }
    }
    public class ZaeContext
    {
        private readonly IMongoDatabase _db;

        public ZaeContext()
        {
            MongoClient client = new MongoClient();
            _db = client.GetDatabase("Alperia");
            _db.GetCollection<Zae>("Zae");
        }

        public IMongoCollection<Zae> ZaeCollection => _db.GetCollection<Zae>("Zae");
    }

}
