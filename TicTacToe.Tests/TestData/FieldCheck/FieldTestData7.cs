using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TicTacToe.BL.Models;

namespace TicTacToe.Tests.TestData.FieldCheck
{
    public class FieldTestData7 : IEnumerable<object[]>
    {
        private static Guid Id = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");
        private static Guid GameId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc10");
        private static Guid PlayerId1 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc11");
        private static Guid PlayerId2 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc12");

        static GameHistoryBL bh0 = new GameHistoryBL
        {
            PlayerId = PlayerId1,
            GameId = GameId,
            IsBot = false,
            XAxis = 1,
            YAxis = 1,
            MoveDate = DateTime.Parse("2020-10-10")
        };
        static IEnumerable<GameHistoryBL> bgh0 = new List<GameHistoryBL> { bh0 };
        static GameHistoryBL bh3 = new GameHistoryBL
        {
            PlayerId = null,
            GameId = GameId,
            IsBot = true,
            XAxis = 2,
            YAxis = 2,
            MoveDate = DateTime.Parse("2020-10-10")
        };
        static IEnumerable<GameHistoryBL> bgh4 = new List<GameHistoryBL>
        {
            new GameHistoryBL
            {
                PlayerId = PlayerId2,
                GameId = GameId,
                IsBot = true,
                XAxis = 2,
                YAxis = 2,
                MoveDate = DateTime.Parse("2020-10-10")
            }
        };

        private List<IEnumerable<GameHistoryBL>> list1 = new List<IEnumerable<GameHistoryBL>> {bgh0, bgh4};
        private List<GameHistoryBL> list2 = new List<GameHistoryBL> {bh0, bh3};


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
