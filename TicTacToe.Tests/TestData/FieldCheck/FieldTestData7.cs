using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TicTacToe.BusinessComponent.Models;

namespace TicTacToe.Tests.TestData.FieldCheck
{
    public class FieldTestData7 : IEnumerable<object[]>
    {
        private static Guid Id = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");
        private static Guid GameId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc10");
        private static Guid PlayerId1 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc11");
        private static Guid PlayerId2 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc12");

        static GameHistory bh0 = new GameHistory
        {
            PlayerId = PlayerId1,
            GameId = GameId,
            IsBot = false,
            XAxis = 1,
            YAxis = 1,
            MoveDate = DateTime.Parse("2020-10-10")
        };
        static IEnumerable<GameHistory> bgh0 = new List<GameHistory> { bh0 };
        static GameHistory bh3 = new GameHistory
        {
            PlayerId = null,
            GameId = GameId,
            IsBot = true,
            XAxis = 2,
            YAxis = 2,
            MoveDate = DateTime.Parse("2020-10-10")
        };
        static IEnumerable<GameHistory> bgh4 = new List<GameHistory>
        {
            new GameHistory
            {
                PlayerId = PlayerId2,
                GameId = GameId,
                IsBot = true,
                XAxis = 2,
                YAxis = 2,
                MoveDate = DateTime.Parse("2020-10-10")
            }
        };

        private List<IEnumerable<GameHistory>> list1 = new List<IEnumerable<GameHistory>> {bgh0, bgh4};
        private List<GameHistory> list2 = new List<GameHistory> {bh0, bh3};


        public IEnumerator<object[]> GetEnumerator()
        {
            for (int i = 0; i < list1.Count; i++)
            {
                yield return new object[] {list1[i], list2[i]};
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
