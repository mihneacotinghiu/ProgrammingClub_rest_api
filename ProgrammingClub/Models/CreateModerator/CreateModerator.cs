using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProgrammingClub.Models.CreateModerator
{
    public class CreateModerator
    {
        [Key]
        [JsonIgnore]
        public Guid? IDModerator { get; set; }
        [Required]
        public string? Title { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public Guid? IDMember { get; set; }
    }
}
