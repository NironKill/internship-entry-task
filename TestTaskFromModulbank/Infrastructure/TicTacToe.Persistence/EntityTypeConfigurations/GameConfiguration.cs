using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicTacToe.Domain.Entity;

namespace TicTacToe.Persistence.EntityTypeConfigurations
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.HasMany(g => g.Moves).WithOne(m => m.Game).HasForeignKey(m => m.GameId);

            builder.HasMany(g => g.Players).WithMany(p => p.Games).UsingEntity<GamePlayer>(
                x => x.HasOne<Player>(gp => gp.Player).WithMany(p => p.GamePlayers).HasForeignKey(gp => gp.PlayerId),
                x => x.HasOne<Game>(gp => gp.Game).WithMany(g => g.GamePlayers).HasForeignKey(gp => gp.GameId));
        }
    }
}
