using TicTacToe.Application.DTOs.Move;
using TicTacToe.Application.Interfaces;
using TicTacToe.Application.Repositories.Abstract;
using TicTacToe.Application.Repositories.Interfaces;
using TicTacToe.Application.Requests.Moves.Write.Create;
using TicTacToe.Domain.Entity;

namespace TicTacToe.Application.Repositories.Implementations
{
    public class MoveRepository : BaseRepository<Move, CreateMoveRequest, MoveGetDTO>, IMoveRepository
    {
        public MoveRepository(IApplicationDbContext context) : base(context) { }
    }
}
