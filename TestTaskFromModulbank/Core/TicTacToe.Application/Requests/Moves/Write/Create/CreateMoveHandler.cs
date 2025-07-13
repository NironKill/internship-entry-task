using MediatR;
using TicTacToe.Application.DTOs.Game;
using TicTacToe.Application.DTOs.Player;
using TicTacToe.Application.Repositories.Interfaces;
using TicTacToe.Application.Services.Interfaces;
using TicTacToe.Domain.Entity;

namespace TicTacToe.Application.Requests.Moves.Write.Create
{
    public class CreateMoveHandler : IRequestHandler<CreateMoveRequest, GameGetDTO>
    {
        private readonly IMoveRepository _move;
        private readonly IPlayerRepository _player;
        private readonly IBoardService _board;

        public CreateMoveHandler(IMoveRepository move, IPlayerRepository player, IBoardService board)
        {
            _move = move;
            _player = player;
            _board = board;
        }

        public async Task<GameGetDTO> Handle(CreateMoveRequest request, CancellationToken cancellationToken)
        {
            Guid moveId =  await _move.Create(request, request => new Move
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

            GameGetDTO response = await _board.GameСompletion(moveId, cancellationToken);

            return response;
        }
    }
}
