namespace TicTacToe.Application.DTOs.GamePlayer
{
    public class GamePlayerCreateDTO
    {
        public Guid GameId { get; set; }
        public Dictionary<Guid, int> Players { get; set; }
    }
}
