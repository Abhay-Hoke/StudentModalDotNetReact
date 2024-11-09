using Microsoft.EntityFrameworkCore;
using StudentAdmissionPortal.Data;
using StudentAdmissionPortal.Models;
using StudentAdmissionPortal.Repositories.Abstract;

namespace StudentAdmissionPortal.Repositories.Implementation
{
    public class NationalityRepository : INationalityRepository
    {
        private readonly StudentModalDbContext _studentModalDbContext;

        public NationalityRepository(StudentModalDbContext studentModalDbContext)
        {
            _studentModalDbContext = studentModalDbContext;
        }

        public async Task<Nationality> AddNationality(Nationality nationality)
        {
            _studentModalDbContext.Nationality.Add(nationality);
            await _studentModalDbContext.SaveChangesAsync();
            return nationality;
            //throw new NotImplementedException();
        }

        public async Task<bool> DeleteNationality(int id)
        {
            var nationality = await _studentModalDbContext.FindAsync<Nationality>(id);
            if(nationality == null) { return false; }

            _studentModalDbContext.Nationality.Remove(nationality);
            await _studentModalDbContext.SaveChangesAsync();
            return true;

            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Nationality>> GetAllNationalities()
        {
            return await _studentModalDbContext.Nationality.ToListAsync();
            throw new NotImplementedException();
        }

        public async Task<Nationality> GetNationalityById(int id)
        {
            return await _studentModalDbContext.Nationality.FindAsync(id);
            //throw new NotImplementedException();
        }

        public async Task<Nationality> UpdateNationality(Nationality nationality)
        {
            _studentModalDbContext.Update(nationality);
            await _studentModalDbContext.SaveChangesAsync();
            return nationality;
            throw new NotImplementedException();
        }
    }
}
