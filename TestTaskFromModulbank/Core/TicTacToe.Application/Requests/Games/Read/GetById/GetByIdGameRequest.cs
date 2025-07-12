using MediatR;
using TicTacToe.Application.DTOs.Game;

namespace TicTacToe.Application.Requests.Games.Read.GetById
{
    public class GetByIdGameRequest : IRequest<GameGetDTO>
    {
        public Guid Id { get; set; }
    }
}
