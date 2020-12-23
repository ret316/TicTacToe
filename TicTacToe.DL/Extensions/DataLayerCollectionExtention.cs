using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using TicTacToe.DL.Services;
using TicTacToe.DL.Services.Implementation;

namespace TicTacToe.DL.Extensions
{
    public static class DataLayerCollectionExtention
    {
        public static IServiceCollection AddDataLayerCollection(this IServiceCollection services)
        {
            services.AddScoped<IUserServiceDL, UserServiceDL>();
            return services;
        }
    }
}
