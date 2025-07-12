using MediatR;
using TicTacToe.Application.DTOs.Player;
using TicTacToe.Application.Repositories.Interfaces;

namespace TicTacToe.Application.Requests.Players.Read.GetAll
{
    public class GetAllPlayerHandler : IRequestHandler<GetAllPlayerRequest, ICollection<PlayerGetDTO>>
    {
        private readonly IPlayerRepository _player;

        public GetAllPlayerHandler(IPlayerRepository player) => _player = player;

        public async Task<ICollection<PlayerGetDTO>> Handle(GetAllPlayerRequest request, CancellationToken cancellationToken) =>
            await _player.GetAll(entity => new PlayerGetDTO
            {
                Id = entity.Id,
                Name = entity.Name
            }, cancellationToken);
    }
}
