using CAS.backend.Context;
using CAS.shared.Models.Datas.Entities;
using CAS.shared.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace CAS.backend.Repos
{
    public class PositionRepo : IPositionRepo
    {
        private readonly CASInMemoryContext _dbContext;

        public PositionRepo(CASInMemoryContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Position>> GetAll()
        {
            return await _dbContext.Positions.ToListAsync();
        }

        public async Task<Position?> GetBy(Guid id)
        {
            return await _dbContext.Positions.FirstOrDefaultAsync(t => t.Id == id);
        }
        public async Task<ControllerResponse> UpdatePositionAsync(Position position)
        {
            ControllerResponse response = new ControllerResponse();
            _dbContext.ChangeTracker.Clear();
            _dbContext.Entry(position).State = EntityState.Modified;
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                response.AppendNewError(e.Message);
                response.AppendNewError($"{nameof(PositionRepo)} position, {nameof(UpdatePositionAsync)} metódusban hiba keletkezett");
                response.AppendNewError($"{position} frissítése nem sikerült!");
            }
            return response;
        }

        public async Task<ControllerResponse> DeleteAsync(Guid id)
        {
            ControllerResponse response = new ControllerResponse();

            Position? PositionToDelete = await GetBy(id);

            if (PositionToDelete == null || PositionToDelete == default)
            {
                response.AppendNewError($"{id} névvel rendelkező admin nem található!");
                response.AppendNewError("A admin törlése nem sikerült!");
            }
            else
            {
                _dbContext.ChangeTracker.Clear();
                _dbContext.Entry(PositionToDelete).State = EntityState.Deleted;
                await _dbContext.SaveChangesAsync();
            }
            return response;
        }

        public async Task<ControllerResponse> InsertAsync(Position position)
        {
            ControllerResponse response = new ControllerResponse();

            try
            {
                // Add the new position to the Positions DbSet
                await _dbContext.Positions.AddAsync(position);

                // Save the changes to the database
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                // If an error occurs, handle it and append it to the response
                response.AppendNewError($"An error occurred while inserting the position: {ex.Message}");
            }

            return response;
        }

    }
}
