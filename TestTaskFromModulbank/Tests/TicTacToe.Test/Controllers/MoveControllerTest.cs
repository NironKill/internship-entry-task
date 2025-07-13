using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TicTacToe.Application.DTOs.Game;
using TicTacToe.Application.DTOs.Move;
using TicTacToe.Application.Requests.Moves.Write.Create;
using TicTacToe.WebAPI.Controllers;

namespace TicTacToe.Test.Controllers
{
    public class MoveControllerTest
    {
        [Fact]
        public async Task Create_ShouldReturnCreated_WhenRequestIsValid()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();

            var dto = new MoveCreateDTO
            {
                Column = 1,
                Row = 2,
                PlayerName = "Player1",
                GameId = Guid.NewGuid()
            };

            var expectedResponse = new GameGetDTO
            {
                Id = dto.GameId,
                Status = "InProgress",
                FieldSize = 3,
                VictoryCondition = 3
            };

            mediatorMock
                .Setup(m => m.Send(It.IsAny<CreateMoveRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedResponse);

            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.Setup(sp => sp.GetService(typeof(IMediator)))
                           .Returns(mediatorMock.Object);

            var httpContext = new DefaultHttpContext
            {
                RequestServices = serviceProvider.Object
            };

            var controller = new MoveController
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = httpContext
                }
            };

            // Act
            var result = await controller.Create(dto);

            // Assert
            result.Should().BeOfType<CreatedResult>();
            var createdResult = result as CreatedResult;

            createdResult!.StatusCode.Should().Be(StatusCodes.Status201Created);
            createdResult.Value.Should().BeEquivalentTo(expectedResponse);

            mediatorMock.Verify(m => m.Send(It.Is<CreateMoveRequest>(r =>
                r.Column == dto.Column &&
                r.Row == dto.Row &&
                r.PlayerName == dto.PlayerName &&
                r.GameId == dto.GameId
            ), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
