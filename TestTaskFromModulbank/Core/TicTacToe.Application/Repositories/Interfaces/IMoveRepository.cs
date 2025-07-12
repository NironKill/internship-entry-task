using TicTacToe.Application.DTOs.Move;
using TicTacToe.Application.Repositories.Abstract;
using TicTacToe.Application.Requests.Move.Write.Create;
using TicTacToe.Domain.Entity;

namespace TicTacToe.Application.Repositories.Interfaces
{
    public interface IMoveRepository : IBaseRepository<Move, CreateMoveRequest, MoveGetDTO>
    {
    }
}
