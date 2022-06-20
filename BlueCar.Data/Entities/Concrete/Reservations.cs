using System;

namespace BlueCar_Api
{
    public class Reservations : MongoDbEntity
	{
		public int resId { get; set; }
		public string ad { get; set; }
		public string soyad { get; set; }
		public string rezervasyon_no { get; set; }
		public string islem_tarih { get; set; } = DateTime.Now.ToString();
		public string telefon { get; set; }
		public string mail { get; set; }
		public string tc { get; set; }
		public string adres { get; set; }
		public string sehir { get; set; }
		public string ulke { get; set; }
		public string ucus_no { get; set; }
		public string marka { get; set; }
		public string model { get; set; }
		public string vites { get; set; }
		public string yakit { get; set; }
		public string alis_yeri { get; set; }
		public string donus_yeri { get; set; }
		public string alis_tarih { get; set; }
		public string donus_tarih { get; set; }
		public string alis_saat { get; set; }
		public string donus_saat { get; set; }
		public string kirabedeli { get; set; }
		public string toplam_tutar { get; set; }
		public string tahsil_edilen { get; set; }
		public string kalan_tutar { get; set; }
		public string tedarikci { get; set; }
		public string aciklama { get; set; }
		public string fatura_adres { get; set; }

		public Kart_Bilgileri kart { get; set; }

		public Ek_Secenekler ek_secenekler { get; set; }
	}
	public class Kart_Bilgileri
	{
		public string kart_sahibi { get; set; }
		public string kart_no { get; set; }
		public string kart_ay{ get; set; }
		public string kart_yil { get; set; }
		public string kart_cvv { get; set; }
	}
	public class Ek_Secenekler
	{
		public decimal ek_sofor { get; set; }
		public decimal mini_sigorta { get; set; }
		public decimal drop_bedeli { get; set; }
		public decimal lcf { get; set; }
		public decimal bebek_koltuk { get; set; }
		public decimal navigasyon { get; set; }
	}
}
