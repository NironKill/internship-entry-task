using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Domain
{
    public class GamePlayer
    {
        [Key]
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }

        public Game Game { get; set; }
        public Player Player { get; set; }
    }
}
