using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TicTacToe.BL.Models;
using TicTacToe.DL.Models;

namespace TicTacToe.Tests.TestData.Statistic
{
    public class StatisticTestData4 : IEnumerable<object[]>
    {
        private static Guid Id = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");
        private static Guid GameId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc10");
        private static Guid PlayerId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc11");

        IEnumerable<UserGamesStatisticBL> list1 = new List<UserGamesStatisticBL> { new UserGamesStatisticBL
            { PlayerId = Id, GameCount = 10, WinCount = 5, LostCount = 3, DrawCount = 2 } };
        IEnumerable<UserGamesStatisticDL> list2 = new List<UserGamesStatisticDL> { new UserGamesStatisticDL
            { PlayerId = Id, GameCount = 10, WinCount = 5, LostCount = 3, DrawCount = 2 } };

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { list1, list2 };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
