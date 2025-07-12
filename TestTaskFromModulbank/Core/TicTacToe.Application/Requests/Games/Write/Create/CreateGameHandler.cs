using MediatR;
using TicTacToe.Application.DTOs.GamePlayer;
using TicTacToe.Application.DTOs.Player;
using TicTacToe.Application.Repositories.Interfaces;
using TicTacToe.Domain.Entity;

namespace TicTacToe.Application.Requests.Games.Write.Create
{
    public class CreateGameHandler : IRequestHandler<CreateGameRequest, Guid>
    {
        private readonly IGameRepository _game;
        private readonly IGamePlayerRepository _gamePlayer;
        private readonly IPlayerRepository _player;

        public CreateGameHandler(IGameRepository game, IGamePlayerRepository gamePlayer, IPlayerRepository player)
        {
            _game = game;
            _gamePlayer = gamePlayer;
            _player = player;
        }

        public async Task<Guid> Handle(CreateGameRequest request, CancellationToken cancellationToken)
        {
            Guid gameId = await _game.Create(request, request => new Game
            {
                Id = Guid.NewGuid(),
                StartAt = request.StartAt,
                FieldSize = request.FieldSize,
                VictoryCondition = request.VictoryCondition,
                Status = request.Status
            }, cancellationToken);

            Dictionary<Guid, int> players = new Dictionary<Guid, int>();
            foreach(KeyValuePair<string, int> player in request.Players)
            {
                PlayerGetDTO dto = await _player.Get(x => x.Name == player.Key, dto => new PlayerGetDTO
                {
                    Id = dto.Id,
                }, cancellationToken);
                players.Add(dto.Id, player.Value);
            }

            GamePlayerCreateDTO playerCreateDTO = new GamePlayerCreateDTO()
            {
                GameId = gameId,
                Players = players
            };

            foreach (KeyValuePair<Guid, int> dto in playerCreateDTO.Players)
            {
                await _gamePlayer.Create(playerCreateDTO, playerCreateDTO => new GamePlayer
                {
                    Id = Guid.NewGuid(),
                    GameId = playerCreateDTO.GameId,
                    PlayerId = dto.Key,
                    Role = dto.Value
                }, cancellationToken);
            }

            return gameId;
        }
    }
}
