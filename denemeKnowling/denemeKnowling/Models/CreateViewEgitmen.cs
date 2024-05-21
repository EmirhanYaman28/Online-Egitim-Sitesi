using System.ComponentModel.DataAnnotations;

namespace denemeKnowling.Models
{
	public class CreateViewEgitmen
	{
		public int ID { get; set; }
		[Required(ErrorMessage = "Egitmen Ad Soyad Yazınız")]
		public string? AdSoyad { get; set; }
		[Required(ErrorMessage = "Egitmen Meslek Yazınız")]
		public string? Meslek { get; set; }
		[Required(ErrorMessage = "Egitmen Hakkında Yazınız")]
		public string? Hakkında { get; set; }
		[Required(ErrorMessage = "Egitmen resimini seeçiniz.")]
		public IFormFile Photo { get; set; }
		public string? PhotoFileName { get; set; }
	}
}
