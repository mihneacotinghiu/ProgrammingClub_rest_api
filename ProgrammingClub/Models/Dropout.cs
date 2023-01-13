using System.ComponentModel.DataAnnotations;

namespace ProgrammingClub.Models
{
    public class Dropout
    {
        [Key]
        public Guid? IDDropout { get; set; }
        public Guid? IDEvent { get; set; }
        public decimal? DropoutRate { get; set; }
    }
}
