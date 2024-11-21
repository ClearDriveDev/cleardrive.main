using CAS.shared.Models.Datas.Entities;
using CAS.shared.Models.Responses;

namespace CAS.backend.Repos
{
    public interface IProblemTicketRepo
    {
        Task<List<ProblemTicket>> GetAll();
        Task<ProblemTicket?> GetBy(Guid id);
        Task<ControllerResponse> UpdateProblemAsync(ProblemTicket problemticket);
        Task<ControllerResponse> DeleteAsync(Guid id);
    }
}
