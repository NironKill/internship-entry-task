using MediatR;
using TicTacToe.Application.DTOs.Game;
using TicTacToe.Application.DTOs.GamePlayer;
using TicTacToe.Application.DTOs.Move;
using TicTacToe.Application.DTOs.Player;
using TicTacToe.Application.Repositories.Interfaces;
using TicTacToe.Application.Services.Interfaces;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Application.Requests.Games.Read.GetAll
{
    public class GetAllGameHandler : IRequestHandler<GetAllGameRequest, ICollection<GameGetDTO>>
    {
        private readonly IGameRepository _game;
        private readonly IBoardService _board;
        private readonly IPlayerRepository _player;
        private readonly IGamePlayerRepository _gamePlayer;
        private readonly IMoveRepository _move;

        public GetAllGameHandler(IGameRepository game, IBoardService board, IPlayerRepository player, IGamePlayerRepository gamePlayer, IMoveRepository move)
        {
            _game = game;
            _board = board;
            _player = player;
            _gamePlayer = gamePlayer;
            _move = move;
        }

        public async Task<ICollection<GameGetDTO>> Handle(GetAllGameRequest request, CancellationToken cancellationToken) =>
            await _game.GetAll(entity => new GameGetDTO
            {
                Id = entity.Id,
                StartAt = entity.StartAt,
                EndAt = entity.EndAt,
                VictoryCondition = entity.VictoryCondition,
                FieldSize = entity.FieldSize,
                Status = ((StatusGame)entity.Status).ToString(),
                WinnerName = entity.WinnerId is not null
                ? _player.Get(x => x.Id == entity.WinnerId, player => new PlayerGetDTO
                {
                    Id = player.Id,
                    Name = player.Name
                }, cancellationToken).Result.Name
                : null,
                Players = _gamePlayer.GetAll(x => x.GameId == entity.Id, gp => new GamePlayerGetDTO
                {
                    Role = ((PlayerRole)gp.Role).ToString(),
                    PlayerName = _player.Get(x => x.Id == gp.PlayerId, p => new PlayerGetDTO 
                    { 
                        Id = p.Id,
                        Name= p.Name
                    }, cancellationToken).Result.Name
                }, cancellationToken).Result.ToList(),
                Moves = _board.MoveMapVisualisation(_move.GetAll(x => x.GameId == entity.Id, move => new MoveGetDTO
                {
                    Column = move.Column,
                    Row = move.Row,
                    PlayerRole = _gamePlayer.Get(x => x.PlayerId == move.PlayerId && x .GameId == move.GameId, gp => new GamePlayerGetDTO
                    {
                        Role = ((PlayerRole)gp.Role).ToString()
                    }, cancellationToken).Result.Role
                }, cancellationToken).Result.ToList(), entity.FieldSize)         
            }, cancellationToken);
    }
}
