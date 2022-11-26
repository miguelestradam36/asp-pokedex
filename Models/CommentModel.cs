using System.ComponentModel.DataAnnotations;

namespace ASP_Pokemon.Models
{
    public class CommentModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Subject { get; set; }
        [Required]
        public string Image { get; set; }
        [Required]
        public string Message { get; set; }
    }
}
