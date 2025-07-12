using TicTacToe.Application.DTOs.Move;

namespace TicTacToe.Application.Services.Interfaces
{
    public interface IBoardService
    {
        char[,] MoveMapVisualisation(List<BoardDTO> dto);
    }
}
