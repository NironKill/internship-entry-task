using System.ComponentModel.DataAnnotations;
using TicTacToe.Domain.Base;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Domain.Entity
{
    public class Game : BaseEntity
    {
        public Guid? WinnerId { get; set; }

        [Range((int)GameRule.MinSize, (int)GameRule.MinSize)]
        public int FieldSize { get; set; }

        /// <summary>
        /// This is a condition for possible expansion of the game rules in the future. This version of the game supports only two players.
        /// </summary>
        [Range((int)GameRule.MinPlayers, (int)GameRule.MaxPlayers)]
        public int PlayerCount { get; set; } = 2;

        [Range((int)GameRule.MinVictoryCondition, (int)GameRule.MaxVictoryCondition)]
        public int VictoryCondition { get; set; }

        public DateTime StartAt { get; set; }

        public DateTime? EndAt { get; set; }

        /// <summary>
        /// This is the status state of the game:
        /// 0 - In Progress;
        /// 1 - Completed;
        /// 2 - Tie;
        /// </summary>
        public int Status { get; set; }

        public ICollection<Player> Players { get; set; }
        public ICollection<Move> Moves { get; set; }
        public ICollection<GamePlayer> GamePlayers { get; set; }
    }
}
