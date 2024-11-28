using ClearDrive.backend.Models.Datas.Enums;

namespace ClearDrive.backend.Dtos
{
    public class ProblemTicketDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public StatusType Status { get; set; }
        public ProblemType Problem { get; set; }

    }
}
