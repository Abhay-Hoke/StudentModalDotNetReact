using StudentAdmissionPortal.Models;

namespace StudentAdmissionPortal.Repositories.Abstract
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAllStudents();
        Task<Student> GetStudentById(int id);
        Task<Student> AddStudent(Student student);
        Task<Student> UpdateStudent(Student student);
        Task<bool> DeleteStudent(int id);
    }
}
