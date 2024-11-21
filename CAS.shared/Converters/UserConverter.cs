using CAS.shared.Dtos;
using CAS.shared.Models.Datas.Entities;

namespace CAS.shared.Converters
{
    public static class UserConverter
    {
        public static UserDto ToDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Password = user.Password,
                TelNumber = user.TelNumber,
            };
        }

        public static User ToModel(this UserDto userDto)
        {
            return new User
            {
                Id = userDto.Id,
                UserName = userDto.UserName,
                Email = userDto.Email,
                Password = userDto.Password,
                TelNumber = userDto.TelNumber,
            };
        }

        public static List<UserDto> GetUsersDtos(this List<User> users)
        {
            return users.Select(userDto => ToDto(userDto)).ToList();
        }

        public static List<User> GetUser(this List<UserDto> userDtos)
        {
            return userDtos.Select(userDto => ToModel(userDto)).ToList();
        }
    }
}

