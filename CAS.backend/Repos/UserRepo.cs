using CAS.backend.Context;
using CAS.shared.Models.Datas.Entities;
using CAS.shared.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace CAS.backend.Repos
{
    public class UserRepo : IUserRepo
    {
        private readonly CASInMemoryContext _dbContext;

        public UserRepo(CASInMemoryContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<User>> GetAll()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<User?> GetBy(Guid id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(t => t.Id == id);
        }
        public async Task<ControllerResponse> UpdateUserAsync(User user)
        {
            ControllerResponse response = new ControllerResponse();
            _dbContext.ChangeTracker.Clear();
            _dbContext.Entry(user).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                response.AppendNewError(e.Message);
                response.AppendNewError($"{nameof(ProblemTicketRepo)} osztály, {nameof(UpdateUserAsync)} metódusban hiba keletkezett");
                response.AppendNewError($"{user} frissítése nem sikerült!");
            }
            return response;
        }

        public async Task<ControllerResponse> DeleteAsync(Guid id)
        {
            ControllerResponse response = new ControllerResponse();

            User? UserToDelete = await GetBy(id);

            if (UserToDelete == null || UserToDelete == default)
            {
                response.AppendNewError($"{id} névvel rendelkező diák nem található!");
                response.AppendNewError("A diák törlése nem sikerült!");
            }
            else
            {
                _dbContext.ChangeTracker.Clear();
                _dbContext.Entry(UserToDelete).State = EntityState.Deleted;
                await _dbContext.SaveChangesAsync();
            }
            return response;
        }
    }
}

