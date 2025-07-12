using MediatR;
using TicTacToe.Application.DTOs.Player;
using TicTacToe.Application.Repositories.Interfaces;

namespace TicTacToe.Application.Requests.Move.Write.Create
{
    public class CreateMoveHandler : IRequestHandler<CreateMoveRequest, Guid>
    {
        private readonly IMoveRepository _move;
        private readonly IPlayerRepository _player;

        public CreateMoveHandler(IMoveRepository move, IPlayerRepository player)
        {
            _move = move;
            _player = player;
        }

        public async Task<Guid> Handle(CreateMoveRequest request, CancellationToken cancellationToken) =>
            await _move.Create(request, request => new Domain.Entity.Move
            {
                Id = Guid.NewGuid(),
                Column = request.Column,
                Row = request.Row,
                GameId = request.GameId,
                PlayerId = _player.Get(x => x.Name == request.PlayerName, entity => new PlayerGetDTO
                {
                    Id = entity.Id,
                    Name = entity.Name,
                }, cancellationToken).Result.Id
            }, cancellationToken);      
    }
}
