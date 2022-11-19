using System.ComponentModel.DataAnnotations;
namespace ASP_Pokemon.Models
{
    public class SpriteModel
    {
        [Required]
        public string front_default { get; set; }
        [Required]
        public string back_default { get; set; }
        [Required]
        public string front_shiny { get; set; }
        [Required]
        public string back_shiny { get; set; }
    }
}
