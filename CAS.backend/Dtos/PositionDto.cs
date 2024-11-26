﻿using CAS.backend.Models.Datas.Enums;

namespace CAS.backend.Dtos
{
    public class PositionDto
    {
        public Guid Id { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public StatusType StatusType { get; set; }

        public PositionDto(Guid id, double latitude, double longitude, StatusType statusType)
        {
            Id = id;
            Latitude = latitude;
            Longitude = longitude;
            StatusType = statusType;
        }

        public PositionDto()
        {
            Id = Guid.NewGuid();
            Latitude = 0.0;
            Longitude = 0.0;
            StatusType = StatusType.ToDO;
        }
    }

}
