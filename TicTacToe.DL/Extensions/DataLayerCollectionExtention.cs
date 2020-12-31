using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicTacToe.DL.Config;
using TicTacToe.DL.Services;
using TicTacToe.DL.Services.Implementation;

namespace TicTacToe.DL.Extensions
{
    public static class DataLayerCollectionExtention
    {
        public static IServiceCollection AddDataLayerCollection(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<DataBaseContext>(options =>
            {
                options.UseNpgsql(connectionString);
                options.EnableSensitiveDataLogging();
            });
            services.AddScoped<IUserServiceDL, UserServiceDL>();
            services.AddScoped<IGameServiceDL, GameServiceDL>();
            services.AddScoped<IStatisticServiceDL, StatisticServiceDL>();
            return services;
        }
    }
}
