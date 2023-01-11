using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProgrammingClub.Models.CreateModels
{
    public class CreateCodeSnippet
    {
        [Key]
        [JsonIgnore]
        public Guid IdCodeSnippet { get; set; }

        [StringLength(100, ErrorMessage = "Title's maximus size is 100")]
        [Required]
        public string Title { get; set; }

        [Required]
        public Guid IdMember { get; set; }

        [Required]
        [Range(0,int.MaxValue,ErrorMessage = "Revision must be a positive number")]
        public int Revision { get; set; }
        
        public Guid? IdSnippetPreviousVersion { get; set; }
        
        [Required]
        public DateTime DateTimeAdded { get; set; }

        [Required]
        public bool isPublished { get; set; }
    }
}
