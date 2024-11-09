using Microsoft.EntityFrameworkCore;
using StudentAdmissionPortal.Data;
using StudentAdmissionPortal.Models;
using StudentAdmissionPortal.Repositories.Abstract;

namespace StudentAdmissionPortal.Repositories.Implementation
{
    public class StudentRepository : IStudentRepository
    {
        private readonly StudentModalDbContext _studentModalDbContext;

        public StudentRepository(StudentModalDbContext studentModalDbContext)
        {
            _studentModalDbContext = studentModalDbContext;
        }

        public async Task<Student> AddStudent(Student student)
        {
            _studentModalDbContext.Student.Add(student);
            await _studentModalDbContext.SaveChangesAsync();
            return student;
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteStudent(int id)
        {
            var student = await _studentModalDbContext.Student.FindAsync(id);
            if(student == null) { return false; }

            _studentModalDbContext.Remove(student);
            await _studentModalDbContext.SaveChangesAsync();
            return true;

            //throw new NotImplementedException();
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            return await _studentModalDbContext.Student.Include(s => s.Nationality)
                .Include(s => s.FamilyMembers)
                .ToListAsync();
           // throw new NotImplementedException();
        }

        public async Task<Student> GetStudentById(int id)
        {
            return await _studentModalDbContext.Student.Include(s => s.Nationality).Include(s => s.FamilyMembers).FirstOrDefaultAsync( s => s.Id == id);
           //throw new NotImplementedException();
        }

        public async Task<Student> UpdateStudent(Student student)
        {
            _studentModalDbContext.Update(student);
            await _studentModalDbContext.SaveChangesAsync();
            return student;

            //throw new NotImplementedException();
        }
    }
}
