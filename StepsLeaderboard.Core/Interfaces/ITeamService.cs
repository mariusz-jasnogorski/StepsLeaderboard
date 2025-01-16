using StepsLeaderboard.Core.Models;

namespace StepsLeaderboard.Core.Interfaces
{
    public interface ITeamService
    {
        Task<Team> CreateTeamAsync(string name);

        Task<IEnumerable<Team>> GetAllTeamsAsync();

        Task<Team?> GetTeamByIdAsync(Guid teamId);

        Task<bool> DeleteTeamAsync(Guid teamId);

        Task<int> GetTotalStepsForTeamAsync(Guid teamId);
    }
}
