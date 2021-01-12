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
using TicTacToe.Tests.TestData.Statistic;
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

        [Theory]
        [ClassData(typeof(StatisticTestData1))]
        public async Task Test1_GetStatistics(Guid id, IEnumerable<GameResultDL> list1, IEnumerable<GameResultBL> list2)
        {
            var mock = new Mock<IStatisticServiceDL>();
            mock.Setup(it => it.GetAllUserGamesAsync(id)).Returns(Task.FromResult(list1));

            var service = GetStatisticService(mock);
            var result = await service.GetAllUserGamesAsync(id);

            Assert.Equal(list2.Select(x => x.GameId), result.Select(x => x.GameId));
        }

        [Theory]
        [ClassData(typeof(StatisticTestData2))]
        public async Task Test2_GetHistory(Guid gameId, IEnumerable<GameHistoryDL> list1, IEnumerable<GameHistoryBL> list2)
        {
            var mock = new Mock<IStatisticServiceDL>();
            mock.Setup(it => it.GetGameHistoryAsync(gameId)).Returns(Task.FromResult(list1));

            var service = GetStatisticService(mock);
            var result = await service.GetGameHistoryAsync(gameId);

            Assert.Equal(list2.Select(x => x.GameId), result.Select(x => x.GameId));
        }

        [Theory]
        [ClassData(typeof(StatisticTestData3))]
        public async Task Test3_SaveMove(GameResultDL res1, GameResultBL res2)
        {
            var mock = new Mock<IStatisticServiceDL>();
            mock.Setup(it => it.SaveStatisticAsync(res1)).Returns(Task.FromResult(default(object)));

            var service = GetStatisticService(mock);
            var result = await service.SaveStatisticAsync(res2);

            Assert.True(result);
        }

        [Theory]
        [ClassData(typeof(StatisticTestData4))]
        public async Task Test4_Top10(IEnumerable<UserGamesStatisticBL> list1, IEnumerable<UserGamesStatisticDL> list2)
        {
            var mock = new Mock<IStatisticServiceDL>();
            mock.Setup(it => it.GetTop10PlayersAsync()).Returns(Task.FromResult(list2));

            var service = GetStatisticService(mock);
            var result = await service.GetTop10PlayersAsync();

            Assert.Equal(list1.Select(x => x.PlayerId), result.Select(x => x.PlayerId));
        }
    }
}
