using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ProgrammingClub.Helpers;

namespace ProgrammingClub.Models.CreateModels
{
    public class CreateMember
    {
        [Key]
        [JsonIgnore]
        public Guid IdMember { get; set; }

        [StringLength(250, MinimumLength = 3, ErrorMessage = ErrorMessagesEnum.IncorectSize)]
        [Required]
        public string Name { get; set; }

        [StringLength(100, ErrorMessage = "Title's maximus size is 250")]
        [Required]
        public string Title { get; set; }

        [StringLength(1000, ErrorMessage = "Descriptions's maximus size is 250")]
        [Required]
        public string Description { get; set; }

        [StringLength(250, ErrorMessage = "Position's maximus size is 250")]

        [Required]
        public string Position { get; set; }

        [Required]
        public string Resume { get; set; }
    }
}
