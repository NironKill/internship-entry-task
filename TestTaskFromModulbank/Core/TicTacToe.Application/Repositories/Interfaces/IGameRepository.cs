using TicTacToe.Application.DTOs.Game;
using TicTacToe.Application.Repositories.Abstract;
using TicTacToe.Application.Requests.Games.Write.Create;
using TicTacToe.Domain.Entity;

namespace TicTacToe.Application.Repositories.Interfaces
{
    public interface IGameRepository : IBaseRepository<Game, CreateGameRequest, GameGetDTO>
    {
    }
}
