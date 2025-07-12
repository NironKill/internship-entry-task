using TicTacToe.Application.DTOs.Player;
using TicTacToe.Application.Repositories.Abstract;
using TicTacToe.Application.Requests.Players.Write.Create;
using TicTacToe.Domain.Entity;

namespace TicTacToe.Application.Repositories.Interfaces
{
    public interface IPlayerRepository : IBaseRepository<Player, CreatePlayerRequest, PlayerGetDTO>
    {
    }
}
