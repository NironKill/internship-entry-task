using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Domain
{
    public class Game
    {
        [Key]
        public Guid Id { get; set; }
        public Guid? WinnerId { get; set; }
        public int Size { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public int Status { get; set; }

        public ICollection<Player> Players { get; set; }
        public ICollection<Move> Moves { get; set; }
        public ICollection<GamePlayer> GamePlayers { get; set; }
    }
}
