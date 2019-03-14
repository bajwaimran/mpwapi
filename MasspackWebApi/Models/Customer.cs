using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasspackWebApi.Models
{
    public class CustomerArten
    {
        public string Kundenart { get; set; }
    }

    public class Customer
    {
        public int Oid { get; set; }
        public bool Selektion { get; set; }
        public int KDNr { get; set; }
        public string Suchname { get; set; }
        public string Anrede { get; set; }
        public string Titel { get; set; }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Name3 { get; set; }
        public string Strasse { get; set; }
        public string Nation { get; set; }
        public string PLZ { get; set; }
        public string Ort { get; set; }
        public string eMail { get; set; }
        public int Kundenart { get; set; }
        public bool kdauftrgesperrt { get; set; }

    }
}