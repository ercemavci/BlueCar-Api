using System;

namespace BlueCar_Api
{
    public class Cars : MongoDbEntity
    {
        public int carId { get; set; }
		public string aracresim { get; set; }
		public string aracmarka { get; set; }
		public string aracmodel { get; set; }
		public string yakit { get; set; }
		public string vites { get; set; }
		public string klima { get; set; }
		public int gunluk_km { get; set; }
		public string yolcu { get; set; }
		public string bagaj { get; set; }
		public string segment { get; set; }
		public string kasatip { get; set; }
		public decimal depozito { get; set; }
		public decimal uc { get; set; }
		public decimal dort_yedi { get; set; }
		public decimal sekiz_onbes { get; set; }
		public decimal onalti_yirmidokuz { get; set; }
		public decimal otuzust { get; set; }
		public int yas { get; set; }
		public decimal teminat { get; set; }
		public int kapi { get; set; }
		public int ehliyetyil { get; set; }
		public string firma { get; set; }

	}
}
