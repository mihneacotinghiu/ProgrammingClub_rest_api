using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProgrammingClub.Models
{
    public class CodeSnippet
    {
        [Key]
        public Guid? IdCodeSnippet { get; set; }
        public string? Title { get; set; }
        public Guid? IdMember { get; set; }
        public int? Revision { get; set; }
        public Guid? IdSnippetPreviousVersion { get; set; }
        public DateTime? DateTimeAdded { get; set; }
        public bool? isPublished { get; set; }
    }
}
