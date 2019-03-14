using System;
using DevExpress.Xpo;

namespace BestellErfassung.DomainObjects.Tools
{
    //[DefaultClassOptions]
    public class Nummernvergabe : XPObject
    {
        public Nummernvergabe(Session session) : base(session) { }

        private string _Nummerart;
        [Size(60)]
        public string Nummerart
        {
            get { return _Nummerart; }
            set { SetPropertyValue<string>("Nummerart", ref _Nummerart, value); }
        }

        private int _Nummer;
        public int Nummer
        {
            get { return _Nummer; }
            set { SetPropertyValue<int>("Nummer", ref _Nummer, value); }
        }

        private string _EANGrundCode;
        public string EANGrundCode
        {
            get { return _EANGrundCode; }
            set { SetPropertyValue<string>("EANGrundCode", ref _EANGrundCode, value); }
        }

        private bool _ZuKundenauswahl;
        public bool ZuKundenauswahl
        {
            get { return _ZuKundenauswahl; }
            set { SetPropertyValue<bool>("ZuKundenauswahl", ref _ZuKundenauswahl, value); }
        }

        private bool _OhneFibuverbuchung;
        public bool ohneFibuverbuchung
        {
            get { return _OhneFibuverbuchung; }
            set { SetPropertyValue<bool>("ohneFibuverbuchung", ref _OhneFibuverbuchung, value); }
        }

        private bool _TRohneFibuverbuchung;
        public bool TRohneFibuverbuchung
        {
            get { return _TRohneFibuverbuchung; }
            set { SetPropertyValue<bool>("TRohneFibuverbuchung", ref _TRohneFibuverbuchung, value); }
        }

        private bool _RechnrohneVerbuchung;
        public bool RechnrohneVerbuchung
        {
            get { return _RechnrohneVerbuchung; }
            set { SetPropertyValue<bool>("RechnrohneVerbuchung", ref _RechnrohneVerbuchung, value); }
        }

    }

}
