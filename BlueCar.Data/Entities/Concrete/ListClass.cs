using System;

namespace BlueCar_Api
{
    public class ListClass : MongoDbEntity
    {
        public string aracmarka { get; set; }
        public string aracmodel { get; set; }
        public string aracresim { get; set; }
        public decimal depozito { get; set; }
        public int gunluk_km { get; set; }
        public string yolcu { get; set; }
        public string bagaj { get; set; }
        public string segment { get; set; }
        public int kapi { get; set; }
        public string kasatip { get; set; }
        public decimal gunluk_fiyat { get; set; }
        public string tedarikci_firma { get; set; }
        public string vites { get; set; }
        public string yakit { get; set; }
        public int gun { get; set; }
        public decimal toplam_fiyat { get; set; }
        public string alisyeri { get; set; }
        public string donusyeri { get; set; }
    }
}
