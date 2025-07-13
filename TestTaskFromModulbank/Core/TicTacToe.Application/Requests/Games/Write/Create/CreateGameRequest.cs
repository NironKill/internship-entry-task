using MediatR;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Application.Requests.Games.Write.Create
{
    public class CreateGameRequest : IRequest<Guid>
    {
        public Dictionary<string, int> Players { get; set; }
        public int FieldSize { get; set; }
        public int VictoryCondition { get; set; }
        public DateTime StartAt { get; set; } = DateTime.UtcNow;
        public int Status { get; set; } = (int)StatusGame.InProgress;
    }
}
