using TicTacToe.Domain.Base;

namespace TicTacToe.Domain.Entity
{
    public class GamePlayer : BaseEntity
    {
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }

        /// <summary>
        /// Is the role of the player:
        /// 0 - X;
        /// 1 - O;
        /// </summary>
        public int Role { get; set; }

        public Game Game { get; set; }
        public Player Player { get; set; }
    }
}
