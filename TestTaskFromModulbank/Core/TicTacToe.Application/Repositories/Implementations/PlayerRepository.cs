using TicTacToe.Application.DTOs.Player;
using TicTacToe.Application.Interfaces;
using TicTacToe.Application.Repositories.Abstract;
using TicTacToe.Application.Repositories.Interfaces;
using TicTacToe.Application.Requests.Players.Write.Create;
using TicTacToe.Domain.Entity;

namespace TicTacToe.Application.Repositories.Implementations
{
    public class PlayerRepository : BaseRepository<Player, CreatePlayerRequest, PlayerGetDTO>, IPlayerRepository
    {
        public PlayerRepository(IApplicationDbContext context) : base(context) { }
    }
}
