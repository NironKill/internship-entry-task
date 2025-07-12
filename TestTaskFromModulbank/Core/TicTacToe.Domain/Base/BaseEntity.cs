using System.ComponentModel.DataAnnotations;

namespace TicTacToe.Domain.Base
{
    public abstract class BaseEntity
    {
        [Key]
        public virtual Guid Id { get; set; }
    }
}
