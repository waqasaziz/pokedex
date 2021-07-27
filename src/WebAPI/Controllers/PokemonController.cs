using System.Threading.Tasks;
using Domain.DataContracts;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace WebAPI.Controllers
{
    [ApiController, ApiVersion("1.0")]
    [Route("[controller]")]
    public class PokemonController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public PokemonController(ISearchService paymentService)
        {
            _searchService = paymentService;
        }


        [HttpGet("{name}")]
        public async Task<IActionResult> Get(string name)
        {
            var response = await _searchService.FindPokemon(name);

            return BuildResult(response);
        }

        [HttpGet("translated/{name}")]
        public async Task<IActionResult> GetTranslated(string name)
        {
            var response = await _searchService.FindPokemon(name, shouldTranslate: true);

            return BuildResult(response);
        }

        private IActionResult BuildResult(SearchResult response)
        {
            if (response.Status == SearchResultStatus.NotFound)
                return NotFound();

            else if (response.Status == SearchResultStatus.Error)
                return StatusCode(StatusCodes.Status500InternalServerError, "An internal server error occured, please try again later");

            return Ok(response.Pokemon);
        }
    }
}
