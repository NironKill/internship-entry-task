using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Application.Interfaces;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Application.Requests.Move.Write.Create
{
    public class CreateMoveValidator : AbstractValidator<CreateMoveRequest>
    {
        private readonly IApplicationDbContext _context;

        public CreateMoveValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(request => request.GameId).NotEmpty().WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");
            RuleFor(request => request.GameId).MustAsync(GameExists)
                .WithMessage("Such a game doesn't exist")
                .WithErrorCode($"{StatusCode.NotFound.GetHashCode()}");

            RuleFor(request => request.Column).NotEmpty().WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");
            RuleFor(request => request.Row).NotEmpty().WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");

            RuleFor(request => request).MustAsync(FieldCheck)
                .WithMessage("This field is already taken.")
                .WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.PlayerName).NotEmpty().WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");
            RuleFor(request => request.PlayerName).MustAsync(PlayerExists)
                .WithMessage("That player doesn't exist.")
                .WithErrorCode($"{StatusCode.NotFound.GetHashCode()}");
        }

        private async Task<bool> FieldCheck(CreateMoveRequest request, CancellationToken cancellationToken) =>
            await _context.Moves.Where(x => x.GameId == request.GameId).AnyAsync(x => x.Column != request.Column && x.Row != request.Row, cancellationToken);
        private async Task<bool> GameExists(Guid id, CancellationToken cancellationToken) =>
            await _context.Games.AnyAsync(x => x.Id == id, cancellationToken);
        private async Task<bool> PlayerExists(string name, CancellationToken cancellationToken) =>
            await _context.Players.AnyAsync(x => x.Name == name, cancellationToken);
    }
}
