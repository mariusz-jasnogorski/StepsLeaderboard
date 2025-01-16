using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StepsLeaderboard.Core.Interfaces;
using StepsLeaderboard.Core.Models;

namespace StepsLeaderboard.WebAPI.Controllers
{
    [ApiController]
    [Route("api")]
    [Authorize]  
    public class CountersController : ControllerBase
    {
        private readonly ICounterService _counterService;

        public CountersController(ICounterService counterService)
        {
            _counterService = counterService;
        }

        [HttpPost("teams/{teamId:guid}/counters")]
        [AllowAnonymous]
        public async Task<ActionResult<Counter>> CreateCounter(Guid teamId, [FromBody] Counter newCounterData)
        {
            if (string.IsNullOrWhiteSpace(newCounterData.OwnerName))
            {
                return BadRequest("OwnerName is required.");
            }
            try
            {
                var createdCounter = await _counterService.CreateCounterAsync(teamId, newCounterData.OwnerName);
                return CreatedAtAction(nameof(GetCounterById),
                    new { counterId = createdCounter.Id },
                    createdCounter);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("counters/{counterId:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<Counter>> GetCounterById(Guid counterId)
        {
            var counter = await _counterService.GetCounterByIdAsync(counterId);
            if (counter == null)
            {
                return NotFound("Counter not found.");
            }
            return Ok(counter);
        }

        [HttpGet("teams/{teamId:guid}/counters")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Counter>>> GetCountersByTeam(Guid teamId)
        {
            var counters = await _counterService.GetCountersByTeamAsync(teamId);
            return Ok(counters);
        }

        [HttpPatch("counters/{counterId:guid}/increment")]
        [AllowAnonymous]
        public async Task<ActionResult<Counter>> IncrementCounter(Guid counterId, [FromBody] IncrementRequest request)
        {
            if (request.Steps <= 0)
            {
                return BadRequest("Steps must be > 0");
            }

            var updatedCounter = await _counterService.IncrementCounterAsync(counterId, request.Steps);
            if (updatedCounter == null)
            {
                return NotFound("Counter not found.");
            }

            return Ok(updatedCounter);
        }

        [HttpDelete("counters/{counterId:guid}")]
        public async Task<IActionResult> DeleteCounter(Guid counterId)
        {
            var success = await _counterService.DeleteCounterAsync(counterId);
            if (!success)
            {
                return NotFound("Counter not found.");
            }
            return NoContent();
        }
    }
}
