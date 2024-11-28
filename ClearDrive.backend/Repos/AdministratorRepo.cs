using ClearDrive.backend.Context;
using ClearDrive.backend.Models.Datas.Entities;
using ClearDrive.backend.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace ClearDrive.backend.Repos
{
    public class AdministratorRepo : IAdministratorRepo
    {
        private readonly CASInMemoryContext _dbContext;

        public AdministratorRepo(CASInMemoryContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Administrator>> GetAll()
        {
            return await _dbContext.Administrators.ToListAsync();
        }

        public async Task<Administrator?> GetBy(Guid id)
        {
            return await _dbContext.Administrators.FirstOrDefaultAsync(t => t.Id == id);
        }
        public async Task<ControllerResponse> UpdateAdminAsync(Administrator administrator)
        {
            ControllerResponse response = new ControllerResponse();
            _dbContext.ChangeTracker.Clear();
            _dbContext.Entry(administrator).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                response.AppendNewError(e.Message);
                response.AppendNewError($"{nameof(AdministratorRepo)} admin, {nameof(UpdateAdminAsync)} metódusban hiba keletkezett");
                response.AppendNewError($"{administrator} frissítése nem sikerült!");
            }
            return response;
        }

        public async Task<ControllerResponse> DeleteAsync(Guid id)
        {
            ControllerResponse response = new ControllerResponse();

            Administrator? AdminToDelete = await GetBy(id);

            if (AdminToDelete == null || AdminToDelete == default)
            {
                response.AppendNewError($"{id} névvel rendelkező admin nem található!");
                response.AppendNewError("A admin törlése nem sikerült!");
            }
            else
            {
                _dbContext.ChangeTracker.Clear();
                _dbContext.Entry(AdminToDelete).State = EntityState.Deleted;
                await _dbContext.SaveChangesAsync();
            }
            return response;
        }
    }
}
