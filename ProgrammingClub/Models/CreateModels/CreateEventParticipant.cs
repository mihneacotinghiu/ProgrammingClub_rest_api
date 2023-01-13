using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProgrammingClub.Models.CreateModels;

public class CreateEventsParticipant
{
    [Key]
    [JsonIgnore]
    public Guid IdEventParticipant { get; set; }

    [Key]
    [Required]
    public Guid IdEvent { get; set; }

    [Key]
    [Required]
    public Guid IdMember {  get; set; }

    [Required]
    public bool Paid { get; set; }

    [Required]
    public bool Present { get; set; }
}