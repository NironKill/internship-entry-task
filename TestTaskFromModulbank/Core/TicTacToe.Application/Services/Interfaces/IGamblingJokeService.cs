namespace TicTacToe.Application.Services.Interfaces
{
    public interface IGamblingJokeService
    {
        Task<Guid> ChangeMove(Guid gameId, Guid playerId, CancellationToken cancellationToken);
    }
}
