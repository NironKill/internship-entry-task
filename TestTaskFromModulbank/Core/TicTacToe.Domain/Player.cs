using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Domain
{
    public class Player
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ICollection<Game> Games { get; set; }
        public ICollection<Move> Moves { get; set; }
        public ICollection<GamePlayer> GamePlayers { get; set; }
    }
}
