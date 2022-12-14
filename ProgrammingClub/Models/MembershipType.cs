using System.ComponentModel.DataAnnotations;

namespace ProgrammingClub.Models
{
    public class MembershipType
    {
        [Key]
        public Guid? IdMembershipType { get; set; }
        [StringLength(100, ErrorMessage = "Name's maximus size is 100")]
        [Required]
        public string? Name { get; set; }
        [StringLength(250, ErrorMessage = "Description's maximus size is 250")]
        [Required]
        public string? Description { get; set; }
        public int? SubscriptionLenghtInMonths { get; set; }


    }
}
