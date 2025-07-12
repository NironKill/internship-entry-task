using MediatR;

namespace TicTacToe.Application.Requests.Games.Write.Delete
{
    public class DeleteGameRequest : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
