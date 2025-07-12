using MediatR;
using TicTacToe.Application.Repositories.Interfaces;

namespace TicTacToe.Application.Requests.Games.Write.Delete
{
    public class DeleteGameHandler : IRequestHandler<DeleteGameRequest, bool>
    {
        private readonly IGameRepository _game;
        
        public DeleteGameHandler(IGameRepository game) => _game = game;

        public async Task<bool> Handle(DeleteGameRequest request, CancellationToken cancellationToken) =>
            await _game.Delete(x => x.Id == request.Id, cancellationToken);
    }
}
