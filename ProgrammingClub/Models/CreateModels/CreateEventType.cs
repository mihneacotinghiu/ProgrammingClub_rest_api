using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProgrammingClub.Models
{
    public class CreateEventType
    {
        [Key]
        [JsonIgnore]
        public Guid? IdEventType { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public Guid IdPricingModel { get; set; }


    }
}
