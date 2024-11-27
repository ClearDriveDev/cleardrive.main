using CAS.mobil.Models.Enums;

namespace CAS.mobil.Models
{
    public class Position
    {
        public Guid Id { get; set; }
        public Location Location { get; set; }
        public StatusType StatusType { get; set; }
        public bool HasId => Id != Guid.Empty;


        public Position(Guid id,Location location, StatusType statusType)
        {
            Id = id;
            Location = location;
            StatusType = statusType;
        }

        public Position(Location location)
        { 
            Id = Guid.NewGuid();
            Location = location;
            StatusType = StatusType.ToDO;
        }

        public Position()
        {
            Id = Guid.NewGuid();
            Location = new Location();
            StatusType = StatusType.ToDO;
        }


    }
}