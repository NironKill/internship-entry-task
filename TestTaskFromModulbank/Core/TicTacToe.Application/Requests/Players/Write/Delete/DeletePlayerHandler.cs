using MediatR;
using TicTacToe.Application.Repositories.Interfaces;

namespace TicTacToe.Application.Requests.Players.Write.Delete
{
    public class DeletePlayerHandler : IRequestHandler<DeletePlayerRequest, bool>
    {
        private readonly IPlayerRepository _player;

        public DeletePlayerHandler(IPlayerRepository player) => _player = player;

        public async Task<bool> Handle(DeletePlayerRequest request, CancellationToken cancellationToken) =>
            await _player.Delete(x => x.Id == request.Id, cancellationToken);
    }
}
