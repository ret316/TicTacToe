using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TicTacToe.BL.Config;
using TicTacToe.BL.Enum;
using TicTacToe.BL.Models;
using TicTacToe.BL.Services;
using TicTacToe.BL.Services.Implementation;
using TicTacToe.DL.Models;
using TicTacToe.DL.Services;
using Xunit;

namespace TicTacToe.Tests.UnitTests
{
    public class GameServiceTests
    {
        private static Guid Id = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");
        private static Guid GameId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc10");
        private static Guid PlayerId1 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc11");
        private static Guid PlayerId2 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc12");
        private static DateTime date = DateTime.Parse("2020-10-10");

        private IGameServiceBL GetGameService(Mock<IGameServiceDL> mock, Mock<IStatisticServiceBL> mock2 = null, Mock<IBotService> mock3 = null)
        {
            if (mock2 is null)
                mock2 = new Mock<IStatisticServiceBL>();
            if (mock3 is null)
                mock3 = new Mock<IBotService>();
            IFieldChecker _fieldChecker = new FIeldChecker();
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile()));
            IMapper _mapper = new Mapper(config);

            return new GameServiceBL(mock.Object, _fieldChecker, mock3.Object, mock2.Object, _mapper);
        }

        public class Colls
        {
            public GameBL gb0;
            public GameBL gb1;
            public GameDL gd0;
            public GameDL gd1;
            public GameHistoryBL ghb0;
            public GameHistoryBL ghb1;
            public GameHistoryDL ghd0;
            public GameHistoryDL ghd2;
            public GameResultBL grb0;
            public GameResultDL grd0;

            public IEnumerable<GameBL> gbh0;
            public IEnumerable<GameDL> gdh0;
            public IEnumerable<GameHistoryBL> gbe1;
            public IEnumerable<GameHistoryDL> gde1;
            public Colls()
            {
                gb0 = new GameBL
                {
                    Id = Id,
                    GameId = GameId,
                    Player1Id = PlayerId1,
                    Player2Id = PlayerId2,
                    IsPlayer2Bot = false,
                    IsGameFinished = false
                };
                gb1 = new GameBL
                {
                    Id = Id,
                    GameId = GameId,
                    Player1Id = PlayerId1,
                    Player2Id = null,
                    IsPlayer2Bot = true,
                    IsGameFinished = false
                };
                gd0 = new GameDL
                {
                    Id = Id,
                    GameId = GameId,
                    Player1Id = PlayerId1,
                    Player2Id = PlayerId2,
                    IsPlayer2Bot = false,
                    IsGameFinished = false
                };
                ghb0 = new GameHistoryBL
                {
                    GameId = GameId,
                    PlayerId = PlayerId1,
                    IsBot = false,
                    XAxis = 1,
                    YAxis = 1,
                    MoveDate = date
                };
                ghd0 = new GameHistoryDL
                {
                    Id = Id,
                    GameId = GameId,
                    PlayerId = PlayerId2,
                    IsBot = false,
                    XAxis = 2,
                    YAxis = 2,
                    MoveDate = date
                };
                ghd0 = new GameHistoryDL
                {
                    Id = Id,
                    GameId = GameId,
                    PlayerId = PlayerId2,
                    IsBot = false,
                    XAxis = 2,
                    YAxis = 2,
                    MoveDate = date
                };

                grb0 = new GameResultBL
                {
                    Id = Id,
                    GameId = GameId,
                    PlayerId = PlayerId1,
                    Result = BL.Models.ResultStatus.Won
                };
                grd0 = new GameResultDL
                {
                    Id = Id,
                    GameId = GameId,
                    PlayerId = PlayerId1,
                    Result = DL.Models.ResultStatus.Won
                };

                gbh0 = new List<GameBL>
                {
                    gb0
                };
                gdh0 = new List<GameDL>
                {
                    gd0
                };

                gbe1 = new List<GameHistoryBL>()
                {
                    ghb0
                };
                gde1 = new List<GameHistoryDL>()
                {
                    ghd0
                };
            }
        }

        [Fact]
        public async Task CreateGameTest()
        {
            var data = new Colls();

            var mock = new Mock<IGameServiceDL>();
            mock.Setup(cfg => cfg.CreateGameAsync(data.gd0)).Returns(Task.FromResult(true));
            
            var service = GetGameService(mock);
            var result = await service.CreateGameAsync(data.gb0);
            Assert.True(result);

            mock.Setup(cfg => cfg.CreateGameAsync(data.gd1)).Returns(Task.FromResult(false));

            service = GetGameService(mock);
            result = await service.CreateGameAsync(data.gb1);
            Assert.False(result);
        }

        [Fact]
        public async Task GetGameTest()
        {
            var data = new Colls();
            var mock = new Mock<IGameServiceDL>();
            mock.Setup(cfg => cfg.GetGameByGameIdAsync(Id)).Returns(Task.FromResult(data.gd0));

            var service = GetGameService(mock);
            var result = await service.GetGameByGameIdAsync(Id);

            Assert.Equal(data.gb0.Id, result.Id);

            mock.Setup(cfg => cfg.GetGamesByUserAsync(PlayerId1)).Returns(Task.FromResult(data.gdh0));

            service = GetGameService(mock);
            var result1 = await service.GetGamesByUserAsync(PlayerId1);

            Assert.Equal(data.gbh0.Select(x => x.GameId), result1.Select(x => x.GameId));
        }

        [Fact]
        public async Task SetGameFinishedTest()
        {
            var data = new Colls();
            var mock = new Mock<IGameServiceDL>();
            mock.Setup(cfg => cfg.GetGameByGameIdAsync(GameId)).Returns(Task.FromResult(data.gd0));
            mock.Setup(cfg => cfg.SetGameAsFinished(data.gd0)).Returns(Task.FromResult(default(object)));

            var service = GetGameService(mock);
            var result = await service.SetGameAsFinished(GameId);

            Assert.True(result);
        }


        [Fact]
        public async Task SaveMoveTest()
        {
            var data = new Colls();

            var mock1 = new Mock<IGameServiceDL>();
            var mock2 = new Mock<IStatisticServiceBL>();
            var mock3 = new Mock<IBotService>();

            mock1.Setup(cfg => cfg.GetGameHistoriesAsync(GameId)).Returns(Task.FromResult(data.gde1));
            mock1.Setup(cfg => cfg.GetGameByGameIdAsync(GameId)).Returns(Task.FromResult(data.gd0));
            mock1.Setup(cfg => cfg.SavePlayerMoveAsync(data.ghd0)).Returns(Task.FromResult(default(object)));
            mock2.Setup(cfg => cfg.SaveStatisticAsync(data.grb0)).Returns(Task.FromResult(true));

            var service = GetGameService(mock1, mock2, mock3);
            var result = await service.SavePlayerMoveAsync(data.ghb0);

            Assert.Equal(CheckStateBL.None, result);
        }
    }
}
