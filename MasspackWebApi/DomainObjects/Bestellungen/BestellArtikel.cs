using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using MasspackWebApi.Models;

namespace BestellErfassung.DomainObjects.Bestellungen
{
    //[DefaultClassOptions]
    public class BestellArtikel : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
        public BestellArtikel(Session session)
            : base(session)
        {
        }




        private int _AuftragsNrKW;
        private decimal _Stueckzahl;
        private string _ArtikelNr;
        [Size(90)]
        public string ArtikelNr
        {
            get
            {
                return _ArtikelNr;
            }
            set
            {
                SetPropertyValue("ArtikelNr", ref _ArtikelNr, value);
            }
        }



        private string _Bezeichnung;
        [Size(140)]
        public string Bezeichnung
        {
            get { return _Bezeichnung; }
            set { SetPropertyValue<string>("Bezeichnung", ref _Bezeichnung, value); }
        }




        public decimal Stueckzahl
        {
            get
            {
                return _Stueckzahl;
            }
            set
            {
                SetPropertyValue("Stueckzahl", ref _Stueckzahl, value);
            }
        }

        private DomainObjects.Bestellung _Bestellung;

        public DomainObjects.Bestellung Bestellung
        {
            get { return _Bestellung; }
            set { SetPropertyValue<DomainObjects.Bestellung>("Bestellung", ref _Bestellung, value); }
        }



        private DomainObjects.Artikel.Artikelstamm _Artikel;
        [Size(60)]
        public DomainObjects.Artikel.Artikelstamm Artikel
        {
            get { return _Artikel; }
            set { SetPropertyValue<DomainObjects.Artikel.Artikelstamm>("Artikel", ref _Artikel, value); }
        }





        private string _mboid;
        [Size(70)]
        public string mboid
        {
            get { return _mboid; }
            set { SetPropertyValue<string>("mboid", ref _mboid, value); }
        }


        public int AuftragsNrKW
        {
            get
            {
                return _AuftragsNrKW;
            }
            set
            {
                SetPropertyValue("AuftragsNrKW", ref _AuftragsNrKW, value);
            }
        }


        private BestellKunden _bestellkunden;
        [Association("BestellKunden-BestellArtikel", typeof(BestellKunden))]
        public BestellKunden BestellKunden
        {
            get { return _bestellkunden; }
            set { SetPropertyValue<BestellKunden>("BestellKunden", ref _bestellkunden, value); }
        }



    }
}
