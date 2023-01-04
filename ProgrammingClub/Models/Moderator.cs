using System.ComponentModel.DataAnnotations;

namespace ProgrammingClub.Models
{
    public class Moderator
    {
        [Key]
        public Guid? IDModerator { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public Guid? IDMember { get; set; }

    }
}
