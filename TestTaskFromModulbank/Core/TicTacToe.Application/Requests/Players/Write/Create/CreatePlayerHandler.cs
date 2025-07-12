using MediatR;
using TicTacToe.Application.Repositories.Interfaces;
using TicTacToe.Domain.Entity;

namespace TicTacToe.Application.Requests.Players.Write.Create
{
    public class CreatePlayerHandler : IRequestHandler<CreatePlayerRequest, Guid>
    {
        private readonly IPlayerRepository _player;

        public CreatePlayerHandler(IPlayerRepository player) => _player = player;

        public async Task<Guid> Handle(CreatePlayerRequest request, CancellationToken cancellationToken) =>
            await _player.Create(request, request => new Player
            {
                Id = Guid.NewGuid(),
                Name = request.Name
            }, cancellationToken);
    }
}
