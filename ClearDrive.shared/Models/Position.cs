using ClearDrive.shared.Models.Enums;

namespace ClearDrive.shared.Models
{

    public class Position
    {
        public Guid Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public StatusType StatusType { get; set; }
        public bool HasId => Id != Guid.Empty;
        public int Priority { get; set; }

        public Position(Guid id, double latitude, double longitude, StatusType statusType, int priority)
        {
            Id = id;
            Latitude = latitude;
            Longitude = longitude;
            StatusType = statusType;
            Priority = priority;
        }

        public Position(double latitude, double longitude, int priority)
        {
            Id = Guid.NewGuid();
            Latitude = latitude;
            Longitude = longitude;
            StatusType = StatusType.ToDO;
            Priority = priority;
        }

        public Position(double latitude, double longitude)
        {
            Id = Guid.NewGuid();
            Latitude = latitude;
            Longitude = longitude;
            StatusType = StatusType.ToDO;
            Priority = 1;
        }

        public Position()
        {
            Id = Guid.NewGuid();
            Latitude = 0.0;
            Longitude = 0.0;
            StatusType = StatusType.ToDO;
            Priority = 1;
        }
    }
}
