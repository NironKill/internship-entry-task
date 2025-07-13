using TicTacToe.Application.DTOs.Game;
using TicTacToe.Application.DTOs.Move;

namespace TicTacToe.Application.Services.Interfaces
{
    public interface IBoardService
    {
        string[][] MoveMapVisualisation(List<MoveGetDTO>? dto, int fieldSize);
        Task<GameGetDTO> GameСompletion(Guid moveId, CancellationToken cancellationToken);
    }
}
