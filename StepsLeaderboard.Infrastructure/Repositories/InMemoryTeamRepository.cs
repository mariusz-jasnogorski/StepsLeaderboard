using StepsLeaderboard.Core.Interfaces;
using StepsLeaderboard.Core.Models;

namespace StepsLeaderboard.Repositories
{
    public class InMemoryTeamRepository : ITeamRepository
    {
        public async Task<Team> CreateTeamAsync(string name)
        {
            var team = new Team { Name = name };

            InMemoryDb.Teams[team.Id] = team;
            return await Task.FromResult(team);
        }

        public async Task<IEnumerable<Team>> GetAllTeamsAsync()
        {
            return await Task.FromResult(InMemoryDb.Teams.Values);
        }

        public async Task<Team?> GetTeamByIdAsync(Guid teamId)
        {
            InMemoryDb.Teams.TryGetValue(teamId, out var team);

            return await Task.FromResult(team);
        }

        public async Task<bool> DeleteTeamAsync(Guid teamId)
        {
            if (InMemoryDb.Teams.ContainsKey(teamId))
            {
                // Remove all counters for that team
                var countersToRemove = InMemoryDb.Counters.Values.Where(c => c.TeamId == teamId).Select(c => c.Id).ToList();

                foreach (var cid in countersToRemove)
                {
                    InMemoryDb.Counters.Remove(cid);
                }

                InMemoryDb.Teams.Remove(teamId);
                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }

        public async Task<int> GetTotalStepsForTeamAsync(Guid teamId)
        {
            var total = InMemoryDb.Counters.Values.Where(c => c.TeamId == teamId).Sum(c => c.Steps);

            return await Task.FromResult(total);
        }
    }
}
