namespace MasspackWebApi.Models
{
    public class MassOrder
    {
        public string Zusatzangabe { get; set; }
        public int[] KundenIds { get; set; }
        public int[] ArtikelIds { get; set; }
        public Item[] Items { get; set; }
    }
    public class Item
    {
        public int Oid { get; set; }
        public string ArtNr { get; set; }
        public int Quantity { get; set; }
    }
}