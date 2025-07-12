using TicTacToe.Application.DTOs.Move;
using TicTacToe.Application.Services.Interfaces;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Application.Services.Implementations
{
    public class BoardService : IBoardService
    {
        public char[,] MoveMapVisualisation(List<BoardDTO> dto)
        {
            int size = dto.Select(x => x.FieldSize).First();
            char[,] board = new char[size, size];

            for (int i = 0; i < size; i++)
                for (int j = 0; j < size; j++)
                    board[i, j] = '_';

            foreach (BoardDTO move in dto)
            {
                board[move.Row, move.Column] = move.PlayerRole switch
                {
                    (int)PlayerRole.X => 'X',
                    (int)PlayerRole.O => 'O',
                    _ => throw new InvalidOperationException($"Unknown player role: {move.PlayerRole}")
                };
            }
            return board;
        }
    }
}
