﻿using CAS.shared.Dtos;
using CAS.shared.Models.Datas.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAS.shared.Converters
{
    public static class AdministratorConverter
    {
        public static AdministratorDto ToDto(this Administrator administrator)
        {
            return new AdministratorDto
            {
                UserName = administrator.UserName,
                Email = administrator.Email,
                Password = administrator.Password,
                Id = administrator.Id,
            };
        }

        public static Administrator ToModel(this AdministratorDto administratorDto)
        {
            return new Administrator
            {
                UserName = administratorDto.UserName,
                Email = administratorDto.Email,
                Password = administratorDto.Password,
                Id = administratorDto.Id,
            };
        }

        public static List<AdministratorDto> GetAdminsDtos(this List<Administrator> administrators)
        {
            return administrators.Select(adminDto => ToDto(adminDto)).ToList();
        }

        public static List<Administrator> GetAdmin(this List<AdministratorDto> administratorDtos)
        {
            return administratorDtos.Select(adminDto => ToModel(adminDto)).ToList();
        }
    }
}