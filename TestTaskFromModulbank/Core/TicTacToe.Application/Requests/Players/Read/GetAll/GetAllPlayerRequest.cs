using MediatR;
using TicTacToe.Application.DTOs.Game;
using TicTacToe.Application.DTOs.Player;

namespace TicTacToe.Application.Requests.Players.Read.GetAll
{
    public class GetAllPlayerRequest : IRequest<ICollection<PlayerGetDTO>>
    {
    }
}
