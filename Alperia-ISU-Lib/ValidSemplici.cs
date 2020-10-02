using CsvHelper;
using System;
using System.Collections.Generic;
using System.Linq;

public class ValidSemplici
    {
        public string Campo { get; set; }
        public string Valore { get; set; }
        public string Descrizione { get; set; }

    public static List<ValidSemplici> LoadValidSemplici(CsvReader csv)
    {
        try
        {
            var zEle = csv.GetRecords<ValidSemplici>().ToList();
            return zEle;
        }
        catch (Exception)
        {
            Console.WriteLine("Errore su file {0}", "ValidazioniSemplici");
            throw;
        }
    }
}

