using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;


namespace BestellErfassung.DomainObjects.Artikel
{
    //[DefaultClassOptions]
        public class SQLArtikelstamm : BasePersistentObject
    { 
        public SQLArtikelstamm(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }

        private int _ArtNrInt;
        public int ArtNrInt
        {
            get { return _ArtNrInt; }
            set { SetPropertyValue<int>("ArtNrInt", ref _ArtNrInt, value); }
        }



        private int _StueckLieferbar;
        public int StueckLieferbar
        {
            get { return _StueckLieferbar; }
            set { SetPropertyValue<int>("StueckLieferbar", ref _StueckLieferbar, value); }
        }

        

        private string _ArtNr;
        [Size(60)]
        public string ArtNr
        {
            get { return _ArtNr; }
            set { SetPropertyValue<string>("ArtNr", ref _ArtNr, value); }
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


        //[Association("SQLArtikelstamm-Artikelstamm"), Aggregated]
        //public XPCollection<Artikelstamm> SQLArtikelstamm_Artikelstamm_XPColl
        //{
        //    get { return GetCollection<Artikelstamm>("SQLArtikelstamm_Artikelstamm_XPColl"); }
        //}

      
      
    }
}
