using TicTacToe.Domain.Base;

namespace TicTacToe.Domain.Entity
{
    public class Player : BaseEntity
    {
        public string Name { get; set; }

        public ICollection<Game> Games { get; set; }
        public ICollection<Move> Moves { get; set; }
        public ICollection<GamePlayer> GamePlayers { get; set; }
    }
}
