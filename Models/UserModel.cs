using System.ComponentModel.DataAnnotations;
namespace ASP_Pokemon.Models
{
    public class UserModel
    {
        [Required]
        public int id { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string password { get; set; }
    }
}
