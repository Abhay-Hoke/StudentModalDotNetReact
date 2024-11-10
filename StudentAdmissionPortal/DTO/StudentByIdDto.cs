namespace StudentAdmissionPortal.DTO
{
    public class StudentByIdDto
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int NationalityId { get; set; }

        public List<string> FamilyMemberNames { get; set; } = new List<string>();

    }
}
