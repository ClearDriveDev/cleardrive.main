using CAS.backend.Dtos;
using CAS.backend.Models.Datas.Entities;

namespace CAS.backend.Converters
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
              
            };
        }

        public static Position ToModel(this PositionDto positionDto)
        {
            return new Position
            {
                Id = positionDto.Id,
                Latitude = positionDto.Latitude,
                Longitude = positionDto.Longitude,
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
