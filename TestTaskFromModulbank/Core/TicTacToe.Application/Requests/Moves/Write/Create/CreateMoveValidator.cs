using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Application.Interfaces;
using TicTacToe.Domain.Entity;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Application.Requests.Moves.Write.Create
{
    public class CreateMoveValidator : AbstractValidator<CreateMoveRequest>
    {
        private readonly IApplicationDbContext _context;

        public CreateMoveValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(request => request.GameId).NotEmpty().WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");
            RuleFor(request => request.GameId).MustAsync(GameExists)
                .WithMessage("Such a game doesn't exist.")
                .WithErrorCode($"{StatusCode.NotFound.GetHashCode()}");

            RuleFor(request => request).MustAsync(RunQueueChecks)
                .WithMessage("It's not your move right now.")
                .WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.GameId).MustAsync(StatusCheck)
                .WithMessage("It's a complete game.")
                .WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.Column).GreaterThan(-1).WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");
            RuleFor(request => request.Row).GreaterThan(-1).WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");

            RuleFor(request => request).MustAsync(FieldCheck)
                .WithMessage("This field is already taken.")
                .WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");

            RuleFor(request => request).MustAsync(SizeCheck)
                .WithMessage("You've gone beyond the size of the board.")
                .WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.PlayerName).NotEmpty().WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");

            RuleFor(request => request).MustAsync(PlayerExists)
                .WithMessage("That player doesn't exist.")
                .WithErrorCode($"{StatusCode.NotFound.GetHashCode()}");
        }

        private async Task<bool> RunQueueChecks(CreateMoveRequest request, CancellationToken cancellationToken)
        {
            GamePlayer gamePlayer = await _context.GamePlayers
                .Include(gp => gp.Player)
                .FirstOrDefaultAsync(x =>
                x.GameId == request.GameId &&
                x.Player.Name == request.PlayerName,
                cancellationToken);

            if (gamePlayer is null)
                return false;

            int totalMoves = await _context.Moves
                .CountAsync(m => m.GameId == request.GameId, cancellationToken);

            int expectedRole = totalMoves % 2 == 0 ? 0 : 1;

            return gamePlayer.Role == expectedRole;
        }
        private async Task<bool> StatusCheck(Guid gameId, CancellationToken cancellationToken) =>
           await _context.Games.Where(x => x.Id == gameId).AnyAsync(x => x.Status == (int)StatusGame.InProgress);
        private async Task<bool> SizeCheck(CreateMoveRequest request, CancellationToken cancellationToken) =>
            await _context.Games.AnyAsync(x => x.FieldSize > request.Row && x.FieldSize > request.Column);
        private async Task<bool> FieldCheck(CreateMoveRequest request, CancellationToken cancellationToken) =>
            !await _context.Moves.Where(x => x.GameId == request.GameId).AnyAsync(x => x.Column == request.Column && x.Row == request.Row, cancellationToken);
        private async Task<bool> GameExists(Guid id, CancellationToken cancellationToken) =>
            await _context.Games.AnyAsync(x => x.Id == id, cancellationToken);
        private async Task<bool> PlayerExists(CreateMoveRequest request, CancellationToken cancellationToken) =>
            await _context.GamePlayers.AnyAsync(x => x.GameId == request.GameId && x.Player.Name == request.PlayerName, cancellationToken);
    }
}
