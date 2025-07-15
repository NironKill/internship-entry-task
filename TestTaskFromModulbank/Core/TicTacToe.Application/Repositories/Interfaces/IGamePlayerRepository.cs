using TicTacToe.Application.DTOs.GamePlayer;
using TicTacToe.Application.Repositories.Abstract;
using TicTacToe.Domain.Entity;

namespace TicTacToe.Application.Repositories.Interfaces
{
    public interface IGamePlayerRepository : IBaseRepository<GamePlayer, GamePlayerCreateDTO, GamePlayerGetDTO>
    {
        Task<Guid> GetOpponentId(Guid gameId, Guid playerId, CancellationToken cancellationToken);
    }
}
