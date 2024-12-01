using ClearDrive.shared.Models.Enums;

namespace ClearDrive.shared.Dtos
{
    public class ProblemTicketDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public StatusType Status { get; set; }
        public ProblemType Problem { get; set; }

    }
}
