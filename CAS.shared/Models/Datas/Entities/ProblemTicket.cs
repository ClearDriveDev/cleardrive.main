using CAS.shared.Models.Datas.Enums;

namespace CAS.shared.Models.Datas.Entities
{
    public class ProblemTicket
    {
        
        public ProblemTicket()
        {
            Id = Guid.NewGuid();
            Description = string.Empty;
            Status = StatusType.Denied;
            Problem = ProblemType.RoadProblems;   
        }

        public ProblemTicket(Guid id, string description, StatusType status, ProblemType problem)
        {
            Id = id;
            Description = description;
            Status = status;
            Problem = problem;
        }

        public ProblemTicket(string description, StatusType status, ProblemType problem)
        {
            Id = Guid.NewGuid();
            Description = description;
            Status = status;
            Problem = problem;
        }

        public Guid Id { get; set; }
        public string Description { get; set; }
        public StatusType Status { get; set; }
        public ProblemType Problem { get; set; }
    }
}
