using System.ComponentModel.DataAnnotations;

namespace ProgrammingClub.Models
{
    public class PricingModels
    {
        [Key]
        public Guid? IdPricingModels { get; set; }
        public string? Name { get; set; }
        public float? Price { get; set; }
        public DateOnly? ModifiedDate { get; set; }
    }
}
