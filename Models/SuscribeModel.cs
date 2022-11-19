using System.ComponentModel.DataAnnotations;

namespace ASP_Pokemon.Models
{
    public class SuscribeModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
