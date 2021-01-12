using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TicTacToe.BL.Models;
using TicTacToe.DL.Models;

namespace TicTacToe.Tests.TestData.Statistic
{
    public class StatisticTestData2 : IEnumerable<object[]>
    {
        private static Guid Id = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");
        private static Guid GameId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc10");
        private static Guid PlayerId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc11");

        IEnumerable<GameHistoryDL> list1 = new List<GameHistoryDL> 
        { 
            new GameHistoryDL
            {
                Id = Id,
                GameId = GameId,
                PlayerId = PlayerId,
                IsBot = false,
                XAxis = 1,
                YAxis = 1,
                MoveDate = DateTime.Parse("2020-10-10")
            }
        };
        IEnumerable<GameHistoryBL> list2 = new List<GameHistoryBL>
        {
            new GameHistoryBL
            {
                GameId = GameId,
                PlayerId = PlayerId,
                IsBot = false,
                XAxis = 1,
                YAxis = 1,
                MoveDate = DateTime.Parse("2020-10-10")
            }
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { GameId, list1, list2};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
