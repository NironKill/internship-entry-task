using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using TicTacToe.Application.DTOs.Game;
using TicTacToe.Application.Requests.Games.Read.GetAll;
using TicTacToe.WebAPI.Controllers;

namespace TicTacToe.Test.Controllers
{
    public class GameControllerTest
    {
        [Fact]
        public async Task GetAll_ShouldReturnOkWithListOfGames()
        {
            // Arrange
            var mediatorMock = new Mock<IMediator>();

            var expectedGames = new List<GameGetDTO>
        {
            new GameGetDTO
            {
                Id = Guid.NewGuid(),
                Status = "InProgress",
                FieldSize = 3,
                VictoryCondition = 3
            },
            new GameGetDTO
            {
                Id = Guid.NewGuid(),
                Status = "Completed",
                FieldSize = 4,
                VictoryCondition = 3
            }
        };

            mediatorMock
                .Setup(m => m.Send(It.IsAny<GetAllGameRequest>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(expectedGames);

            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.Setup(x => x.GetService(typeof(IMediator)))
                           .Returns(mediatorMock.Object);

            var httpContext = new DefaultHttpContext
            {
                RequestServices = serviceProvider.Object
            };

            var controller = new GameController
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = httpContext
                }
            };

            // Act
            IActionResult result = await controller.GetAll();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            var okResult = result as OkObjectResult;
            okResult!.Value.Should().BeEquivalentTo(expectedGames);

            mediatorMock.Verify(m => m.Send(It.IsAny<GetAllGameRequest>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
