namespace StudentAdmissionPortal.DTO
{
    public class FamilyMemberNationalityDto
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int RelationshipId { get; set; }
        public int NationalityId { get; set; }
    }
}
