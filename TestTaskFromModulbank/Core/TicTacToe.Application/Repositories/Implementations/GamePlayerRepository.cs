using Microsoft.EntityFrameworkCore;
using TicTacToe.Application.DTOs.GamePlayer;
using TicTacToe.Application.Interfaces;
using TicTacToe.Application.Repositories.Abstract;
using TicTacToe.Application.Repositories.Interfaces;
using TicTacToe.Domain.Entity;

namespace TicTacToe.Application.Repositories.Implementations
{
    public class GamePlayerRepository : BaseRepository<GamePlayer, GamePlayerCreateDTO, GamePlayerGetDTO>, IGamePlayerRepository
    {
        private readonly new IApplicationDbContext _context;

        public GamePlayerRepository(IApplicationDbContext context) : base(context) 
        {
            _context = context;
        }

        public async Task<Guid> GetOpponentId(Guid gameId, Guid playerId, CancellationToken cancellationToken) =>
            await _context.GamePlayers.Where(x => x.GameId == gameId && x.PlayerId != playerId).Select(x => x.PlayerId).FirstOrDefaultAsync();
    }
}
