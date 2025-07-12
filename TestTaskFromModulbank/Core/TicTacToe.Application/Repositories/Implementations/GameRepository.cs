using TicTacToe.Application.DTOs.Game;
using TicTacToe.Application.Interfaces;
using TicTacToe.Application.Repositories.Abstract;
using TicTacToe.Application.Repositories.Interfaces;
using TicTacToe.Application.Requests.Games.Write.Create;
using TicTacToe.Domain.Entity;

namespace TicTacToe.Application.Repositories.Implementations
{
    public class GameRepository : BaseRepository<Game, CreateGameRequest, GameGetDTO>, IGameRepository
    {
        public GameRepository(IApplicationDbContext context) : base(context) { }
    }
}
