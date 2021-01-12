using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TicTacToe.BL.Models;

namespace TicTacToe.Tests.TestData.FieldCheck
{
    public class FieldTestData2 : IEnumerable<object[]>
    {
        private static Guid Id = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");
        private static Guid GameId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc10");
        private static Guid PlayerId1 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc11");
        private static Guid PlayerId2 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc12");

        static IEnumerable<GameHistoryBL> bgh0 = new List<GameHistoryBL>
        {
            new GameHistoryBL
            {
                PlayerId = PlayerId1,
                GameId = GameId,
                IsBot = false,
                XAxis = 1,
                YAxis = 1,
                MoveDate = DateTime.Parse("2020-10-10")
            }
        };
        static IEnumerable<GameHistoryBL> bgh1 = new List<GameHistoryBL>
        {
            new GameHistoryBL
            {
                PlayerId = PlayerId1,
                GameId = GameId,
                IsBot = false,
                XAxis = 1,
                YAxis = 1,
                MoveDate = DateTime.Parse("2020-10-10")
            },
            new GameHistoryBL
            {
                PlayerId = PlayerId2,
                GameId = GameId,
                IsBot = false,
                XAxis = 2,
                YAxis = 2,
                MoveDate = DateTime.Parse("2020-10-10")
            },
            new GameHistoryBL
            {
                PlayerId = PlayerId1,
                GameId = GameId,
                IsBot = false,
                XAxis = 1,
                YAxis = 0,
                MoveDate = DateTime.Parse("2020-10-10")
            },
            new GameHistoryBL
            {
                PlayerId = PlayerId2,
                GameId = GameId,
                IsBot = false,
                XAxis = 2,
                YAxis = 1,
                MoveDate = DateTime.Parse("2020-10-10")
            },
            new GameHistoryBL
            {
                PlayerId = PlayerId1,
                GameId = GameId,
                IsBot = false,
                XAxis = 1,
                YAxis = 2,
                MoveDate = DateTime.Parse("2020-10-10")
            },

        };
        static IEnumerable<GameHistoryBL> bgh2 = new List<GameHistoryBL>
        {
            new GameHistoryBL
            {
                PlayerId = PlayerId1,
                GameId = GameId,
                IsBot = false,
                XAxis = 1,
                YAxis = 1,
                MoveDate = DateTime.Parse("2020-10-10")
            },
            new GameHistoryBL
            {
                PlayerId = PlayerId2,
                GameId = GameId,
                IsBot = false,
                XAxis = 2,
                YAxis = 1,
                MoveDate = DateTime.Parse("2020-10-10")
            },
            new GameHistoryBL
            {
                PlayerId = PlayerId1,
                GameId = GameId,
                IsBot = false,
                XAxis = 2,
                YAxis = 0,
                MoveDate = DateTime.Parse("2020-10-10")
            },
            new GameHistoryBL
            {
                PlayerId = PlayerId2,
                GameId = GameId,
                IsBot = false,
                XAxis = 1,
                YAxis = 0,
                MoveDate = DateTime.Parse("2020-10-10")
            },
            new GameHistoryBL
            {
                PlayerId = PlayerId1,
                GameId = GameId,
                IsBot = false,
                XAxis = 0,
                YAxis = 2,
                MoveDate = DateTime.Parse("2020-10-10")
            },
        };
        static IEnumerable<GameHistoryBL> bgh3 = new List<GameHistoryBL>
                {
                    new GameHistoryBL
                    {
                        PlayerId = PlayerId1,
                        GameId = GameId,
                        IsBot = false,
                        XAxis = 1,
                        YAxis = 1,
                        MoveDate = DateTime.Parse("2020-10-10")
                    },
                    new GameHistoryBL
                    {
                        PlayerId = PlayerId2,
                        GameId = GameId,
                        IsBot = false,
                        XAxis = 2,
                        YAxis = 0,
                        MoveDate = DateTime.Parse("2020-10-10")
                    },
                    new GameHistoryBL
                    {
                        PlayerId = PlayerId1,
                        GameId = GameId,
                        IsBot = false,
                        XAxis = 2,
                        YAxis = 2,
                        MoveDate = DateTime.Parse("2020-10-10")
                    },
                    new GameHistoryBL
                    {
                        PlayerId = PlayerId2,
                        GameId = GameId,
                        IsBot = false,
                        YAxis = 0,
                        XAxis = 0,
                        MoveDate = DateTime.Parse("2020-10-10")
                    },
                    new GameHistoryBL
                    {
                        PlayerId = PlayerId1,
                        GameId = GameId,
                        IsBot = false,
                        XAxis = 1,
                        YAxis = 0,
                        MoveDate = DateTime.Parse("2020-10-10")
                    },
                    new GameHistoryBL
                    {
                        PlayerId = PlayerId2,
                        GameId = GameId,
                        IsBot = false,
                        XAxis = 1,
                        YAxis = 2,
                        MoveDate = DateTime.Parse("2020-10-10")
                    },
                    new GameHistoryBL
                    {
                        PlayerId = PlayerId1,
                        GameId = GameId,
                        IsBot = false,
                        XAxis = 0,
                        YAxis = 1,
                        MoveDate = DateTime.Parse("2020-10-10")
                    },
                    new GameHistoryBL
                    {
                        PlayerId = PlayerId2,
                        GameId = GameId,
                        IsBot = false,
                        XAxis = 2,
                        YAxis = 1,
                        MoveDate = DateTime.Parse("2020-10-10")
                    },
                    new GameHistoryBL
                    {
                        PlayerId = PlayerId1,
                        GameId = GameId,
                        IsBot = false,
                        XAxis = 0,
                        YAxis = 2,
                        MoveDate = DateTime.Parse("2020-10-10")
                    },
                };

        private List<IEnumerable<GameHistoryBL>> list1 = new List<IEnumerable<GameHistoryBL>> {bgh0, /*bgh1,*/ bgh2, bgh3};
        public IEnumerator<object[]> GetEnumerator()
        {
            foreach (var item in list1)
            {
                yield return new object[] {item};
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
