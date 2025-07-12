using TicTacToe.Domain.Base;

namespace TicTacToe.Domain.Entity
{
    public class Move : BaseEntity
    {
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public Game Game { get; set; }
        public Player Player { get; set; }
    }
}
