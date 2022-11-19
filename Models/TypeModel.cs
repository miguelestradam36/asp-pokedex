using System.ComponentModel.DataAnnotations;
namespace ASP_Pokemon.Models
{
    public class TypeModel
    {
        public class Type
        {
            [Required]
            public string name { get; set; }
        }
        [Required]
        public Type type { get; set; }

    }
}
