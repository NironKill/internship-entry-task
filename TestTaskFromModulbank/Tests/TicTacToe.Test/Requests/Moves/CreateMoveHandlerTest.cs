using Moq;
using System.Linq.Expressions;
using TicTacToe.Application.DTOs.Game;
using TicTacToe.Application.DTOs.Player;
using TicTacToe.Application.Repositories.Interfaces;
using TicTacToe.Application.Requests.Moves.Write.Create;
using TicTacToe.Application.Services.Interfaces;
using TicTacToe.Domain.Entity;
using FluentAssertions;

namespace TicTacToe.Test.Requests.Moves
{
    public class CreateMoveHandlerTest
    {
        private readonly Mock<IMoveRepository> _moveMock;
        private readonly Mock<IPlayerRepository> _playerMock;
        private readonly Mock<IBoardService> _boardMock;
        private readonly CreateMoveHandler _handler;

        public CreateMoveHandlerTest()
        {
            _moveMock = new Mock<IMoveRepository>();
            _playerMock = new Mock<IPlayerRepository>();
            _boardMock = new Mock<IBoardService>();

            _handler = new CreateMoveHandler(_moveMock.Object, _playerMock.Object, _boardMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldCreateMoveAndReturnGameDto()
        {
            // Arrange
            var playerId = Guid.NewGuid();
            var moveId = Guid.NewGuid();
            var gameId = Guid.NewGuid();

            var request = new CreateMoveRequest
            {
                PlayerName = "Player1",
                Column = 1,
                Row = 2,
                GameId = gameId
            };

            _playerMock.Setup(x => x.Get(
                It.IsAny<Expression<Func<Player, bool>>>(),
                It.IsAny<Func<Player, PlayerGetDTO>>(),
                It.IsAny<CancellationToken>()))
                .Returns((Expression<Func<Player, bool>> predicate,
                          Expression<Func<Player, PlayerGetDTO>> selector,
                          CancellationToken _) =>
                {
                    var dto = selector.Compile().Invoke(new Player
                    {
                        Id = playerId,
                        Name = request.PlayerName
                    });
                    return Task.FromResult(dto);
                });

            _moveMock.Setup(x => x.Create(
                It.IsAny<CreateMoveRequest>(),
                It.IsAny<Func<CreateMoveRequest, Move>>(),
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(moveId);

            var expectedResponse = new GameGetDTO
            {
                Id = gameId,
                Status = "InProgress",
                FieldSize = 3,
                VictoryCondition = 3,
            };

            _boardMock.Setup(x => x.GameСompletion(moveId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(gameId);
            result.Status.Should().Be("InProgress");

            _moveMock.Verify(x => x.Create(It.IsAny<CreateMoveRequest>(),
                                           It.IsAny<Func<CreateMoveRequest, Move>>(),
                                           It.IsAny<CancellationToken>()), Times.Once);

            _boardMock.Verify(x => x.GameСompletion(moveId, It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
