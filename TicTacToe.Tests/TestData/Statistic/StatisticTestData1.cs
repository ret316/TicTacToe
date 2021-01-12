using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TicTacToe.BL.Models;
using TicTacToe.DL.Models;
using ResultStatus = TicTacToe.DL.Models.ResultStatus;

namespace TicTacToe.Tests.TestData.Statistic
{
    public class StatisticTestData1 : IEnumerable<object[]>
    {
        private static Guid Id = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");
        private static Guid GameId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc10");
        private static Guid PlayerId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc11");

        static GameResultDL stat = new GameResultDL { Id = Id, GameId = GameId, PlayerId = PlayerId, Result = ResultStatus.Won };
        static IEnumerable<GameResultDL> list1 = new List<GameResultDL> { stat };
        static GameResultBL stat2 = new GameResultBL { Id = Id, GameId = GameId, PlayerId = PlayerId, Result = BL.Models.ResultStatus.Won };
        static IEnumerable<GameResultBL> list2 = new List<GameResultBL> { stat2 };

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {Id, list1, list2};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
