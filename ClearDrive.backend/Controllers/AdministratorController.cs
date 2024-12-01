using ClearDrive.backend.Repos;
using ClearDrive.backend.Converters;
using ClearDrive.shared.Dtos;
using ClearDrive.backend.Models.Datas.Entities;
using Microsoft.AspNetCore.Mvc;
using ClearDrive.shared.Responses;

namespace ClearDrive.backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdministratorController : ControllerBase
    {

        private IAdministratorRepo _adminRepo;

        public AdministratorController(IAdministratorRepo administratorRepo)
        {
            _adminRepo = administratorRepo;
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBy(Guid id)
        {
            Administrator? entity = new();
            if (_adminRepo is not null)
            {
                entity = await _adminRepo.GetBy(id);
                if (entity != null)
                    return Ok(entity.ToDto());
            }
            return BadRequest("Az adatok elérhetetlenek!");
        }

        [HttpGet]
        public async Task<IActionResult> SelectAllRecordToListAsync()
        {
            List<Administrator>? users = new();

            if (_adminRepo != null)
            {
                users = await _adminRepo.GetAll();
                return Ok(users.GetAdminsDtos());
            }
            return BadRequest("Az adatok elérhetetlenek!");
        }

        [HttpPut()]
        public async Task<ActionResult> UpdateAdminAsync(AdministratorDto entity)
        {
            ControllerResponse response = new();
            if (_adminRepo is not null)
            {
                response = await _adminRepo.UpdateAdminAsync(entity.ToModel());
                if (response.HasError)
                {
                    return BadRequest(response);
                }
                else
                {
                    return Ok(response);
                }
            }
            response.ClearAndAddError("Az adatok frissítés nem lehetséges!");
            return BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdminAsync(Guid id)
        {
            ControllerResponse response = new();
            if (_adminRepo is not null)
            {
                response = await _adminRepo.DeleteAsync(id);
                if (response.HasError)
                {
                    Console.WriteLine(response.Error);
                }
                else
                {
                    return Ok(response);
                }
            }
            response.ClearAndAddError("Az adatok törlése nem lehetséges!");
            return BadRequest(response);
        }
    }
}
