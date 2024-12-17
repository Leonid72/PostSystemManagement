using System.ComponentModel.DataAnnotations;

namespace API.Models.Dto
{
    public class PostItemDto
    {
        public int PostItemId { get; set; }
        public string Title { get; set; }
        public string? Content { get; set; } = String.Empty;
        public string? ImagePath { get; set; } = String.Empty;
        public string? Place { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? CreatedBy { get; set; } = String.Empty;
    }
}
