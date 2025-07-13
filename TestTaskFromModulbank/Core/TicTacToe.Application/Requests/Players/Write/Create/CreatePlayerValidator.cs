using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Application.Interfaces;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Application.Requests.Players.Write.Create
{
    public class CreatePlayerValidator : AbstractValidator<CreatePlayerRequest>
    {
        private readonly IApplicationDbContext _context;

        public CreatePlayerValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(request => request.Name).NotEmpty().WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.Name).MustAsync(PlayerExists)
                .WithMessage("The name's already taken")
                .WithErrorCode($"{StatusCode.NotFound.GetHashCode()}");
        }

        private async Task<bool> PlayerExists(string name, CancellationToken cancellation) =>
           !await _context.Players.AnyAsync(x => x.Name == name, cancellation);
    }
}
