﻿namespace StudentAdmissionPortal.DTO
{
    public class FamilyMemberBasicDto
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int RelationshipId { get; set; }
    }
}