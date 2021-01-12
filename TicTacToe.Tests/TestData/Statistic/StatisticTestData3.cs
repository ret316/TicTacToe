using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TicTacToe.BL.Models;
using TicTacToe.DL.Models;
using ResultStatus = TicTacToe.DL.Models.ResultStatus;

namespace TicTacToe.Tests.TestData.Statistic
{
    public class StatisticTestData3 : IEnumerable<object[]>
    {
        private static Guid Id = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");
        private static Guid GameId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc10");
        private static Guid PlayerId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc11");

        GameResultDL res1 = new GameResultDL { Id = Id, GameId = GameId, PlayerId = PlayerId, Result = ResultStatus.Won };
        GameResultBL res2 = new GameResultBL { Id = Id, GameId = GameId, PlayerId = PlayerId, Result = BL.Models.ResultStatus.Won };

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {res1, res2};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
