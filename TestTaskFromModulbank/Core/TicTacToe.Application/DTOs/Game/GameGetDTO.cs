using TicTacToe.Application.DTOs.GamePlayer;

namespace TicTacToe.Application.DTOs.Game
{
    public class GameGetDTO
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string? WinnerName { get; set; } = string.Empty;
        public int FieldSize { get; set; }
        public int VictoryCondition { get; set; }
        public DateTime StartAt { get; set; } 
        public DateTime? EndAt { get; set; }
        public string Status { get; set; }

        public List<GamePlayerGetDTO> Players { get; set; }
        public string[][] Moves { get; set; } 
    }
}
