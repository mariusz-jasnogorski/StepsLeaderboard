using StepsLeaderboard.Core.Models;

public static class InMemoryDb
{
    public static Dictionary<Guid, Team> Teams { get; } = new();
    public static Dictionary<Guid, Counter> Counters { get; } = new();
}
