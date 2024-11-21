namespace CAS.shared.Dtos
{
    public class PositionDto
    {
        public Guid Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public PositionDto(Guid id, double latitude, double longitude)
        {
            Id = id;
            Latitude = latitude;
            Longitude = longitude;
        }

        public PositionDto()
        {
            Id = Guid.NewGuid();
            Latitude = 0.0;
            Longitude = 0.0;
        }
    }

}
