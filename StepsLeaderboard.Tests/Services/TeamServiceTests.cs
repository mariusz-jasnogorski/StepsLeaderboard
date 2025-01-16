using Moq;
using StepsLeaderboard.Core.Interfaces;
using StepsLeaderboard.Core.Models;
using StepsLeaderboard.Services;

namespace StepsLeaderboard.Tests.Services
{
    [TestClass]
    public class TeamServiceTests
    {
        private Mock<ITeamRepository> _teamRepoMock;
        private TeamService _service;

        [TestInitialize]
        public void TestInitialize()
        {
            _teamRepoMock = new Mock<ITeamRepository>();
            _service = new TeamService(_teamRepoMock.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public async Task CreateTeamAsync_Should_ThrowException_IfNameIsEmpty()
        {
            // Arrange
            var emptyName = "";

            // Act
            await _service.CreateTeamAsync(emptyName);

            // Assert - handled by [ExpectedException]
        }

        [TestMethod]
        public async Task CreateTeamAsync_Should_CallRepository_AndReturnTeam()
        {
            // Arrange
            var expectedTeam = new Team { Id = Guid.NewGuid(), Name = "Mock Team" };
            _teamRepoMock
                .Setup(r => r.CreateTeamAsync("Mock Team"))
                .ReturnsAsync(expectedTeam);

            // Act
            var result = await _service.CreateTeamAsync("Mock Team");

            // Assert
            Assert.AreEqual("Mock Team", result.Name);
            _teamRepoMock.Verify(r => r.CreateTeamAsync("Mock Team"), Times.Once);
        }

        [TestMethod]
        public async Task DeleteTeamAsync_Should_ReturnFalse_IfTeamNotFound()
        {
            // Arrange
            var fakeTeamId = Guid.NewGuid();
            _teamRepoMock
                .Setup(r => r.DeleteTeamAsync(fakeTeamId))
                .ReturnsAsync(false);

            // Act
            var result = await _service.DeleteTeamAsync(fakeTeamId);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task GetTotalStepsForTeamAsync_Should_CallRepository()
        {
            // Arrange
            var teamId = Guid.NewGuid();
            _teamRepoMock
                .Setup(r => r.GetTotalStepsForTeamAsync(teamId))
                .ReturnsAsync(1234);

            // Act
            var steps = await _service.GetTotalStepsForTeamAsync(teamId);

            // Assert
            Assert.AreEqual(1234, steps);
            _teamRepoMock.Verify(r => r.GetTotalStepsForTeamAsync(teamId), Times.Once);
        }
    }
}
