using Microsoft.EntityFrameworkCore;
using StudentAdmissionPortal.Data;
using StudentAdmissionPortal.Models;
using StudentAdmissionPortal.Repositories.Abstract;

namespace StudentAdmissionPortal.Repositories.Implementation
{
    public class FamilyMembersRepository : IFamilyMembersRepository
    {
        private readonly StudentModalDbContext _studentModalDbContext;

        public FamilyMembersRepository(StudentModalDbContext studentModalDbContext)
        {
            _studentModalDbContext = studentModalDbContext;
        }

        public async Task<FamilyMembers> AddFamilyMember(FamilyMembers familyMember)
        {
            _studentModalDbContext.FamilyMembers.Add(familyMember);
            await _studentModalDbContext.SaveChangesAsync();
            return familyMember;

            throw new NotImplementedException();
        }

        public async Task<bool> DeleteFamilyMember(int id)
        {
            var familyMember = await _studentModalDbContext.FamilyMembers.FindAsync(id);
            if (familyMember == null) return false;

            _studentModalDbContext.FamilyMembers.Remove(familyMember);
            await _studentModalDbContext.SaveChangesAsync();
            return true;

            throw new NotImplementedException();
        }

        public async Task<FamilyMembers> GetFamilyMemberById(int id)
        {
            return await _studentModalDbContext.FamilyMembers
                .Include(fm => fm.Nationality)
                .Include(fm => fm.Student)
                .FirstOrDefaultAsync(fm => fm.Id == id);
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<FamilyMembers>> GetFamilyMembersByStudentId(int studentId)
        {
            return await _studentModalDbContext.FamilyMembers
                .Where(fm => fm.StudentId == studentId)
                .Include(fm => fm.Nationality)
                .ToListAsync();
            throw new NotImplementedException();
        }

        public async Task<FamilyMembers> UpdateFamilyMember(FamilyMembers familyMember)
        {
            _studentModalDbContext.FamilyMembers.Update(familyMember);
            await _studentModalDbContext.SaveChangesAsync();
            return familyMember;

            throw new NotImplementedException();
        }
    }
}
