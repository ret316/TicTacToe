using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.BL.Services;
using TicTacToe.BL.Services.Implementation;

namespace TicTacToe.BL.Extensions
{
    public static class BusinessLayerCollectionExtention
    {
        public static IServiceCollection AddBusinessLayerCollection(this IServiceCollection services)
        {
            services.AddScoped<IUserServiceBL, UserServiceBL>();
            return services;
        }
    }
}
