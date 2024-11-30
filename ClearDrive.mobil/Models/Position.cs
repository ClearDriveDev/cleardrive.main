using ClearDrive.mobil.Models.Enums;

namespace ClearDrive.mobil.Models
{
    public class Position
    {
        public Guid Id { get; set; }
        public Location Location { get; set; }
        public StatusType StatusType { get; set; }
        public bool HasId => Id != Guid.Empty;
        public int Priority { get; set; }


        public Position(Guid id, Location location, StatusType statusType, int priority)
        {
            Id = id;
            Location = location;
            StatusType = statusType;
            Priority = priority;
        }

        public Position(Location location)
        {
            Id = Guid.NewGuid();
            Location = location;
            StatusType = StatusType.ToDO;
            Priority = 0;
        }

        public Position()
        {
            Id = Guid.NewGuid();
            Location = new Location();
            StatusType = StatusType.ToDO;
            Priority = 0;
        }


    }
}