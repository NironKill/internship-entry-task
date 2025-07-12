using MediatR;
using TicTacToe.Application.DTOs.Game;

namespace TicTacToe.Application.Requests.Games.Read.GetAll
{
    public class GetAllGameRequest : IRequest<ICollection<GameGetDTO>>
    {
    }
}
