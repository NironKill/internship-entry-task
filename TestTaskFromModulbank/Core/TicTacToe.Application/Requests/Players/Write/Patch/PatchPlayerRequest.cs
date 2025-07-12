using MediatR;
using TicTacToe.Application.DTOs.Player;

namespace TicTacToe.Application.Requests.Players.Write.Patch
{
    public class PatchPlayerRequest : IRequest<PlayerGetDTO>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
