using FluentValidation;
using Microsoft.EntityFrameworkCore;
using TicTacToe.Application.Interfaces;
using TicTacToe.Domain.Enums;

namespace TicTacToe.Application.Requests.Games.Write.Create
{
    public class CreateGameValidator : AbstractValidator<CreateGameRequest>
    {
        private readonly IApplicationDbContext _context;

        public CreateGameValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(request => request.FieldSize).NotEmpty().WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");
            RuleFor(request => request.FieldSize).InclusiveBetween((int)GameRule.MinSize, (int)GameRule.MaxSize)
                .WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.VictoryCondition).NotEmpty().WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");
            RuleFor(request => request.VictoryCondition).InclusiveBetween((int)GameRule.MinVictoryCondition, (int)GameRule.MaxVictoryCondition)
                .WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");
            RuleFor(request => request.VictoryCondition).Must((request, victoryCondition) => BoardOversizeCheck(request.FieldSize, victoryCondition))
                .WithMessage("The victory condition exceeds the size of the board.")
                .WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");

            RuleFor(request => request.Players).Must(players => players is not null && players.Keys.All(key => !string.IsNullOrEmpty(key)))
                .WithMessage("Player names cannot be empty.")
                .WithErrorCode($"{StatusCode.BadRequest.GetHashCode()}");
            RuleFor(request => request.Players).MustAsync(PlayerExists)
                .WithMessage("That player doesn't exist.")
                .WithErrorCode($"{StatusCode.NotFound.GetHashCode()}");
        }

        private bool BoardOversizeCheck(int fieldSize, int victoryCondition) => victoryCondition <= fieldSize;
        private async Task<bool> PlayerExists(Dictionary<string, int> players, CancellationToken cancellationToken) 
        { 
            foreach (KeyValuePair<string, int> player in players)
            {
                bool result = await _context.Players.AnyAsync(x => x.Name == player.Key, cancellationToken);

                if (!result)
                    return false;
            }
            return true;
        }
    }
}
