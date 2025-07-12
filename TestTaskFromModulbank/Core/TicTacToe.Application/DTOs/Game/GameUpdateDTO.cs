namespace TicTacToe.Application.DTOs.Game
{
    public class GameUpdateDTO
    {
        public Guid? WinnerId { get; set; }
        public DateTime? EndAt { get; set; }
        public int Status { get; set; }
    }
}
