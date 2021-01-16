using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Moq;
using TicTacToe.BusinessComponent.Config;
using TicTacToe.BusinessComponent.Models;
using TicTacToe.BusinessComponent.Services;
using TicTacToe.BusinessComponent.Services.Implementation;
using TicTacToe.DataComponent.Models;
using TicTacToe.DataComponent.Services;
using TicTacToe.Tests.TestData.Statistic;
using Xunit;

namespace TicTacToe.Tests.UnitTests
{
    public class StatisticServiceTests
    {
        private BusinessComponent.Services.IStatisticService GetStatisticService(Mock<DataComponent.Services.IStatisticService> mock)
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile()));
            IMapper mapper = new Mapper(config);
            return new StatisticService(mock.Object, mapper);
        }

        [Theory]
        [ClassData(typeof(StatisticTestData1))]
        public async Task Test1_GetStatistics(Guid id, IEnumerable<DataComponent.Models.GameResult> list1, IEnumerable<BusinessComponent.Models.GameResult> list2)
        {
            var mock = new Mock<DataComponent.Services.IStatisticService>();
            mock.Setup(it => it.GetAllUserGamesAsync(id)).Returns(Task.FromResult(list1));

            var service = GetStatisticService(mock);
            var result = await service.GetAllUserGamesAsync(id);

            Assert.Equal(list2.Select(x => x.GameId), result.Select(x => x.GameId));
        }

        [Theory]
        [ClassData(typeof(StatisticTestData2))]
        public async Task Test2_GetHistory(Guid gameId, IEnumerable<DataComponent.Models.GameHistory> list1, IEnumerable<BusinessComponent.Models.GameHistory> list2)
        {
            var mock = new Mock<DataComponent.Services.IStatisticService>();
            mock.Setup(it => it.GetGameHistoryAsync(gameId)).Returns(Task.FromResult(list1));

            var service = GetStatisticService(mock);
            var result = await service.GetGameHistoryAsync(gameId);

            Assert.Equal(list2.Select(x => x.GameId), result.Select(x => x.GameId));
        }

        [Theory]
        [ClassData(typeof(StatisticTestData3))]
        public async Task Test3_SaveMove(DataComponent.Models.GameResult res1, BusinessComponent.Models.GameResult res2)
        {
            var mock = new Mock<DataComponent.Services.IStatisticService>();
            mock.Setup(it => it.SaveStatisticAsync(res1)).Returns(Task.FromResult(default(object)));

            var service = GetStatisticService(mock);
            var result = await service.SaveStatisticAsync(res2);

            Assert.True(result);
        }

        [Theory]
        [ClassData(typeof(StatisticTestData4))]
        public async Task Test4_Top10(IEnumerable<BusinessComponent.Models.UserGamesStatistic> list1, IEnumerable<DataComponent.Models.UserGamesStatistic> list2)
        {
            var mock = new Mock<DataComponent.Services.IStatisticService>();
            mock.Setup(it => it.GetTop10PlayersAsync()).Returns(Task.FromResult(list2));

            var service = GetStatisticService(mock);
            var result = await service.GetTop10PlayersAsync();

            Assert.Equal(list1.Select(x => x.PlayerId), result.Select(x => x.PlayerId));
        }
    }
}
