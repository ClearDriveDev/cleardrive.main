using ClearDrive.backend.Dtos;
using ClearDrive.backend.Models.Datas.Entities;

namespace ClearDrive.backend.Converters
{
    public static class PositionConverter
    {
        public static PositionDto ToDto(this Position position)
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

        public static Position ToModel(this PositionDto positionDto)
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

        public static List<PositionDto> GetPositionsDtos(this List<Position> positions)
        {
            return positions.Select(posDto => ToDto(posDto)).ToList();
        }

        public static List<Position> GetPosition(this List<PositionDto> positionDtos)
        {
            return positionDtos.Select(posDto => ToModel(posDto)).ToList();
        }
    }
}
