namespace TicTacToe.Application.DTOs.Move
{
    public class MoveGetDTO
    {
        public Guid Id { get; set; } = Guid.Empty;
        public Guid GameId { get; set; } = Guid.Empty;
        public Guid PlayerId { get; set; } = Guid.Empty;
        public int Row { get; set; } = default(int);
        public int Column { get; set; } = default(int);
    }
}
