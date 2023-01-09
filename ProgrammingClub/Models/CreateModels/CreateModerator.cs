using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProgrammingClub.Models.CreateModels
{
    public class CreateModerator
    {
        [Key]
        [JsonIgnore]
        public Guid? IDModerator { get; set; }

        [StringLength(100, ErrorMessage = "Title's maximus size is 100")]
        [Required]
        public string? Title { get; set; }

        [StringLength(1000, ErrorMessage = "Descriptions's maximus size is 1000")]
        [Required]
        public string? Description { get; set; }

        [Required]
        public Guid? IDMember { get; set; }
    }
}
