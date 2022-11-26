using System.ComponentModel.DataAnnotations;
namespace ASP_Pokemon.Models
{
    public class LoginModel
    {
        [Required]
        public int id { get; set; }
        [Required]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public UserModel user { get; set; }
    }
}
