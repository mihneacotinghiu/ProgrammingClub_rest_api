using System.ComponentModel.DataAnnotations;

namespace ProgrammingClub.Models
{
    public class PricingModel
    {
        [Key]
        public Guid? IdPricingModel { get; set; }
        public string? Name { get; set; }
        public double? Price { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
