using System.ComponentModel.DataAnnotations;
namespace ASP_Pokemon.Models
{
    public class PokemonModel
    {

        [Required]
        public string name { get; set; }
        [Required]
        public List<TypeModel> types { get; set; }
        [Required]
        public int id { get; set; }
        [Required]
        public int weight { get; set; }
        [Required]
        public int base_experience { get; set; }
        public class AbilityModel
        {
            public class Ability
            {
                [Required]
                public string name { get; set; }
            }
            [Required]
            public Ability ability { get; set; }
        }
        public class StatsModel
        {
            public class Statics
            {
                [Required]
                public string name { get; set; }
                [Required]
                public string url { get; set; }
            }
            [Required]
            public int base_stat { get; set; }
            [Required]
            public int effort { get; set; }
            [Required]
            public Statics stat { get; set; }
        }
        [Required]
        public List<StatsModel> stats { get; set; }
        [Required]
        public List<AbilityModel> abilities { get; set; }
        [Required]
        public int height { get; set; }
        [Required]
        public int order { get; set; }
        [Required]
        public SpriteModel sprites { get; set; }
    }
}
