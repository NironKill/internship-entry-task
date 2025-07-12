using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Application.Interfaces;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Application.Requests.Players.Write.Patch
{
    public class PatchPlayerValidator : AbstractValidator<PatchPlayerRequest>
    {
        private readonly IApplicationDbContext _context;

        public PatchPlayerValidator(IApplicationDbContext context)
        {
            _context = context;
            RuleFor(request => request.Id).NotEmpty().WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.Id).MustAsync(PlayerByIdExists)
                .WithMessage("That player doesn't exist")
                .WithErrorCode($"{StatusCode.NotFound.GetHashCode()}");

            RuleFor(request => request.Name).NotEmpty().WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");
            RuleFor(request => request.Name).MustAsync(PlayerByNameExists)
                .WithMessage("The name's already taken")
                .WithErrorCode($"{StatusCode.NotFound.GetHashCode()}");
        }

        private async Task<bool> PlayerByNameExists(string name, CancellationToken cancellation) =>
           await _context.Players.AnyAsync(x => x.Name != name, cancellation);
        private async Task<bool> PlayerByIdExists(Guid id, CancellationToken cancellation) =>
           await _context.Players.AnyAsync(x => x.Id == id, cancellation);
    }
}
