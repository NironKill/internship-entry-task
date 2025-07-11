using Microsoft.EntityFrameworkCore;

namespace TicTacToe.Persistence.Common
{
    public static class Preparation
    {
        public static async Task Initialize(ApplicationDbContext context) => await context.Database.MigrateAsync();
    }
}
