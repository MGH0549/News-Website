using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace NewsWebsite.Models
{
    public class TeamMember
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Job { get; set; }
        public string? Image { get; set; }

        [NotMapped]
        [DisplayName("Image")]
        public IFormFile file { get; set; }
    }
}
