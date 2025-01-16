using StepsLeaderboard.Core.Models;

namespace StepsLeaderboard.Core.Interfaces
{
    public interface ICounterRepository
    {
        Task<Counter> CreateCounterAsync(Guid teamId, string ownerName);

        Task<Counter?> GetCounterByIdAsync(Guid counterId);

        Task<IEnumerable<Counter>> GetCountersByTeamAsync(Guid teamId);

        Task<Counter?> IncrementCounterAsync(Guid counterId, int steps);

        Task<bool> DeleteCounterAsync(Guid counterId);
    }
}
