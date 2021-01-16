using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TicTacToe.BusinessComponent.Models;
using TicTacToe.DataComponent.Models;

namespace TicTacToe.Tests.TestData.Bot
{
    public class BotTestData1 : IEnumerable<object[]>
    {
        private static Guid GameId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc10");
        private static Guid PlayerId1 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc11");
        DataComponent.Models.GameHistory move2 = new DataComponent.Models.GameHistory
        {
            PlayerId = PlayerId1,
            GameId = GameId,
            IsBot = false,
            XAxis = 1,
            YAxis = 1,
            MoveDate = DateTime.Parse("2020-10-10")
        };
        BusinessComponent.Models.GameHistory move1 = new BusinessComponent.Models.GameHistory
        {
            PlayerId = PlayerId1,
            GameId = GameId,
            IsBot = false,
            XAxis = 1,
            YAxis = 1,
            MoveDate = DateTime.Parse("2020-10-10")
        };
        char[,] board = new char[3, 3]
        {
            {'\0', '\0', '\0'}, {'\0', 'X', '\0'}, {'\0', '\0', '\0'}
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { move1, move2, board };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
