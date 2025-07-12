using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using TicTacToe.Application.Common.Behaviors;
using TicTacToe.Application.Repositories.Implementations;
using TicTacToe.Application.Repositories.Interfaces;
using TicTacToe.Application.Services.Implementations;
using TicTacToe.Application.Services.Interfaces;

namespace TicTacToe.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies()));

            services.AddValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

            services.AddScoped<IGamePlayerRepository, GamePlayerRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IMoveRepository, MoveRepository>();

            services.AddScoped<IBoardService, BoardService>();

            return services;
        }
    }
}
