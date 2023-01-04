using System.ComponentModel.DataAnnotations;

namespace ProgrammingClub.Models
{
    public class EventType
    {
        [Key]
        public Guid? IdEventType { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public Guid? IdPricingModel { get; set; }


    }
}
