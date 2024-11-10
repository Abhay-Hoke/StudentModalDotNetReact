namespace StudentAdmissionPortal.DTO
{
    public class FamilyMemberBasicDto
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public int StudentId {  get; set; }
       
        public DateTime DateOfBirth { get; set; }
        public string Relationship { get; set; }
    }
}
