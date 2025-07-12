using MediatR;

namespace TicTacToe.Application.Requests.Players.Write.Delete
{
    public class DeletePlayerRequest : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
