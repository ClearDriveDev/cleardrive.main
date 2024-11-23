namespace CAS.backend.Models.Datas.Entities 
{ 

    public class Position
    {
        public Guid Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool HasId => Id != Guid.Empty;

        public Position(Guid id, double latitude, double longitude)
        {
            Id = id;
            Latitude = latitude;
            Longitude = longitude;
        }

        public Position(double latitude, double longitude)
        {
            Id = Guid.NewGuid();
            Latitude = latitude;
            Longitude = longitude;
        }

        public Position()
        {
            Id = Guid.NewGuid();
            Latitude = 0.0;
            Longitude = 0.0;
        }
    }
}
