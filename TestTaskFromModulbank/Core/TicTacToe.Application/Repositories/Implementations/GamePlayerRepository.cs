using TicTacToe.Application.DTOs.GamePlayer;
using TicTacToe.Application.Interfaces;
using TicTacToe.Application.Repositories.Abstract;
using TicTacToe.Application.Repositories.Interfaces;
using TicTacToe.Domain.Entity;

namespace TicTacToe.Application.Repositories.Implementations
{
    public class GamePlayerRepository : BaseRepository<GamePlayer, GamePlayerCreateDTO, GamePlayerGetDTO>, IGamePlayerRepository
    {
        public GamePlayerRepository(IApplicationDbContext context) : base(context) { }
    }
}
