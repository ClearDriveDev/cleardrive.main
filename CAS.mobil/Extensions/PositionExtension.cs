using CAS.mobil.Dtos;
using CAS.mobil.Models;

namespace CAS.mobil.Extensions
{
    public static class PositionExtension
    {
        public static PositionDto ToPositionDto(this Position position)
        {
            return new PositionDto
            {
                Id = position.Id,
                Latitude = position.Location.Latitude,
                Longitude = position.Location.Longitude,
                StatusType = position.StatusType,
            };
        }

        public static Position ToPosition(this PositionDto positionDto)
        {
            return new Position
            {
                Id = positionDto.Id,
                Location = new Location(positionDto.Latitude, positionDto.Longitude),
                StatusType = positionDto.StatusType,  
            };
        }
    }
}
