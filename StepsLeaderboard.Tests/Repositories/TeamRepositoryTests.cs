using StepsLeaderboard.Core.Models;
using StepsLeaderboard.Repositories;

namespace StepsLeaderboard.Tests.Repositories
{
    [TestClass]
    public class TeamRepositoryTests
    {
        private InMemoryTeamRepository _repository;

        [TestInitialize]
        public void TestInitialize()
        {
            // Clears the static in-memory DB to ensure a clean state for each test
            InMemoryDb.Teams.Clear();
            InMemoryDb.Counters.Clear();

            _repository = new InMemoryTeamRepository();
        }

        [TestMethod]
        public async Task CreateTeamAsync_Should_AddTeamToInMemoryStore()
        {
            // Arrange
            string teamName = "Test Team";

            // Act
            var createdTeam = await _repository.CreateTeamAsync(teamName);

            // Assert
            Assert.IsNotNull(createdTeam);
            Assert.AreEqual(teamName, createdTeam.Name);
            Assert.IsTrue(InMemoryDb.Teams.ContainsKey(createdTeam.Id));
        }

        [TestMethod]
        public async Task GetTeamByIdAsync_Should_ReturnTeam_When_Exists()
        {
            // Arrange
            var team = await _repository.CreateTeamAsync("Another Team");
            var teamId = team.Id;

            // Act
            var fetchedTeam = await _repository.GetTeamByIdAsync(teamId);

            // Assert
            Assert.IsNotNull(fetchedTeam);
            Assert.AreEqual(teamId, fetchedTeam.Id);
            Assert.AreEqual("Another Team", fetchedTeam.Name);
        }

        [TestMethod]
        public async Task GetTeamByIdAsync_Should_ReturnNull_When_NotFound()
        {
            // Arrange
            var nonExistentId = Guid.NewGuid();

            // Act
            var fetchedTeam = await _repository.GetTeamByIdAsync(nonExistentId);

            // Assert
            Assert.IsNull(fetchedTeam);
        }

        [TestMethod]
        public async Task DeleteTeamAsync_Should_RemoveTeam_FromInMemoryStore()
        {
            // Arrange
            var team = await _repository.CreateTeamAsync("Team to Delete");
            var teamId = team.Id;

            // Act
            bool deleted = await _repository.DeleteTeamAsync(teamId);

            // Assert
            Assert.IsTrue(deleted, "Expected delete to return true");
            Assert.IsFalse(InMemoryDb.Teams.ContainsKey(teamId));
        }

        [TestMethod]
        public async Task GetAllTeamsAsync_Should_ReturnAllTeams()
        {
            // Arrange
            await _repository.CreateTeamAsync("Team A");
            await _repository.CreateTeamAsync("Team B");

            // Act
            var teams = await _repository.GetAllTeamsAsync();

            // Assert
            Assert.AreEqual(2, teams.Count());
        }

        [TestMethod]
        public async Task GetTotalStepsForTeamAsync_Should_SumCounters()
        {
            // Arrange
            var team = await _repository.CreateTeamAsync("Team with Counters");
            var teamId = team.Id;

            // Manually add counters in the in-memory store for testing
            var c1 = new Counter { TeamId = teamId, Steps = 1000 };
            var c2 = new Counter { TeamId = teamId, Steps = 500 };
            InMemoryDb.Counters[c1.Id] = c1;
            InMemoryDb.Counters[c2.Id] = c2;

            // Act
            var totalSteps = await _repository.GetTotalStepsForTeamAsync(teamId);

            // Assert
            Assert.AreEqual(1500, totalSteps);
        }
    }
}
