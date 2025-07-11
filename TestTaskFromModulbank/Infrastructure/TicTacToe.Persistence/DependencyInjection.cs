using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicTacToe.Application.Interfaces;
using TicTacToe.Persistence.Common;
using TicTacToe.Persistence.Settings;

namespace TicTacToe.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DataBaseSet>(configuration.GetSection(DataBaseSet.Configuration));
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(Connection.GetOptionConfiguration(
                configuration.GetSection(DataBaseSet.Configuration).Get<DataBaseSet>().ConnectionString)));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());

            return services;
        }
    }
}
