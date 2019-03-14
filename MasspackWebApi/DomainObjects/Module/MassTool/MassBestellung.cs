using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;

namespace BestellErfassung.DomainObjects.Module.MassTool
{
    //[DefaultClassOptions]
    public class MassBestellung : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (http://documentation.devexpress.com/#Xaf/CustomDocument3146).
        public MassBestellung(Session session)
            : base(session)
        {
        }


        private int _AuftragsNrKW;
        private string _Mitarbeiter;
        [Size(40)]
        public string Mitarbeiter
        {
            get { return _Mitarbeiter; }
            set { SetPropertyValue<string>("Mitarbeiter", ref _Mitarbeiter, value); }
        }

        private string _mboid;
        [Size(70)]
        public string mboid
        {
            get { return _mboid; }
            set { SetPropertyValue<string>("mboid", ref _mboid, value); }
        }

        private int _BestellNr;
        public int BestellNr
        {
            get { return _BestellNr; }
            set { SetPropertyValue<int>("BestellNr", ref _BestellNr, value); }
        }

        private int _KDNr;
        public int KDNr
        {
            get { return _KDNr; }
            set { SetPropertyValue<int>("KDNr", ref _KDNr, value); }
        }

        private string _ArtikelNr;
        [Size(60)]
        public string ArtikelNr
        {
            get { return _ArtikelNr; }
            set { SetPropertyValue<string>("ArtikelNr", ref _ArtikelNr, value); }
        }

        private decimal _Stueck;
        [DbType("numeric (12,4)")]
        public decimal Stueck
        {
            get { return _Stueck; }
            set { SetPropertyValue<decimal>("Stueck", ref _Stueck, value); }
        }

        private bool _Bestellt;
        public bool Bestellt
        {
            get { return _Bestellt; }
            set { SetPropertyValue<bool>("Bestellt", ref _Bestellt, value); }
        }



        private DateTime _Datum;
        public DateTime Datum
        {
            get { return _Datum; }
            set { SetPropertyValue<DateTime>("Datum", ref _Datum, value); }
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

    }
}
