using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TicTacToe.BL.Config;
using TicTacToe.BL.Models;
using TicTacToe.BL.Services;
using TicTacToe.BL.Services.Implementation;
using TicTacToe.DL.Models;
using TicTacToe.DL.Services;
using Xunit;
using ResultStatus = TicTacToe.DL.Models.ResultStatus;

namespace TicTacToe.Tests.UnitTests
{
    public class StatisticServiceTests
    {
        private IStatisticServiceBL GetStatisticService(Mock<IStatisticServiceDL> mock)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile()));
            IMapper mapper = new Mapper(config);
            return new StatisticServiceBL(mock.Object, mapper);
        }

        private Guid Id = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");
        private Guid GameId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc10");
        private Guid PlayerId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc11");

        [Fact]
        public async Task GetStatistics()
        {
            var stat = new GameResultDL {Id = Id, GameId = GameId, PlayerId = PlayerId, Result = ResultStatus.Won};
            IEnumerable<GameResultDL> list1 = new List<GameResultDL> {stat};
            var stat2 = new GameResultBL { Id = Id, GameId = GameId, PlayerId = PlayerId, Result = BL.Models.ResultStatus.Won };
            IEnumerable<GameResultBL> list2 = new List<GameResultBL> { stat2 };

            var mock = new Mock<IStatisticServiceDL>();
            mock.Setup(it => it.GetAllUserGamesAsync(Id)).Returns(Task.FromResult(list1));

            var service = GetStatisticService(mock);
            var result = await service.GetAllUserGamesAsync(Id);

            Assert.Equal(list2.Select(x => x.GameId), result.Select(x => x.GameId));
        }

        [Fact]
        public async Task GetHistory()
        {
            var history1 = new GameHistoryDL
            {
                Id = Id, GameId = GameId, PlayerId = PlayerId, IsBot = false, XAxis = 1, YAxis = 1,
                MoveDate = DateTime.Parse("2020-10-10")
            };
            IEnumerable<GameHistoryDL> list1 = new List<GameHistoryDL> {history1};
            var history2 = new GameHistoryBL
            {
                GameId = GameId, PlayerId = PlayerId, IsBot = false, XAxis = 1, YAxis = 1,
                MoveDate = DateTime.Parse("2020-10-10")
            };
            IEnumerable<GameHistoryBL> list2 = new List<GameHistoryBL> { history2 };

            var mock = new Mock<IStatisticServiceDL>();
            mock.Setup(it => it.GetGameHistoryAsync(GameId)).Returns(Task.FromResult(list1));

            var service = GetStatisticService(mock);
            var result = await service.GetGameHistoryAsync(GameId);

            Assert.Equal(list2.Select(x => x.GameId), result.Select(x => x.GameId));
        }

        [Fact]
        public async Task SaveMove()
        {
            var res1 = new GameResultDL {Id = Id, GameId = GameId, PlayerId = PlayerId, Result = ResultStatus.Won};
            var res2 = new GameResultBL { Id = Id, GameId = GameId, PlayerId = PlayerId, Result = BL.Models.ResultStatus.Won };

            var mock = new Mock<IStatisticServiceDL>();
            mock.Setup(it => it.SaveStatisticAsync(res1)).Returns(Task.FromResult(default(object)));

            var service = GetStatisticService(mock);
            var result = await service.SaveStatisticAsync(res2);

            Assert.True(result);
        }

        [Fact]
        public async Task Top10()
        {
            var res1 = new UserGamesStatisticBL
                {PlayerId = Id, GameCount = 10, WinCount = 5, LostCount = 3, DrawCount = 2};
            IEnumerable<UserGamesStatisticBL> list1 = new List<UserGamesStatisticBL> {res1};
            var res2 = new UserGamesStatisticDL
                { PlayerId = Id, GameCount = 10, WinCount = 5, LostCount = 3, DrawCount = 2 };
            IEnumerable<UserGamesStatisticDL> list2 = new List<UserGamesStatisticDL> { res2 };

            var mock = new Mock<IStatisticServiceDL>();
            mock.Setup(it => it.GetTop10PlayersAsync()).Returns(Task.FromResult(list2));

            var service = GetStatisticService(mock);
            var result = await service.GetTop10PlayersAsync();

            Assert.Equal(list1.Select(x => x.PlayerId), result.Select(x => x.PlayerId));
        }
    }
}
