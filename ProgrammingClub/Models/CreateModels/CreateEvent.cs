using ProgrammingClub.Helpers;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProgrammingClub.Models.CreateModels
{
    public class CreateEvent
    {
        [Key]
        [JsonIgnore]
        public Guid IdEvent { get; set; }
        [StringLength(250, MinimumLength = 3, ErrorMessage = ErrorMessagesEnum.IncorrectSize)]
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Guid IdModerator { get; set; }
        [Required]
        public Guid IdEventType { get; set; }
    }
}
