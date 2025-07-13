using MediatR;
using TicTacToe.Application.DTOs.Game;

namespace TicTacToe.Application.Requests.Moves.Write.Create
{
    public class CreateMoveRequest : IRequest<GameGetDTO>
    {
        public Guid GameId { get; set; }
        public string PlayerName { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
}
