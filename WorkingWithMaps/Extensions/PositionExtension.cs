using WorkingWithMaps.Dtos;
using WorkingWithMaps.Models;

namespace WorkingWithMaps.Extensions
{
    public static class PositionExtension
    {
        public static PositionDto ToPositionDto(this Position position)
        {
            return new PositionDto
            {
                Id = position.Id,
                Latitude = position.Location.Latitude,
                Longitude = position.Location.Longitude
            };
        }

        public static Position ToPosition(this PositionDto positionDto)
        {
            return new Position
            {
                Id = positionDto.Id,
                Location = new Location(positionDto.Latitude, positionDto.Longitude)
            };
        }
    }
}
