using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Serilog;
using TicTacToe.DL.Config;
using TicTacToe.DL.Models;
using Xunit;

namespace TicTacToe.Tests.IntegrationTests
{
    public class GameTests
    {

        private static Guid Id = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");
        private static Guid GameId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc10");
        private static Guid PlayerId1 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc11");
        private static Guid PlayerId2 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc12");
        private static DateTime date = DateTime.Parse("2020-10-10");

        public TestServer server;
        public string conString;

        private HttpClient GetConfiguration()
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("test_appsettings.json", true, true)
                .AddEnvironmentVariables().Build();

            conString = builder.GetConnectionString("DbConString");

            server ??= new TestServer(new WebHostBuilder()
                .UseEnvironment("Debug")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseConfiguration(builder).UseStartup<Startup>()
                .UseSerilog((hostingContext, loggerConfiguration) => {
                    loggerConfiguration
                        .ReadFrom.Configuration(hostingContext.Configuration)
                        .Enrich.FromLogContext()
                        .Enrich.WithProperty("ApplicationName", typeof(Program).Assembly.GetName().Name)
                        .Enrich.WithProperty("Environment", hostingContext.HostingEnvironment);
                    loggerConfiguration.Enrich.WithProperty("DebuggerAttached", Debugger.IsAttached);
                }));

            return server.CreateClient();
        }

        private DataBaseContext ContextBuiler()
        {
            var optionsBuiler = new DbContextOptionsBuilder<DataBaseContext>();
            var options = optionsBuiler.UseNpgsql(conString).Options;
            return new DataBaseContext(options);
        }

        private async Task DBInit(Colls data)
        {
            using (var db = ContextBuiler())
            {
                await db.Games.AddAsync(data.gd0);

                await db.SaveChangesAsync();
            }
        }

        public class Colls
        {
            public GameDL gd0;
            public IEnumerable<GameDL> gde0;
            public Colls()
            {
                gd0 = new GameDL
                {
                    GameId = GameId,
                    Player1Id = PlayerId1,
                    Player2Id = PlayerId2,
                    IsPlayer2Bot = false,
                    IsGameFinished = false
                };
                gde0 = new List<GameDL>
                {
                    gd0
                };
            }
        }

        [Fact]
        public async Task CreateGames()
        {
            var client = GetConfiguration();
            var data = new Colls();
            //await DBInit(data);

            var json = JsonConvert.SerializeObject(data.gd0);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var result = await client.PostAsync("api/games/create", content);

            using (var db = ContextBuiler())
            {
                var game = await db.Games.FirstOrDefaultAsync(x =>
                    x.Player1Id == data.gd0.Player1Id && x.Player2Id == data.gd0.Player2Id);

                Assert.NotNull(game);

                db.Games.Remove(game);

                await db.SaveChangesAsync();
            }
        }

        [Fact]
        public async Task GetGames()
        {
            var client = GetConfiguration();
            var data = new Colls();
            await DBInit(data);

            var result = await client.GetAsync("api/games/");

            var json = await result.Content.ReadAsStringAsync();
            var res1 = JsonConvert.DeserializeObject<IEnumerable<GameDL>>(json);

            Assert.Equal(data.gde0.Select(s => s.GameId), res1.Select(s => s.GameId));

            using (var db = ContextBuiler())
            {
                var d = await db.Games.ToListAsync();
                db.Games.RemoveRange(d);
                await db.SaveChangesAsync();
            }
        }
    }
}
