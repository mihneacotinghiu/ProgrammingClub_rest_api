using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;

namespace ProgrammingClub.Models
{
    public class Announcement
    {
        [Key]
        public Guid? IdAnnouncement { get; set; }

        public DateTime? ValidFrom { get; set; }

        public DateTime? ValidTo { get; set; }

        [StringLength(250, ErrorMessage = "Title's maximus size is 250")]
        public string? Title { get; set; }

        [StringLength(1000, ErrorMessage = "Text's maximus size is 1000")]
        public string? Text { get; set; }

        public DateTime? EventDate { get; set; }

        [StringLength(1000, ErrorMessage = "Tags's maximus size is 1000")]
        public string? Tags { get; set; }
    }
}
