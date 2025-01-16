using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StepsLeaderboard.Core.Interfaces;
using StepsLeaderboard.Core.Models;

namespace StepsLeaderboard.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]  
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _teamService;

        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<object>>> GetAllTeams()
        {
            var teams = await _teamService.GetAllTeamsAsync();
            var result = new List<object>();

            // Example: also fetch total steps from the team service
            foreach (var t in teams)
            {
                var totalSteps = await _teamService.GetTotalStepsForTeamAsync(t.Id);
                result.Add(new
                {
                    TeamId = t.Id,
                    t.Name,
                    TotalSteps = totalSteps
                });
            }
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<Team>> CreateTeam([FromBody] Team newTeamData)
        {
            if (string.IsNullOrWhiteSpace(newTeamData.Name))
            {
                return BadRequest("Team name is required.");
            }

            var createdTeam = await _teamService.CreateTeamAsync(newTeamData.Name);
            return CreatedAtAction(nameof(GetTeamById), new { teamId = createdTeam.Id }, createdTeam);
        }

        [HttpGet("{teamId:guid}")]
        [AllowAnonymous]
        public async Task<ActionResult<object>> GetTeamById(Guid teamId)
        {
            var team = await _teamService.GetTeamByIdAsync(teamId);
            if (team == null)
            {
                return NotFound("Team not found.");
            }

            var totalSteps = await _teamService.GetTotalStepsForTeamAsync(teamId);

            return Ok(new
            {
                team.Id,
                team.Name,
                TotalSteps = totalSteps,
                team.CounterIds
            });
        }

        [HttpDelete("{teamId:guid}")]
        public async Task<IActionResult> DeleteTeam(Guid teamId)
        {
            var success = await _teamService.DeleteTeamAsync(teamId);
            if (!success)
            {
                return NotFound("Team not found.");
            }
            return NoContent();
        }

        [HttpGet("{teamId:guid}/totalSteps")]
        [AllowAnonymous]
        public async Task<ActionResult<int>> GetTotalStepsForTeam(Guid teamId)
        {
            var team = await _teamService.GetTeamByIdAsync(teamId);
            if (team == null)
            {
                return NotFound("Team not found.");
            }
            var totalSteps = await _teamService.GetTotalStepsForTeamAsync(teamId);
            return Ok(totalSteps);
        }
    }
}
