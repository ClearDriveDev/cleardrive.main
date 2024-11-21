namespace WorkingWithMaps.Models
{
    public class Position
    {
        public Guid Id { get; set; }
        public Location Location { get; set; }
        public bool HasId => Id != Guid.Empty;


        public Position(Guid id,Location location)
        {
            Id = id;
            Location = location;
        }

        public Position(Location location)
        { 
            Id = Guid.NewGuid();
            Location = location;
        }

        public Position()
        {
            Id = Guid.NewGuid();
            Location = new Location();
        }


    }
}