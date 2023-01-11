using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProgrammingClub.Models
{
    public class CodeSnippet
    {
        [Key]
        public Guid? IdCodeSnippet { get; set; }

        [StringLength(100, ErrorMessage = "Title's maximus size is 100")]
        public string? Title { get; set; }

        public Guid? IdMember { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Revision must be a positive number")]
        public int? Revision { get; set; }

        public Guid? IdSnippetPreviousVersion { get; set; }

        public DateTime? DateTimeAdded { get; set; }

        public bool? isPublished { get; set; }
    }
}
