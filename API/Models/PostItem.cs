using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class PostItem
    {
        [Key]

        public int PostItemId { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(150, MinimumLength = 5)]
        public string Title { get; set; }
        public string? Content { get; set; } = String.Empty;
        public string? ImagePath { get; set; } = String.Empty;
        public int? PlaceId { get; set; }
        public Place? Place { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

    }
}
