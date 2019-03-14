using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;


namespace BestellErfassung.DomainObjects.Bestellungen
{
    //[DefaultClassOptions]
   public class BestellAuftraege : XPObject
    {     public BestellAuftraege(Session session)
            : base(session)
        {
        }

        private string _AuftragsNr;
        private string _mboid;
        [Size(70)]
        public string mboid
        {
            get { return _mboid; }
            set { SetPropertyValue<string>("mboid", ref _mboid, value); }
        }

        [Size(100)]
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


        private Bestellung _Bestellung;
        [Association("Bestellung-BestellAuftraege", typeof(Bestellung))]
        public Bestellung Bestellung
        {
            get { return _Bestellung; }
            set { SetPropertyValue<Bestellung>("Bestellung", ref _Bestellung, value); }
        }


        public override void AfterConstruction()
        {
            base.AfterConstruction();
         }
    }
}
