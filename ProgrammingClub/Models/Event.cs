using ProgrammingClub.Helpers;
using System.ComponentModel.DataAnnotations;

namespace ProgrammingClub.Models
{
    public class Event
    {
        [Key]
        public Guid IdEvent { get; set; }
        [StringLength(250, MinimumLength = 3, ErrorMessage = ErrorMessagesEnum.IncorrectSize)]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid? IdModerator { get; set; }
        public Guid? IdEventType { get; set; }
    }
}
