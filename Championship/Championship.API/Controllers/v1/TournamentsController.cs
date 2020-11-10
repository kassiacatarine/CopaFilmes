using System.Threading.Tasks;
using Championship.Application.Services;
using Championship.Application.ViewModels;
using Championship.Domain.SeedWork;
using Microsoft.AspNetCore.Mvc;

namespace Championship.API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class TournamentsController : ControllerBase
    {
        private readonly ITournamentService _service;
        public TournamentsController(ITournamentService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<ActionResult<Response<TournamentViewModel>>> PostAsync([FromBody] CreateTournamentViewModel model)
        {
            var response = await _service.CreateAsync(model);
            if(response.Success) 
                return Created(nameof(PostAsync), response);
            return BadRequest(response);
        }
    }
}
