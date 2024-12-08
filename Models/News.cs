using Microsoft.Build.Framework;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsWebsite.Models
{
    public class News
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Image { get; set; }
        public DateTime? Date { get; set; }
        public string? Topic { get; set; }

        [ForeignKey("Category")]
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        [NotMapped]
        [Required]
        [DisplayName("Image")]

        public IFormFile ClientFiles { get; set; }
    }
}
