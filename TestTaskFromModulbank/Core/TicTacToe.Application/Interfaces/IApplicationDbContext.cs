using Microsoft.EntityFrameworkCore;
using TicTacToe.Domain.Entity;

namespace TicTacToe.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Game> Games { get; set; }
        DbSet<Player> Players { get; set; }
        DbSet<Move> Moves { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
