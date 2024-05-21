using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace denemeKnowling.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(20, ErrorMessage = "İsim uzunluğunuz 20 haneden uzun olamaz!"), DisplayName("Kullanıcı Adı")]
        public string? UserName { get; set; }
        [Required]
        [DisplayName("Mail")]
        public string? EMail { get; set; }
        [Required]
        [DisplayName("Şifre")]
        public string? Password { get; set; }

        [NotMapped]
        [Compare("Password", ErrorMessage = "Şifreler eşleşmiyor.")]
        public string? ConfirmPassword { get; set; }

        [Required]
        [DisplayName("Doğum Tarihi")]
        public DateTime BirthDate { get; set; }
        //[DisplayName("Profil Fotoğrafı")]
        //public string? profileImage { get; set; }
        [Required]
        [DisplayName("Yetki")]
        public string? Permission { get; set; } = "Üye";

    }
}
