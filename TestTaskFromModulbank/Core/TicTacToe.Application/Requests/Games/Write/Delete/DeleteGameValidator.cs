using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Application.Interfaces;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Application.Requests.Games.Write.Delete
{
    public class DeleteGameValidator : AbstractValidator<DeleteGameRequest>
    {
        private readonly IApplicationDbContext _context;

        public DeleteGameValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(request => request.Id).NotEmpty().WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.Id).MustAsync(GameExists)
                .WithMessage("Such a game doesn't exist")
                .WithErrorCode($"{StatusCode.NotFound.GetHashCode()}");
        }

        private async Task<bool> GameExists(Guid id, CancellationToken cancellation) =>
            await _context.Games.AnyAsync(x => x.Id == id, cancellation);
    }
}
