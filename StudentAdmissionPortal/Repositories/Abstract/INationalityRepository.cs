using StudentAdmissionPortal.Models;

namespace StudentAdmissionPortal.Repositories.Abstract
{
    public interface INationalityRepository
    {
        Task<IEnumerable<Nationality>> GetAllNationalities();
        Task<Nationality> GetNationalityById(int id);
        Task<Nationality> AddNationality(Nationality nationality);
        Task<Nationality> UpdateNationality(Nationality nationality);
        Task<bool> DeleteNationality(int id);
    }
}
