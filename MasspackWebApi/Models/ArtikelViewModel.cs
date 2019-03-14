namespace MasspackWebApi.Models
{
    public class ArtikelViewModel
    {
        public int Oid { get; set; }
        public int ArtNrInt { get; set; }
        public string ArtNr { get; set; }
        public int Bestand { get; set; }
        public string Bezeichnung { get; set; }
        public string Artikeltext1 { get; set; }
        public string Artikeltext2 { get; set; }
        public string Artikeltext3 { get; set; }
        public decimal Stueckzahl { get; set; }
        public decimal Verpackungseinheit { get; set; }
        public decimal StueckzahlLieferbar { get; set; }

    }
}