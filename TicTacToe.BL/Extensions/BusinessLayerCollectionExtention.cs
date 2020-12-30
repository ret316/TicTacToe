using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using TicTacToe.BL.Services;
using TicTacToe.BL.Services.Implementation;

namespace TicTacToe.BL.Extensions
{
    public static class BusinessLayerCollectionExtention
    {
        public static IServiceCollection AddBusinessLayerCollection(this IServiceCollection services, byte[] key)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = context =>
                        {
                            var userService = context.HttpContext.RequestServices.GetRequiredService<IUserServiceBL>();
                            var userId = Guid.Parse(context.Principal.Identity.Name);
                            var user = userService.GetUserAsync(userId).Result;
                            if (user == null)
                            {
                                // return unauthorized if user no longer exists
                                context.Fail("Unauthorized");
                            }
                            return Task.CompletedTask;
                        }
                    };
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            services.AddScoped<IUserServiceBL, UserServiceBL>();
            services.AddScoped<IGameServiceBL, GameServiceBL>();
            services.AddScoped<IFieldChecker, FIeldChecker>();
            services.AddScoped<IStatisticServiceBL, StatisticServiceBL>();
            services.AddScoped<IBotService, BotService>();
            return services;
        }
    }
}
