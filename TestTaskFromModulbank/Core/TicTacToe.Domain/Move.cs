using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Domain
{
    public class Move
    {
        [Key]
        public Guid Id { get; set; }
        public Guid GameId { get; set; }
        public Guid PlayerId { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }

        public Game Game { get; set; }
        public Player Player { get; set; }
    }
}
