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
using TicTacToe.Tests.TestData.Game;
using Xunit;

namespace TicTacToe.Tests.UnitTests
{
    public class GameServiceTests
    {

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

        [Theory]
        [ClassData(typeof(GameTestData1))]
        public async Task Test1_CreateGame(GameBL gb0, GameDL gd0, bool tRes)
        {
            var mock = new Mock<IGameServiceDL>();
            mock.Setup(cfg => cfg.CreateGameAsync(gd0)).Returns(Task.FromResult(tRes));
            
            var service = GetGameService(mock);
            var result = await service.CreateGameAsync(gb0);
            Assert.True(result);
        }

        [Theory]
        [ClassData(typeof(GameTestData2))]
        public async Task Test2_GetGame(Guid id, Guid playerId1, GameDL gd0, IEnumerable<GameDL> gdh0, IEnumerable<GameBL> gbh0)
        {
            var mock = new Mock<IGameServiceDL>();
            mock.Setup(cfg => cfg.GetGameByGameIdAsync(id)).Returns(Task.FromResult(gd0));

            var service = GetGameService(mock);
            var result = await service.GetGameByGameIdAsync(id);

            Assert.Equal(id, result.Id);

            mock.Setup(cfg => cfg.GetGamesByUserAsync(playerId1)).Returns(Task.FromResult(gdh0));

            service = GetGameService(mock);
            var result1 = await service.GetGamesByUserAsync(playerId1);

            Assert.Equal(gbh0.Select(x => x.GameId), result1.Select(x => x.GameId));

            mock.Setup(cfg => cfg.GetAllGamesAsync()).Returns(Task.FromResult(gdh0));
            service = GetGameService(mock);

            var result2 = await service.GetAllGamesAsync();

            Assert.Equal(gbh0.Select(x => x.GameId), result2.Select(x => x.GameId));
        }

        [Theory]
        [ClassData(typeof(GameTestData3))]
        public async Task Test3_SetGameFinished(Guid gameId, GameDL gd0)
        {
            var mock = new Mock<IGameServiceDL>();
            mock.Setup(cfg => cfg.GetGameByGameIdAsync(gameId)).Returns(Task.FromResult(gd0));
            mock.Setup(cfg => cfg.SetGameAsFinished(gd0)).Returns(Task.FromResult(default(object)));

            var service = GetGameService(mock);
            var result = await service.SetGameAsFinished(gameId);

            Assert.True(result);
        }


        [Theory]
        [ClassData(typeof(GameTestData4))]
        public async Task Test4_SaveMove(Guid gameId, GameDL gd0, IEnumerable<GameHistoryDL> gde1, GameHistoryDL ghd0, GameHistoryBL ghb0, GameResultBL grb0)
        {
            var mock1 = new Mock<IGameServiceDL>();
            var mock2 = new Mock<IStatisticServiceBL>();
            var mock3 = new Mock<IBotService>();

            mock1.Setup(cfg => cfg.GetGameHistoriesAsync(gameId)).Returns(Task.FromResult(gde1));
            mock1.Setup(cfg => cfg.GetGameByGameIdAsync(gameId)).Returns(Task.FromResult(gd0));
            mock1.Setup(cfg => cfg.SavePlayerMoveAsync(ghd0)).Returns(Task.FromResult(default(object)));
            mock2.Setup(cfg => cfg.SaveStatisticAsync(grb0)).Returns(Task.FromResult(true));

            var service = GetGameService(mock1, mock2, mock3);
            var result = await service.SavePlayerMoveAsync(ghb0);

            Assert.Equal(CheckStateBL.None, result);
        }

        [Theory]
        [ClassData(typeof(GameTestData5))]
        public async Task Test5_SaveMove(Guid gameId, GameDL gd0, IEnumerable<GameHistoryDL> gde1, GameHistoryDL ghd0, GameHistoryBL ghb0, GameResultBL grb0)
        {
            var mock1 = new Mock<IGameServiceDL>();
            var mock2 = new Mock<IStatisticServiceBL>();
            var mock3 = new Mock<IBotService>();

            mock1.Setup(cfg => cfg.GetGameHistoriesAsync(gameId)).Returns(Task.FromResult(gde1));
            mock1.Setup(cfg => cfg.GetGameByGameIdAsync(gameId)).Returns(Task.FromResult(gd0));
            mock1.Setup(cfg => cfg.SavePlayerMoveAsync(ghd0)).Returns(Task.FromResult(default(object)));
            mock2.Setup(cfg => cfg.SaveStatisticAsync(grb0)).Returns(Task.FromResult(true));

            var service = GetGameService(mock1, mock2, mock3);
            var result = await service.SavePlayerMoveAsync(ghb0);

            Assert.Equal(CheckStateBL.None, result);
        }
    }
}
