using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.Bot.BusinessComponent.Services;
using TicTacToe.Bot.BusinessComponent.Services.Implementation;

namespace TicTacToe.Bot.BusinessComponent.Extensions
{
    public static class BotBusinesComponentCollectionExtension
    {
        public static IServiceCollection AddBusinessLayerCollection(this IServiceCollection services)
        {
            services.AddHttpClient<IBotService, BotService>(client =>
            {
                //TODO make config address
                client.BaseAddress = new Uri("https://localhost:44366/");
            });
            return services;
        }
    }
}
