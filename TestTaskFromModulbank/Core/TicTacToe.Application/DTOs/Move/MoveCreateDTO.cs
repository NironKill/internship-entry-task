namespace TicTacToe.Application.DTOs.Move
{
    public class MoveCreateDTO
    {
        public Guid GameId { get; set; }
        public string PlayerName { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
}
