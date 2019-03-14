using System;
using DevExpress.Xpo;

namespace BestellErfassung.DomainObjects.Kunden
{
    //[DefaultClassOptions]
    public class Kundenstamm : XPObject
    {
      
             public Kundenstamm(Session session) : base(session) { }


//Grunddaten
             private bool _Selektion;
             [NonPersistent]
             public bool Selektion
             {
                 get { return _Selektion; }
                 set { SetPropertyValue<bool>("Selektion", ref _Selektion, value); }
             }

        private int _KDNr;
   
        public int KDNr
        {
            get { return _KDNr; }
            set { SetPropertyValue<int>("KDNr", ref _KDNr, value); }
        }

        
        private string _Suchname;
        [Size(60)]
        public string Suchname
        {
            get { return _Suchname; }
            set { SetPropertyValue<string>("Suchname", ref _Suchname, value); }
        }

        private string _Anrede;
        [Size(30)]
        public string Anrede
        {
            get { return _Anrede; }
            set { SetPropertyValue<string>("Anrede", ref _Anrede, value); }
        }

        private string _Titel;
        [Size(30)]
        public string Titel
        {
            get { return _Titel; }
            set { SetPropertyValue<string>("Titel", ref _Titel, value); }
        }

        private string _Name1;
        [Size(60)]
        public string Name1
        {
            get { return _Name1; }
            set { SetPropertyValue<string>("Name1", ref _Name1, value); }
        }

        private string _Name2;
        [Size(60)]
        public string Name2
        {
            get { return _Name2; }
            set { SetPropertyValue<string>("Name2", ref _Name2, value); }
        }

        private string _Name3;
        [Size(60)]
        public string Name3
        {
            get { return _Name3; }
            set { SetPropertyValue<string>("Name3", ref _Name3, value); }
        }

        private string _Strasse;
        [Size(60)]
        public string Strasse
        {
            get { return _Strasse; }
            set { SetPropertyValue<string>("Strasse", ref _Strasse, value); }
        }

        private string _Nation;
        [Size(5)]
        public string Nation
        {
            get { return _Nation; }
            set { SetPropertyValue<string>("Nation", ref _Nation, value); }
        }

        private string _PLZ;
        [Size(10)]
        public string PLZ
        {
            get { return _PLZ; }
            set { SetPropertyValue<string>("PLZ", ref _PLZ, value); }
        }

        private string _Ort;
        [Size(60)]
        public string Ort
        {
            get { return _Ort; }
            set { SetPropertyValue<string>("Ort", ref _Ort, value); }
        }

                private string _EMail;
        [Size(120)]
        public string eMail
        {
            get { return _EMail; }
            set { SetPropertyValue<string>("eMail", ref _EMail, value); }
        }
        private Kundenarten _Kundenart;
        public Kundenarten Kundenart
        {
            get { return _Kundenart; }
            set { SetPropertyValue<Kundenarten>("Kundenart", ref _Kundenart, value); }
        }

        private bool _Kdauftrgesperrt;
        public bool kdauftrgesperrt
        {
            get { return _Kdauftrgesperrt; }
            set { SetPropertyValue<bool>("kdauftrgesperrt", ref _Kdauftrgesperrt, value); }
        }
    }
}
