using ClearDrive.mobil.Dtos;
using ClearDrive.mobil.Models;

namespace ClearDrive.mobil.Extensions
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
                Priority = position.Priority,
            };
        }

        public static Position ToPosition(this PositionDto positionDto)
        {
            return new Position
            {
                Id = positionDto.Id,
                Location = new Location(positionDto.Latitude, positionDto.Longitude),
                StatusType = positionDto.StatusType,
                Priority = positionDto.Priority
            };
        }
    }
}
