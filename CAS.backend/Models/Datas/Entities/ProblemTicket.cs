using CAS.backend.Models.Datas.Enums;

namespace CAS.backend.Models.Datas.Entities
{
    public class ProblemTicket
    {
        
        public ProblemTicket()
        {
            Id = Guid.NewGuid();
            Description = string.Empty;
            Status = StatusType.Done;
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
