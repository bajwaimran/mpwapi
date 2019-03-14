using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using MasspackWebApi.Models;

namespace BestellErfassung.DomainObjects.Bestellungen
{
    //[DefaultClassOptions]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]
    // Specify more UI options using a declarative approach (http://documentation.devexpress.com/#Xaf/CustomDocument2701).
    public class BestellKunden : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
        public BestellKunden(Session session)
            : base(session)
        {
        }


        private DomainObjects.Kunden.Kundenstamm _Kunde;
        [Size(60)]
        public DomainObjects.Kunden.Kundenstamm Kunde
        {
            get { return _Kunde; }
            set { SetPropertyValue<DomainObjects.Kunden.Kundenstamm>("Kunde", ref _Kunde, value); }
        }


        private int _KDNr;

        public int KDNr
        {
            get { return _KDNr; }
            set { SetPropertyValue<int>("KDNr", ref _KDNr, value); }
        }

        private Bestellung _bestellung;
        [Association("Bestellung-BestellKunden", typeof(Bestellung))]
        public Bestellung Bestellung
        {
            get { return _bestellung; }
            set { SetPropertyValue<Bestellung>("Bestellung", ref _bestellung, value); }
        }

        [Association("BestellKunden-BestellArtikel", typeof(Bestellungen.BestellArtikel)), DevExpress.Xpo.Aggregated]
        public XPCollection<Bestellungen.BestellArtikel> BestellKunden_BestellArtikel_XPColl
        {
            get { return GetCollection<Bestellungen.BestellArtikel>("BestellKunden_BestellArtikel_XPColl"); }
        }

    }
}
