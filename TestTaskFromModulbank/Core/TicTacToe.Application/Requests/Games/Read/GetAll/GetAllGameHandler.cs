using MediatR;
using TicTacToe.Application.DTOs.Game;
using TicTacToe.Application.DTOs.GamePlayer;
using TicTacToe.Application.DTOs.Move;
using TicTacToe.Application.Repositories.Interfaces;
using TicTacToe.Application.Services.Interfaces;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Application.Requests.Games.Read.GetAll
{
    public class GetAllGameHandler : IRequestHandler<GetAllGameRequest, ICollection<GameGetDTO>>
    {
        private readonly IGameRepository _game;
        private readonly IBoardService _move;

        public GetAllGameHandler(IGameRepository game, IBoardService move)
        {
            _game = game;
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
                WinnerName = entity.Players?.Where(x => x.Id == entity.WinnerId).Select(x => x.Name).FirstOrDefault(),
                Players = entity.GamePlayers.Select(gp => new GamePlayerGetDTO
                {
                    Role = ((PlayerRole)gp.Role).ToString(),
                    PlayerName = gp.Player.Name
                }).ToList(),
                Moves = _move.MoveMapVisualisation(entity.Moves.Where(x => x.GameId == entity.Id).Select(m => new BoardDTO
                {
                    FieldSize = entity.FieldSize,
                    PlayerRole = entity.GamePlayers.Where(x => x.PlayerId == m.PlayerId && x.GameId == m.GameId).Select(x => x.Role).FirstOrDefault(),
                    Column = m.Column,
                    Row = m.Row,
                }).ToList())
            }, cancellationToken);       
    }
}
