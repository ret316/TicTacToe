using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TicTacToe.BusinessComponent.Models;
using TicTacToe.DataComponent.Enum;
using TicTacToe.BusinessComponent.Enum;
using TicTacToe.DataComponent.Models;

namespace TicTacToe.Tests.TestData.Statistic
{
    public class StatisticTestData1 : IEnumerable<object[]>
    {
        private static Guid Id = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");
        private static Guid GameId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc10");
        private static Guid PlayerId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc11");

        static DataComponent.Models.GameResult stat = new DataComponent.Models.GameResult { Id = Id, GameId = GameId, PlayerId = PlayerId, Result = DataComponent.Enum.ResultStatus.Won };
        static IEnumerable<DataComponent.Models.GameResult> list1 = new List<DataComponent.Models.GameResult> { stat };
        static BusinessComponent.Models.GameResult stat2 = new BusinessComponent.Models.GameResult { Id = Id, GameId = GameId, PlayerId = PlayerId, Result = BusinessComponent.Enum.ResultStatus.Won };
        static IEnumerable<BusinessComponent.Models.GameResult> list2 = new List<BusinessComponent.Models.GameResult> { stat2 };

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {Id, list1, list2};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
