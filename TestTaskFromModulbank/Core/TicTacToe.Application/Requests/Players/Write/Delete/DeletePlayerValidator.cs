using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Application.Interfaces;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Application.Requests.Players.Write.Delete
{
    public class DeletePlayerValidator : AbstractValidator<DeletePlayerRequest>
    {
        private readonly IApplicationDbContext _context;

        public DeletePlayerValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(request => request.Id).NotEmpty().WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.Id).MustAsync(PlayerExists)
                .WithMessage("That player doesn't exist")
                .WithErrorCode($"{StatusCode.NotFound.GetHashCode()}");
        }

        private async Task<bool> PlayerExists(Guid id, CancellationToken cancellation) =>
           await _context.Players.AnyAsync(x => x.Id == id, cancellation);
    }
}
