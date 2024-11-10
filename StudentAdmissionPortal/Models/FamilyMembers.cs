using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentAdmissionPortal.Models
{
    public class FamilyMembers
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        [Required]
        
        public string DateOfBirth { get; set; }

        
        [StringLength(50)]
        public string Relationship { get; set; }

        
        public int? NationalityId { get; set; }

        
        public int? StudentId { get; set; }

        [ForeignKey("NationalityId")]
        public Nationality Nationality { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }
    }
}
