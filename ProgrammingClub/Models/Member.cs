using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProgrammingClub.Models
{
    public class Member
    {
        [Key]
        public Guid? IdMember { get; set; }
       
        public string? Name { get; set; }
        
        public string? Title { get; set; }
      
        public string? Description { get; set; }
        
        public string? Position { get; set; }
        
        public string? Resume { get; set; }
    }
}
