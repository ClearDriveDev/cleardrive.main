﻿using ClearDrive.backend.Repos;
using ClearDrive.shared.Dtos;
using Microsoft.AspNetCore.Mvc;
using ClearDrive.shared.Models;
using ClearDrive.shared.Responses;

namespace ClearDrive.backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PositionController : ControllerBase
    {
        private IPositionRepo _posRepo;

        public PositionController(IPositionRepo positionRepo)
        {
            _posRepo = positionRepo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBy(Guid id)
        {
            Position? entity = await _posRepo.GetBy(id);
            if (entity != null)
            {
                PositionDto positionDto = new PositionDto
                {
                    Id = entity.Id,
                    Latitude = entity.Latitude,
                    Longitude = entity.Longitude,
                    StatusType=entity.StatusType,
                    Priority = entity.Priority
                };
                return Ok(positionDto); 
            }
            return BadRequest("Az adatok elérhetetlenek!");
        }

        [HttpPost]
        public async Task<IActionResult> InsertPositionAsync(PositionDto positionDto)
        {
            try
            {
                Position position = new Position
                {
                    Id = positionDto.Id,
                    Latitude = positionDto.Latitude,
                    Longitude = positionDto.Longitude,
                    StatusType=positionDto.StatusType,
                    Priority = positionDto.Priority
                };

                ControllerResponse response = await _posRepo.InsertAsync(position);

                if (response.HasError)
                {
                    return BadRequest(response);
                }

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet]
        public async Task<IActionResult> SelectAllRecordToListAsync()
        {
            var positions = await _posRepo.GetAll();
            List<PositionDto> positionDtos = positions.Select(position => new PositionDto
            {
                Id = position.Id,
                Latitude = position.Latitude,
                Longitude = position.Longitude,
                StatusType=position.StatusType,
                Priority = position.Priority
            }).ToList();

            return Ok(positionDtos); 
        }

        [HttpPut()]
        public async Task<IActionResult> UpdatePositionAsync(PositionDto entity)
        {
            Position position = new Position
            {
                Id = entity.Id,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude,
                StatusType= entity.StatusType,
                Priority = entity.Priority
            };

            ControllerResponse response = await _posRepo.UpdatePositionAsync(position);

            if (response.HasError)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePsoitionAsync(Guid id)
        {
            ControllerResponse response = await _posRepo.DeleteAsync(id);
            if (response.HasError)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

    }
}
