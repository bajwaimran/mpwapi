using BestellErfassung.FileHelpers;
using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using System;
//using DevExpress.Persistent.BaseImpl;


namespace BestellErfassung.DomainObjects.Artikel
{
    //[DefaultClassOptions]
    public class Artikelstamm : XPObject
    {
        public Artikelstamm(Session session) : base(session) { }

        private int _Bestand;
        bool ArtNrInt_nichtgefunden;

        protected override void OnSaving()
        {
            if (Session.IsNewObject(this))
            {
                try
                {
                    if (ArtNrInt == 0)
                    {
                        int fArtNrInt;
                        do
                        {
                            fArtNrInt = IntegerFunctions.ProceedeNumber("ArtNrInt", ArtNrInt, 100000);

                            DomainObjects.Artikel.Artikelstamm _artnrintgef = Session.DefaultSession.FindObject<DomainObjects.Artikel.Artikelstamm>(CriteriaOperator.Parse("ArtNrInt == ?", fArtNrInt));
                            if (_artnrintgef != null)
                                ArtNrInt_nichtgefunden = false;
                            else
                                ArtNrInt_nichtgefunden = true;

                        } while (ArtNrInt_nichtgefunden == false);

                        if ((fArtNrInt > 0) && ArtNrInt != fArtNrInt)
                            ArtNrInt = fArtNrInt;
                    }

                }


                catch
                {

                }
            }
            base.OnSaving();
        }


        private int _ArtNrInt;
        public int ArtNrInt
        {
            get { return _ArtNrInt; }
            set { SetPropertyValue<int>("ArtNrInt", ref _ArtNrInt, value); }
        }

        //private int _StueckLieferbar;
        //public int StueckLieferbar
        //{
        //    get { return _StueckLieferbar; }
        //    set { SetPropertyValue<int>("StueckLieferbar", ref _StueckLieferbar, value); }
        //}



        private string _ArtNr;
        [Size(60)]
        public string ArtNr
        {
            get { return _ArtNr; }
            set { SetPropertyValue<string>("ArtNr", ref _ArtNr, value); }
        }


        public int Bestand
        {
            get
            {
                return _Bestand;
            }
            set
            {
                SetPropertyValue("Bestand", ref _Bestand, value);
            }
        }

        private string _Bezeichnung;
        [Size(140)]
        public string Bezeichnung
        {
            get { return _Bezeichnung; }
            set { SetPropertyValue<string>("Bezeichnung", ref _Bezeichnung, value); }
        }

        private string _Artikeltext1;
        [Size(SizeAttribute.Unlimited)]
        public string Artikeltext1
        {
            get { return _Artikeltext1; }
            set { SetPropertyValue<string>("Artikeltext1", ref _Artikeltext1, value); }
        }

        private string _Artikeltext2;
        [Size(SizeAttribute.Unlimited)]
        public string Artikeltext2
        {
            get { return _Artikeltext2; }
            set { SetPropertyValue<string>("Artikeltext2", ref _Artikeltext2, value); }
        }

        private string _Artikeltext3;
        [Size(SizeAttribute.Unlimited)]
        public string Artikeltext3
        {
            get { return _Artikeltext3; }
            set { SetPropertyValue<string>("Artikeltext3", ref _Artikeltext3, value); }
        }



        private decimal _Stueckzahl;
        [NonPersistent]
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

        private decimal _Verpackungseinheit;
        [DbType("numeric (12,4)")]
        public decimal Verpackungseinheit
        {
            get { return _Verpackungseinheit; }
            set { SetPropertyValue<decimal>("Verpackungseinheit", ref _Verpackungseinheit, value); }
        }

        //private XPCollection<AuditDataItemPersistent> changeHistory;
        //public XPCollection<AuditDataItemPersistent> ChangeHistory
        //{
        //    get
        //    {
        //        if (changeHistory == null)
        //        {
        //            changeHistory = AuditedObjectWeakReference.GetAuditTrail(Session, this);
        //        }
        //        return changeHistory;
        //    }
        //}
    }

}
