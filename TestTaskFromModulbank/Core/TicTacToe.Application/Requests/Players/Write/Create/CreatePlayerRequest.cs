using MediatR;

namespace TicTacToe.Application.Requests.Players.Write.Create
{
    public class CreatePlayerRequest : IRequest<Guid>
    {
        public string Name { get; set; }
    }
}
