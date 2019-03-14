using BestellErfassung.DomainObjects;
using BestellErfassung.DomainObjects.Artikel;
using BestellErfassung.DomainObjects.Bestellungen;
using MasspackWebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MasspackWebApi.Helpers
{
    public static class Mapper
    {
        public static Order GetOrder(Bestellung obj)
        {
            var bestellKundens = obj.Bestellung_BestellKunden_XPColl;
            var bestellAuftraege = obj.Bestellung_BestellAuftraege_XPColl;
            var order = new Order
            {
                Zusatzangabe = obj.Zusatzangabe,
                AuftragsNr = obj.AuftragsNr,
                Datum = obj.Datum,
                Fertig = obj.Fertig,
                Status = obj.Status,
                Bestellung_BestellAuftraege_XPColl = GetListOfOrderAuf(obj.Bestellung_BestellAuftraege_XPColl.ToList()),
                Bestellung_BestellKunden_XPColl = GetListOfOrderKunden(obj.Bestellung_BestellKunden_XPColl.ToList())
            };
            return order;
        }

        public static List<OrderAuftraege> GetListOfOrderAuf(List<BestellAuftraege> auf)
        {
            List<OrderAuftraege> model = new List<OrderAuftraege>();
            foreach (BestellAuftraege item in auf)
            {
                var order = GetOrder(item.Bestellung);
                model.Add(new OrderAuftraege
                {
                    AuftragsNr = item.AuftragsNr,
                    Bestellung = order,
                    mboid = item.mboid
                });
            }

            return model;
        }
        public static List<OrderArtikel> GetListOfOrderArtikel(IEnumerable<BestellArtikel> artikels)
        {
            List<OrderArtikel> model = new List<OrderArtikel>();
            foreach (BestellArtikel item in artikels)
            {
                model.Add(new OrderArtikel
                {
                    //Artikel = item,
                    ArtikelNr = item.ArtikelNr,
                    AuftragsNrKW = item.AuftragsNrKW,
                    BestellKunden = GetOrderKunden(item.BestellKunden),
                    Bestellung = GetOrder(item.Bestellung),
                    Bezeichnung = item.Bezeichnung,
                    mboid = item.mboid,
                    Stueckzahl = item.Stueckzahl
                });
            }

            return model;
        }
        public static OrderKunden GetOrderKunden(BestellKunden bistellkunden)
        {
            OrderKunden model = new OrderKunden
            {
                Bestellung = GetOrder(bistellkunden.Bestellung),
                KDNr = bistellkunden.KDNr,
                BestellKunden_BestellArtikel_XPColl = GetListOfOrderArtikel(bistellkunden.BestellKunden_BestellArtikel_XPColl)
            };
            return model;
        }
        public static List<OrderKunden> GetListOfOrderKunden(List<BestellKunden> kundens)
        {
            List<BestellArtikel> bestellArtikels = new List<BestellArtikel>();
            List<OrderKunden> model = new List<OrderKunden>();
            foreach (BestellKunden item in kundens)
            {
                bestellArtikels = item.BestellKunden_BestellArtikel_XPColl.ToList();
                model.Add(new OrderKunden
                {
                    Bestellung = GetOrder(item.Bestellung),
                    BestellKunden_BestellArtikel_XPColl = GetListOfOrderArtikel(bestellArtikels),
                    KDNr = item.KDNr
                });
            }


            return model;
        }
        public static Art GetArtikel(Artikelstamm item)
        {
            var model = new Art
            {
                Artikeltext1 = item.Artikeltext1,
                Artikeltext2 = item.Artikeltext2,
                Artikeltext3 = item.Artikeltext3,
                Bestand = item.Bestand,
                Bezeichnung = item.Bezeichnung,
                Stueckzahl = item.Stueckzahl,
                Verpackungseinheit = item.Verpackungseinheit
            };
            return model;
        }


    }
}