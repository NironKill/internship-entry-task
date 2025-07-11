using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TicTacToe.Domain;

namespace TicTacToe.Persistence.EntityTypeConfigurations
{
    public class PlayerConfiguration : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.HasMany(p => p.Moves).WithOne(m => m.Player).HasForeignKey(m => m.PlayerId);

            builder.HasIndex(p => p.Name).IsUnique();
        }
    }
}
