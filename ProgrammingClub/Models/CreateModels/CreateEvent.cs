using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProgrammingClub.Models.CreateModels
{
    public class CreateEvent
    {
        [Key]
        [JsonIgnore]
        public Guid IdEvent { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid IdModerator { get; set; }
        public Guid IdEventType { get; set; }
    }
}
