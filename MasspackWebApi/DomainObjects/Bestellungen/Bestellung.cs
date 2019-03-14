using DevExpress.Xpo;
using System;

namespace BestellErfassung.DomainObjects
{
    //[DefaultClassOptions]
    public class Bestellung : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
        public Bestellung(Session session)
            : base(session)
        {
        }


        private Boolean _Status;
        //[CaptionsForBoolValues("True Caption", "False Caption")]

        public Boolean Status
        {
            get { return _Status; }
            set { SetPropertyValue<Boolean>("Status", ref _Status, value); }
        }

        private Boolean _Fertig;
        //[CaptionsForBoolValues("True Caption", "False Caption")]

        public Boolean Fertig
        {
            get { return _Fertig; }
            set { SetPropertyValue<Boolean>("Fertig", ref _Fertig, value); }
        }


        private DateTime _Datum;
        public DateTime Datum
        {
            get { return _Datum; }
            set { SetPropertyValue<DateTime>("Datum", ref _Datum, value); }
        }

        private string _AuftragsNr;
        [Size(90)]
        public string AuftragsNr
        {
            get
            {
                return _AuftragsNr;
            }
            set
            {
                SetPropertyValue("AuftragsNr", ref _AuftragsNr, value);
            }
        }

        private string _Zusatzangabe;
        [Size(60)]
        public string Zusatzangabe
        {
            get { return _Zusatzangabe; }
            set { SetPropertyValue<string>("Zusatzangabe", ref _Zusatzangabe, value); }
        }


        [Association("Bestellung-BestellKunden", typeof(Bestellungen.BestellKunden)), DevExpress.Xpo.Aggregated]
        public XPCollection<Bestellungen.BestellKunden> Bestellung_BestellKunden_XPColl
        {
            get { return GetCollection<Bestellungen.BestellKunden>("Bestellung_BestellKunden_XPColl"); }
        }

        [Association("Bestellung-BestellAuftraege", typeof(Bestellungen.BestellAuftraege)), DevExpress.Xpo.Aggregated]
        public XPCollection<Bestellungen.BestellAuftraege> Bestellung_BestellAuftraege_XPColl
        {
            get { return GetCollection<Bestellungen.BestellAuftraege>("Bestellung_BestellAuftraege_XPColl"); }
        }


    }
}
