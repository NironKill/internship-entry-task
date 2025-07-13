using FluentAssertions;
using Moq;
using System.Linq.Expressions;
using TicTacToe.Application.DTOs.Player;
using TicTacToe.Application.Repositories.Interfaces;
using TicTacToe.Application.Requests.Players.Read.GetById;
using TicTacToe.Domain.Entity;

namespace TicTacToe.Test.Requests.Players
{
    public class GetByIdPlayerHandlerTest
    {
        [Fact]
        public async Task Handle_ShouldReturnPlayerGetDTO_WhenPlayerExists()
        {
            // Arrange
            var playerId = Guid.NewGuid();
            var expectedName = "Player1";

            var request = new GetByIdPlayerRequest { Id = playerId };

            var expectedDto = new PlayerGetDTO
            {
                Id = playerId,
                Name = expectedName
            };

            var playerRepoMock = new Mock<IPlayerRepository>();

            playerRepoMock.Setup(x => x.Get(
                It.IsAny<Expression<Func<Player, bool>>>(),
                It.IsAny<Func<Player, PlayerGetDTO>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedDto);

            var handler = new GetByIdPlayerHandler(playerRepoMock.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(playerId);
            result.Name.Should().Be(expectedName);

            playerRepoMock.Verify(x => x.Get(
                It.IsAny<Expression<Func<Player, bool>>>(),
                It.IsAny<Func<Player, PlayerGetDTO>>(),
                It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
