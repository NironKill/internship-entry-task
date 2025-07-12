using MediatR;

namespace TicTacToe.Application.Requests.Move.Write.Create
{
    public class CreateMoveRequest : IRequest<Guid>
    {
        public Guid GameId { get; set; }
        public string PlayerName { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
}
