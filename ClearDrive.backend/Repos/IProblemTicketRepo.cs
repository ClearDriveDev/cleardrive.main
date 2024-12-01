using ClearDrive.backend.Models.Datas.Entities;
using ClearDrive.shared.Responses;

namespace ClearDrive.backend.Repos
{
    public interface IProblemTicketRepo
    {
        Task<List<ProblemTicket>> GetAll();
        Task<ProblemTicket?> GetBy(Guid id);
        Task<ControllerResponse> UpdateProblemAsync(ProblemTicket problemticket);
        Task<ControllerResponse> DeleteAsync(Guid id);
    }
}
