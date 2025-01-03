﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentAdmissionPortal.Models
{
    public class FamilyMembers
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        [StringLength(50)]
        public string Relationship { get; set; }

        
        public int? NationalityId { get; set; }

        
        public int StudentId { get; set; }

        [ForeignKey("NationalityId")]
        public Nationality Nationality { get; set; }

        [ForeignKey("StudentId")]
        public Student Student { get; set; }
    }
}
