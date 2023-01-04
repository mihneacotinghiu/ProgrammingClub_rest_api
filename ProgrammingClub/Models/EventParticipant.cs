using System.ComponentModel.DataAnnotations;

namespace ProgrammingClub.Models
{
    public class EventsParticipant
    {
        [Key]
        public Guid? IdEventParticipant { get; set; }
        public Guid? IdEvent { get; set; }
        public Guid? IdMember { get; set; }
        public bool? Paid { get; set; }
        public bool? Present { get; set; }
    }
}
