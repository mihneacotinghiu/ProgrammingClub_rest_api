using System.ComponentModel.DataAnnotations;

namespace ProgrammingClub.Models
{
    public class Event
    {
        [Key]
        public Guid IdEvent { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid IdModerator { get; set; }
        public Guid IdEventType { get; set; }
    }
}
