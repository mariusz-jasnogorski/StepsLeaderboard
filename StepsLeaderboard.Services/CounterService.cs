using StepsLeaderboard.Core.Interfaces;
using StepsLeaderboard.Core.Models;

namespace StepsLeaderboard.Services
{
    public class CounterService : ICounterService
    {
        private readonly ICounterRepository _counterRepository;

        public CounterService(ICounterRepository counterRepository)
        {
            _counterRepository = counterRepository;
        }

        public async Task<Counter> CreateCounterAsync(Guid teamId, string ownerName)
        {
            if (string.IsNullOrWhiteSpace(ownerName))
            {
                throw new ArgumentException("Owner name is required.");
            }

            return await _counterRepository.CreateCounterAsync(teamId, ownerName);
        }

        public async Task<Counter?> GetCounterByIdAsync(Guid counterId)
        {
            return await _counterRepository.GetCounterByIdAsync(counterId);
        }

        public async Task<IEnumerable<Counter>> GetCountersByTeamAsync(Guid teamId)
        {
            return await _counterRepository.GetCountersByTeamAsync(teamId);
        }

        public async Task<Counter?> IncrementCounterAsync(Guid counterId, int steps)
        {
            if (steps <= 0)
            {
                throw new ArgumentException("Steps must be > 0");
            }

            return await _counterRepository.IncrementCounterAsync(counterId, steps);
        }

        public async Task<bool> DeleteCounterAsync(Guid counterId)
        {
            return await _counterRepository.DeleteCounterAsync(counterId);
        }
    }
}
