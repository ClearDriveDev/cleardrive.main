using ClearDrive.backend.Converters;
using ClearDrive.backend.Dtos;
using ClearDrive.backend.Models.Datas.Entities;
using ClearDrive.backend.Models.Responses;
using ClearDrive.backend.Repos;
using Microsoft.AspNetCore.Mvc;

namespace ClearDrive.backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProblemTicketController : ControllerBase
    {
        private IProblemTicketRepo _problemRepo;

        public ProblemTicketController(IProblemTicketRepo problemRepo)
        {
            _problemRepo = problemRepo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBy(Guid id)
        {
            ProblemTicket? entity = new();
            if (_problemRepo is not null)
            {
                entity = await _problemRepo.GetBy(id);
                if (entity != null)
                    return Ok(entity.ToDto());
            }
            return BadRequest("Az adatok elérhetetlenek!");
        }

        [HttpGet]
        public async Task<IActionResult> SelectAllRecordToListAsync()
        {
            List<ProblemTicket>? users = new();

            if (_problemRepo != null)
            {
                users = await _problemRepo.GetAll();
                return Ok(users.GetProblemsDtos());
            }
            return BadRequest("Az adatok elérhetetlenek!");
        }

        [HttpPut()]
        public async Task<ActionResult> UpdateProblemAsync(ProblemTicketDto entity)
        {
            ControllerResponse response = new();
            if (_problemRepo is not null)
            {
                response = await _problemRepo.UpdateProblemAsync(entity.ToModel());
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
        public async Task<IActionResult> DeleteProblemAsync(Guid id)
        {
            ControllerResponse response = new();
            if (_problemRepo is not null)
            {
                response = await _problemRepo.DeleteAsync(id);
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
