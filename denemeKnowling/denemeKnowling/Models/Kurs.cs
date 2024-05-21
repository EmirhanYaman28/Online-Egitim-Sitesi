using System.ComponentModel.DataAnnotations;

namespace denemeKnowling.Models
{
	public class Kurs
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = "Kurs Adı Yazınız")]
		[Display(Name = "Kurs Adı")]
		public string? Adı { get; set; }

		[Required(ErrorMessage = "Kurs Açıklaması Yazınız")]
		[Display(Name = "Açıklaması")]
		public string? Açıklaması { get; set; }

		[Required(ErrorMessage = "Kurs Kategorisi Yazınız")]
		[Display(Name = "Kategori")]
		public string? Kategorisi { get; set; }

		[Required(ErrorMessage = "Kurs Fiyatı Yazınız")]
		[Display(Name = "Fiyatı")]
		public float? Fiyatı { get; set; }

		[Required(ErrorMessage = "Kurs Süresi Yazınız")]
		[Display(Name = "Süresi")]
		public string? Süresi { get; set; }

		[Required(ErrorMessage = "Kurs Öğretmeni Yazınız")]
		[Display(Name = "Öğretmeni")]
		public string? Öğretmeni { get; set; }

		[Required(ErrorMessage = "Ürün resimini seeçiniz.")]
        public string? PhotoPath { get; set; }



    }
}
