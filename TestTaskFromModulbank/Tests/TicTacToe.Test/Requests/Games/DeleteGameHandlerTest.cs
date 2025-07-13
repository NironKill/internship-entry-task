using FluentAssertions;
using Moq;
using System.Linq.Expressions;
using TicTacToe.Application.Repositories.Interfaces;
using TicTacToe.Application.Requests.Games.Write.Delete;
using TicTacToe.Domain.Entity;

namespace TicTacToe.Test.Requests.Games
{
    public class DeleteGameHandlerTest
    {
        [Fact]
        public async Task Handle_ShouldReturnTrue_WhenGameSuccessfullyDeleted()
        {
            // Arrange
            var gameId = Guid.NewGuid();
            var request = new DeleteGameRequest { Id = gameId };

            var gameRepoMock = new Mock<IGameRepository>();

            gameRepoMock.Setup(x => x.Delete(
                    It.Is<Expression<Func<Game, bool>>>(expr => TestPredicate(expr, gameId)),
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);

            var handler = new DeleteGameHandler(gameRepoMock.Object);

            // Act
            var result = await handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeTrue();

            gameRepoMock.Verify(x => x.Delete(
                It.IsAny<Expression<Func<Game, bool>>>(),
                It.IsAny<CancellationToken>()), Times.Once);
        }

        private bool TestPredicate(Expression<Func<Game, bool>> predicate, Guid expectedId)
        {
            var compiled = predicate.Compile();
            return compiled(new Game { Id = expectedId });
        }
    }
}
