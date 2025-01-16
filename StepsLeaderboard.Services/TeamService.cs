using StepsLeaderboard.Core.Interfaces;
using StepsLeaderboard.Core.Models;
using System.Xml.Linq;

namespace StepsLeaderboard.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;

        
        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public async Task<Team> CreateTeamAsync(string name)
        {
            // Additional domain validations can go here
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException("Team name is required.");
            }

            return await _teamRepository.CreateTeamAsync(name);
        }

        public async Task<IEnumerable<Team>> GetAllTeamsAsync()
        {
            return await _teamRepository.GetAllTeamsAsync();
        }

        public async Task<Team?> GetTeamByIdAsync(Guid teamId)
        {
            return await _teamRepository.GetTeamByIdAsync(teamId);
        }

        public async Task<bool> DeleteTeamAsync(Guid teamId)
        {
            return await _teamRepository.DeleteTeamAsync(teamId);
        }

        public async Task<int> GetTotalStepsForTeamAsync(Guid teamId)
        {
            var team = await _teamRepository.GetTeamByIdAsync(teamId);
            if (team == null)
            {
                throw new ArgumentException("Team not found.");
            }   

            return await _teamRepository.GetTotalStepsForTeamAsync(teamId);
        }
    }
}
