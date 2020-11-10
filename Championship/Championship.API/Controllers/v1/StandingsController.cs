using System.Threading.Tasks;
using Championship.Application.Services;
using Championship.Application.ViewModels;
using Championship.Domain.SeedWork;
using Microsoft.AspNetCore.Mvc;

namespace Championship.API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class StandingsController : ControllerBase
    {
        private readonly IStandingService _service;
        public StandingsController(IStandingService service)
        {
            _service = service;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<StandingViewModel>>> GetAsync(string id)
        {
            var response = await _service.GetByIdAsync(id);
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
