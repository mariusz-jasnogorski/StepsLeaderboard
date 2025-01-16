using StepsLeaderboard.Core.Interfaces;
using StepsLeaderboard.Core.Models;

namespace StepsLeaderboard.Repositories
{
    public class InMemoryCounterRepository : ICounterRepository
    {
        public async Task<Counter> CreateCounterAsync(Guid teamId, string ownerName)
        {
            // Checking if the team exists is optional in the repository, but it can also be done in the service.
            if (!InMemoryDb.Teams.ContainsKey(teamId))
            {
                throw new ArgumentException("Team does not exist");
            }

            var counter = new Counter
            {
                TeamId = teamId,
                OwnerName = ownerName,
                Steps = 0
            };

            InMemoryDb.Counters[counter.Id] = counter;
            InMemoryDb.Teams[teamId].CounterIds.Add(counter.Id);

            return await Task.FromResult(counter);
        }

        public async Task<Counter?> GetCounterByIdAsync(Guid counterId)
        {
            InMemoryDb.Counters.TryGetValue(counterId, out var counter);
            return await Task.FromResult(counter);
        }

        public async Task<IEnumerable<Counter>> GetCountersByTeamAsync(Guid teamId)
        {
            var counters = InMemoryDb.Counters.Values.Where(c => c.TeamId == teamId);

            return await Task.FromResult(counters);
        }

        public async Task<Counter?> IncrementCounterAsync(Guid counterId, int steps)
        {
            if (InMemoryDb.Counters.TryGetValue(counterId, out var counter))
            {
                counter.Steps += steps;
                return await Task.FromResult(counter);
            }
            return await Task.FromResult<Counter?>(null);
        }

        public async Task<bool> DeleteCounterAsync(Guid counterId)
        {
            if (InMemoryDb.Counters.TryGetValue(counterId, out var counter))
            {
                // Remove from the Team’s reference
                var team = InMemoryDb.Teams[counter.TeamId];
                team.CounterIds.Remove(counter.Id);

                InMemoryDb.Counters.Remove(counterId);
                return await Task.FromResult(true);
            }
            return await Task.FromResult(false);
        }
    }
}
