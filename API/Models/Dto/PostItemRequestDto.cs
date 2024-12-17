using System.ComponentModel.DataAnnotations;

namespace API.Models.Dto
{
    public class PostItemRequestDto
    {
        public int? PostItemId { get; set; }
        [Required(ErrorMessage = "Title is required.")]
        [StringLength(150, MinimumLength = 5, ErrorMessage = "Title must be between 5 and 150 characters.")]
        public string Title { get; set; }

        public string? Content { get; set; } = string.Empty;

        public int? PlaceId { get; set; } = null;
        public IFormFile? Image { get; set; } = null;
        public string? ImagePath { get; set; } = String.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string CreatedBy { get; set; }
    }
}
