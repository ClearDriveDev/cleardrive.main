using ClearDrive.shared.Dtos;
using ClearDrive.shared.Models;

namespace ClearDrive.mobil.Extensions
{
    public static class PositionExtension
    {
        public static PositionDto ToPositionDto(this Position position)
        {
            return new PositionDto
            {
                Id = position.Id,
                Latitude = position.Latitude,
                Longitude = position.Longitude,
                StatusType = position.StatusType,
                Priority = position.Priority
            };
        }

        public static Position ToPosition(this PositionDto positionDto)
        {
            return new Position
            {
                Id = positionDto.Id,
                Latitude = positionDto.Latitude,
                Longitude = positionDto.Longitude,
                StatusType = positionDto.StatusType,
                Priority = positionDto.Priority
            };
        }
    }
}
