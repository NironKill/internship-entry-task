using Microsoft.EntityFrameworkCore;
using TicTacToe.Application.DTOs.GamePlayer;
using TicTacToe.Application.Interfaces;
using TicTacToe.Application.Repositories.Interfaces;
using TicTacToe.Application.Services.Interfaces;

namespace TicTacToe.Application.Services.Implementations
{
    public class GamblingJokeService : IGamblingJokeService
    {
        private readonly IMoveRepository _move;
        private readonly IGamePlayerRepository _gamePlayer;

        public GamblingJokeService(IMoveRepository move, IGamePlayerRepository gamePlayer)
        {
            _move = move;
            _gamePlayer = gamePlayer;
        }

        public async Task<Guid> ChangeMove(Guid gameId, Guid playerId, CancellationToken cancellationToken)
        {
            int totalMoves = await _move.Count(gameId, cancellationToken) + 1;

            if (totalMoves % 3 == 0)
            {
                Random random = new Random();
                if (random.NextDouble() < 0.1)               
                    return await _gamePlayer.GetOpponentId(gameId, playerId, cancellationToken);         
            }
            return playerId;
        }
    }
}
