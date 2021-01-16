using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TicTacToe.BusinessComponent.Models;
using TicTacToe.DataComponent.Models;

namespace TicTacToe.Tests.TestData.Statistic
{
    public class StatisticTestData3 : IEnumerable<object[]>
    {
        private static Guid Id = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");
        private static Guid GameId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc10");
        private static Guid PlayerId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc11");

        DataComponent.Models.GameResult res1 = new DataComponent.Models.GameResult { Id = Id, GameId = GameId, PlayerId = PlayerId, Result = DataComponent.Enum.ResultStatus.Won };
        BusinessComponent.Models.GameResult res2 = new BusinessComponent.Models.GameResult { Id = Id, GameId = GameId, PlayerId = PlayerId, Result = BusinessComponent.Enum.ResultStatus.Won };

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {res1, res2};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
