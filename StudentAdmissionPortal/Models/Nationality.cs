using System.ComponentModel.DataAnnotations;

namespace StudentAdmissionPortal.Models
{
    public class Nationality
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }


        public ICollection<Student> Students { get; set; }

        public ICollection<FamilyMembers> FamilyMembers { get; set; }
    }
}
