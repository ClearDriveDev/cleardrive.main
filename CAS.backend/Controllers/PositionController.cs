﻿using CAS.backend.Repos;
using CAS.shared.Converters;
using CAS.shared.Dtos;
using CAS.shared.Models.Datas.Entities;
using CAS.shared.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CAS.backend.Controllers
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
                    Longitude = entity.Longitude
                };
                return Ok(positionDto); 
            }
            return BadRequest("Az adatok elérhetetlenek!");
        }

        [HttpGet]
        public async Task<IActionResult> SelectAllRecordToListAsync()
        {
            var positions = await _posRepo.GetAll();
            List<PositionDto> positionDtos = positions.Select(position => new PositionDto
            {
                Id = position.Id,
                Latitude = position.Latitude,
                Longitude = position.Longitude
            }).ToList();

            return Ok(positionDtos); 
        }

        [HttpPut()]
        public async Task<ActionResult> UpdatePositionAsync(PositionDto entity)
        {
            Position position = new Position
            {
                Id = entity.Id,
                Latitude = entity.Latitude,
                Longitude = entity.Longitude
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
