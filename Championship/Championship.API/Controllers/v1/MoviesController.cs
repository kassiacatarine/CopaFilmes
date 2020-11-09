using System.Collections.Generic;
using System.Threading.Tasks;
using Championship.Application.Services;
using Championship.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace Championship.API.Controllers.v1
{
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _service;
        public MoviesController(IMovieService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MovieViewModel>>> GetAsync()
        {
            var response = await _service.GetMoviesAsync();
            if (response.Success)
                return Ok(response);
            return BadRequest(response);
        }
    }
}
