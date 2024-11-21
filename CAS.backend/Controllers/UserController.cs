using CAS.backend.Repos;
using CAS.shared.Converters;
using CAS.shared.Dtos;
using CAS.shared.Models.Datas.Entities;
using CAS.shared.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace CAS.backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private IUserRepo _userRepo;

        public UserController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet("{userName}")]
        public async Task<IActionResult> GetBy(Guid id)
        {
            User? entity = new();
            if (_userRepo is not null)
            {
                entity = await _userRepo.GetBy(id);
                if (entity != null)
                    return Ok(entity.ToDto());
            }
            return BadRequest("Az adatok elérhetetlenek!");
        }

        [HttpGet]
        public async Task<IActionResult> SelectAllRecordToListAsync()
        {
            List<User>? users = new();

            if (_userRepo != null)
            {
                users = await _userRepo.GetAll();
                return Ok(users.GetUsersDtos());
            }
            return BadRequest("Az adatok elérhetetlenek!");
        }

        [HttpPut()]
        public async Task<ActionResult> UpdateUserAsync(UserDto entity)
        {
            ControllerResponse response = new();
            if (_userRepo is not null)
            {
                response = await _userRepo.UpdateUserAsync(entity.ToModel());
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
        public async Task<IActionResult> DeleteUserAsync(Guid id)
        {
            ControllerResponse response = new();
            if (_userRepo is not null)
            {
                response = await _userRepo.DeleteAsync(id);
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
