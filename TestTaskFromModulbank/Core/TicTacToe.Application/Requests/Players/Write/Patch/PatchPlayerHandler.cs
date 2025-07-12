using MediatR;
using TicTacToe.Application.DTOs.Player;
using TicTacToe.Application.Repositories.Interfaces;

namespace TicTacToe.Application.Requests.Players.Write.Patch
{
    public class PatchPlayerHandler : IRequestHandler<PatchPlayerRequest, PlayerGetDTO>
    {
        private readonly IPlayerRepository _player;

        public PatchPlayerHandler(IPlayerRepository player) => _player = player;

        public async Task<PlayerGetDTO> Handle(PatchPlayerRequest request, CancellationToken cancellationToken) =>
            await _player.Update(x => x.Id == request.Id, (entity) =>
            {
                entity.Name = request.Name;
            }, map => new PlayerGetDTO
            {
                Id = request.Id,
                Name = request.Name,
            }, cancellationToken);
    }
}
