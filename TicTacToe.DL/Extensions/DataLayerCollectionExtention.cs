using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicTacToe.DataComponent.Config;
using TicTacToe.DataComponent.Services;
using TicTacToe.DataComponent.Services.Implementation;

namespace TicTacToe.DataComponent.Extensions
{
    public static class DataLayerCollectionExtention
    {
        public static IServiceCollection AddDataLayerCollection(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DataBaseContext>(options =>
            {
                options.UseNpgsql(connectionString);
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            });
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IStatisticService, StatisticService>();
            return services;
        }
    }
}
