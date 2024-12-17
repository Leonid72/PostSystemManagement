using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Place
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } // Name of the place
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
