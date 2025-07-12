namespace TicTacToe.Application.DTOs.Player
{
    public class PlayerGetDTO
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Name { get; set; } = string.Empty;
    }
}
