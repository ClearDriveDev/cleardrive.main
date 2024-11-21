using CAS.shared.Models.Responses;
using CAS.backend.Context;
using CAS.shared.Models.Datas.Entities;
using Microsoft.EntityFrameworkCore;

namespace CAS.backend.Repos
{
    public class ProblemTicketRepo : IProblemTicketRepo
    {
        private readonly CASInMemoryContext _dbContext;

        public ProblemTicketRepo(CASInMemoryContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<ProblemTicket>> GetAll()
        {
            return await _dbContext.ProblemTickets.ToListAsync();
        }

        public async Task<ProblemTicket?> GetBy(Guid id)
        {
            return await _dbContext.ProblemTickets.FirstOrDefaultAsync(t => t.Id == id);
        }
        public async Task<ControllerResponse> UpdateProblemAsync(ProblemTicket problemTicket)
        {
            ControllerResponse response = new ControllerResponse();
            _dbContext.ChangeTracker.Clear();
            _dbContext.Entry(problemTicket).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                response.AppendNewError(e.Message);
                response.AppendNewError($"{nameof(ProblemTicketRepo)} problem, {nameof(UpdateProblemAsync)} metódusban hiba keletkezett");
                response.AppendNewError($"{problemTicket} frissítése nem sikerült!");
            }
            return response;
        }

        public async Task<ControllerResponse> DeleteAsync(Guid id)
        {
            ControllerResponse response = new ControllerResponse();
            
            ProblemTicket? ProblemToDelete = await GetBy(id);
           
            if (ProblemToDelete == null || ProblemToDelete == default)
            {
                response.AppendNewError($"{id} idével rendelkező probléma nem található!");
                response.AppendNewError("A diák törlése nem sikerült!");
            }
            else
            {
                _dbContext.ChangeTracker.Clear();
                _dbContext.Entry(ProblemToDelete).State = EntityState.Deleted;
                await _dbContext.SaveChangesAsync();
            }
            return response;
        }
    }
}
