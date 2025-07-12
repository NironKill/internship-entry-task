using MediatR;
using TicTacToe.Application.DTOs.Player;
using TicTacToe.Application.Repositories.Interfaces;
using TicTacToe.Application.Requests.Players.Read.GetAll;

namespace TicTacToe.Application.Requests.Players.Read.GetById
{
    public class GetByIdPlayerHandler : IRequestHandler<GetByIdPlayerRequest, PlayerGetDTO>
    {
        private readonly IPlayerRepository _player;

        public GetByIdPlayerHandler(IPlayerRepository player) => _player = player;

        public async Task<PlayerGetDTO> Handle(GetByIdPlayerRequest request, CancellationToken cancellationToken) =>
            await _player.Get(x => x.Id == request.Id, entity => new PlayerGetDTO
            {
                Id = entity.Id,
                Name = entity.Name
            }, cancellationToken);
    }
}
