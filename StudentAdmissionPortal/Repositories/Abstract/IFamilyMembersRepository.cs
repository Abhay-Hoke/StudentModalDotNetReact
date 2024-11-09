using StudentAdmissionPortal.Models;

namespace StudentAdmissionPortal.Repositories.Abstract
{
    public interface IFamilyMembersRepository
    {
        Task<IEnumerable<FamilyMembers>> GetFamilyMembersByStudentId(int studentId);
        Task<FamilyMembers> GetFamilyMemberById(int id);
        Task<FamilyMembers> AddFamilyMember(FamilyMembers familyMember);
        Task<FamilyMembers> UpdateFamilyMember(FamilyMembers familyMember);
        Task<bool> DeleteFamilyMember(int id);
    }
}
