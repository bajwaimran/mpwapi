using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasspackWebApi.Models
{
    public class Order
    {
        public bool Status { get; set; }
        public bool Fertig { get; set; }
        public DateTime Datum { get; set; }
        public string AuftragsNr { get; set; }
        public string Zusatzangabe { get; set; }
        public ICollection<OrderKunden> Bestellung_BestellKunden_XPColl { get; set; }
        public ICollection<OrderAuftraege> Bestellung_BestellAuftraege_XPColl { get; set; }


    }
    public class OrderKunden
    {
        public int KDNr { get; set; }
        public Order Bestellung { get; set; }
        public ICollection<OrderArtikel> BestellKunden_BestellArtikel_XPColl { get; set; }

    }
    public class OrderArtikel
    {
        public string ArtikelNr { get; set; }
        public string Bezeichnung { get; set; }
        public decimal Stueckzahl { get; set; }
        public Order Bestellung { get; set; }
        public Art Artikel { get; set; }
        public string mboid { get; set; }
        public int AuftragsNrKW { get; set; }
        public OrderKunden BestellKunden { get; set; }
    }
    public class OrderAuftraege
    {
        public string mboid { get; set; }
        public string AuftragsNr { get; set; }
        public Order Bestellung { get; set; }
    }

    public class Art
    {
        public int Oid { get; set; }
        public int ArtNr { get; set; }
        public int Bestand { get; set; }
        public string Bezeichnung { get; set; }
        public string Artikeltext1 { get; set; }
        public string Artikeltext2 { get; set; }
        public string Artikeltext3 { get; set; }
        public decimal Stueckzahl { get; set; }
        public decimal Verpackungseinheit { get; set; }
        public decimal StueckzahlLieferbar { get; set; }


    }
}