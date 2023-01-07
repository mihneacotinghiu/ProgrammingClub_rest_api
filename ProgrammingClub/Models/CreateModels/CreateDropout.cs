using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProgrammingClub.Models.CreateModels
{
    public class CreateDropout
    {
        [Key]
        [JsonIgnore]
        public Guid? IDDropout { get; set; }
        [Required]
        public Guid? IDEvent { get; set; }
        [Required]
        public float? DropoutRate { get; set; }
    }
}
