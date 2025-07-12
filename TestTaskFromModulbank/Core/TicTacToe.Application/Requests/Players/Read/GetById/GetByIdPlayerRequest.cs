using MediatR;
using TicTacToe.Application.DTOs.Player;

namespace TicTacToe.Application.Requests.Players.Read.GetById
{
    public class GetByIdPlayerRequest : IRequest<PlayerGetDTO>
    {
        public Guid Id { get; set; }
    }
}
