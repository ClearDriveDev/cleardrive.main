
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
                Latitude = position.Location.Latitude, // A Location osztály tartalmazza az adatokat
                Longitude = position.Location.Longitude
            };
        }

        public static Position ToPosition(this PositionDto positionDto)
        {
            return new Position
            {
                Id = positionDto.Id,
                Location = new Location(positionDto.Latitude, positionDto.Longitude) // Létrehozunk egy új Location objektumot
            };
        }
    }
}
