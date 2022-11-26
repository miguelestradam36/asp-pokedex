using System.ComponentModel.DataAnnotations;
namespace ASP_Pokemon.Models
{
    public class UserModel
    {
        [Required]
        public int id { get; set; }
        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string username { get; set; }
        [StringLength(60, MinimumLength = 12)]
        [Required]
        public string password { get; set; }
    }
}
