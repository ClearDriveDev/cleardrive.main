using CAS.shared.Dtos;
using CAS.shared.Models.Datas.Entities;

namespace CAS.shared.Converters
{
    public static class ProblemTicketConverter
    {
        public static ProblemTicketDto ToDto(this ProblemTicket problemTicket)
        {
            return new ProblemTicketDto
            {
                Id = problemTicket.Id,
                Description = problemTicket.Description,
                Status = problemTicket.Status,
                Problem = problemTicket.Problem
            };
        }

        public static ProblemTicket ToModel(this ProblemTicketDto problemTicket) 
        {
            return new ProblemTicket
            {
                Id = problemTicket.Id,
                Description = problemTicket.Description,
                Status = problemTicket.Status,
                Problem = problemTicket.Problem
            };
        }

        public static List<ProblemTicketDto> GetProblemsDtos(this List<ProblemTicket> problemTickets)
        {
            return problemTickets.Select(problemDto => ToDto(problemDto)).ToList();
        }

        public static List<ProblemTicket> GetProblem(this List<ProblemTicketDto> problemTicketDtos)
        {
            return problemTicketDtos.Select(problemDto => ToModel(problemDto)).ToList();
        }
    }
}
