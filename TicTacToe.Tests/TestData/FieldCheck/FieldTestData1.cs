using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TicTacToe.BusinessComponent.Models;

namespace TicTacToe.Tests.TestData.FieldCheck
{
    public class FieldTestData1 : IEnumerable<object[]>
    {
        private static Guid Id = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");
        private static Guid GameId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc10");
        private static Guid PlayerId1 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc11");
        private static Guid PlayerId2 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc12");

        static IEnumerable<GameHistory> bgh0 = new List<GameHistory>
        {
            new GameHistory
            {
                PlayerId = PlayerId1,
                GameId = GameId,
                IsBot = false,
                XAxis = 1,
                YAxis = 1,
                MoveDate = DateTime.Parse("2020-10-10")
            }
        };
        static char[,] b0 = new char[3, 3]
        {
            {'\0', '\0', '\0'}, {'\0', 'X', '\0'}, {'\0', '\0', '\0'}
        };
        static IEnumerable<GameHistory> bgh1 = new List<GameHistory>
        {
            new GameHistory
            {
                PlayerId = PlayerId1,
                GameId = GameId,
                IsBot = false,
                XAxis = 1,
                YAxis = 1,
                MoveDate = DateTime.Parse("2020-10-10")
            },
            new GameHistory
            {
                PlayerId = PlayerId2,
                GameId = GameId,
                IsBot = false,
                XAxis = 2,
                YAxis = 2,
                MoveDate = DateTime.Parse("2020-10-10")
            },
            new GameHistory
            {
                PlayerId = PlayerId1,
                GameId = GameId,
                IsBot = false,
                XAxis = 1,
                YAxis = 0,
                MoveDate = DateTime.Parse("2020-10-10")
            },
            new GameHistory
            {
                PlayerId = PlayerId2,
                GameId = GameId,
                IsBot = false,
                XAxis = 2,
                YAxis = 1,
                MoveDate = DateTime.Parse("2020-10-10")
            },
            new GameHistory
            {
                PlayerId = PlayerId1,
                GameId = GameId,
                IsBot = false,
                XAxis = 1,
                YAxis = 2,
                MoveDate = DateTime.Parse("2020-10-10")
            },

        };
        static char[,] b1 = new char[3, 3]
        {
            {'\0', 'X', '\0'}, {'\0', 'X', 'O'}, {'\0', 'X', 'O'}
        };
        static IEnumerable<GameHistory> bgh2 = new List<GameHistory>
        {
            new GameHistory
            {
                PlayerId = PlayerId1,
                GameId = GameId,
                IsBot = false,
                XAxis = 1,
                YAxis = 1,
                MoveDate = DateTime.Parse("2020-10-10")
            },
            new GameHistory
            {
                PlayerId = PlayerId2,
                GameId = GameId,
                IsBot = false,
                XAxis = 2,
                YAxis = 1,
                MoveDate = DateTime.Parse("2020-10-10")
            },
            new GameHistory
            {
                PlayerId = PlayerId1,
                GameId = GameId,
                IsBot = false,
                XAxis = 2,
                YAxis = 0,
                MoveDate = DateTime.Parse("2020-10-10")
            },
            new GameHistory
            {
                PlayerId = PlayerId2,
                GameId = GameId,
                IsBot = false,
                XAxis = 1,
                YAxis = 0,
                MoveDate = DateTime.Parse("2020-10-10")
            },
            new GameHistory
            {
                PlayerId = PlayerId1,
                GameId = GameId,
                IsBot = false,
                XAxis = 0,
                YAxis = 2,
                MoveDate = DateTime.Parse("2020-10-10")
            },
        };
        static char[,] b2 = new char[3, 3]
        {
            {'\0', 'O', 'X'}, {'\0', 'X', 'O'}, {'X', '\0', '\0'}
        };
        static IEnumerable<GameHistory> bgh3 = new List<GameHistory>
                {
                    new GameHistory
                    {
                        PlayerId = PlayerId1,
                        GameId = GameId,
                        IsBot = false,
                        XAxis = 1,
                        YAxis = 1,
                        MoveDate = DateTime.Parse("2020-10-10")
                    },
                    new GameHistory
                    {
                        PlayerId = PlayerId2,
                        GameId = GameId,
                        IsBot = false,
                        XAxis = 2,
                        YAxis = 0,
                        MoveDate = DateTime.Parse("2020-10-10")
                    },
                    new GameHistory
                    {
                        PlayerId = PlayerId1,
                        GameId = GameId,
                        IsBot = false,
                        XAxis = 2,
                        YAxis = 2,
                        MoveDate = DateTime.Parse("2020-10-10")
                    },
                    new GameHistory
                    {
                        PlayerId = PlayerId2,
                        GameId = GameId,
                        IsBot = false,
                        YAxis = 0,
                        XAxis = 0,
                        MoveDate = DateTime.Parse("2020-10-10")
                    },
                    new GameHistory
                    {
                        PlayerId = PlayerId1,
                        GameId = GameId,
                        IsBot = false,
                        XAxis = 1,
                        YAxis = 0,
                        MoveDate = DateTime.Parse("2020-10-10")
                    },
                    new GameHistory
                    {
                        PlayerId = PlayerId2,
                        GameId = GameId,
                        IsBot = false,
                        XAxis = 1,
                        YAxis = 2,
                        MoveDate = DateTime.Parse("2020-10-10")
                    },
                    new GameHistory
                    {
                        PlayerId = PlayerId1,
                        GameId = GameId,
                        IsBot = false,
                        XAxis = 0,
                        YAxis = 1,
                        MoveDate = DateTime.Parse("2020-10-10")
                    },
                    new GameHistory
                    {
                        PlayerId = PlayerId2,
                        GameId = GameId,
                        IsBot = false,
                        XAxis = 2,
                        YAxis = 1,
                        MoveDate = DateTime.Parse("2020-10-10")
                    },
                    new GameHistory
                    {
                        PlayerId = PlayerId1,
                        GameId = GameId,
                        IsBot = false,
                        XAxis = 0,
                        YAxis = 2,
                        MoveDate = DateTime.Parse("2020-10-10")
                    },
                };
        static char[,] b3 = new char[3, 3]
        {
            {'O', 'X', 'O'}, {'X', 'X', 'O'}, {'X', 'O', 'X'}
        };

        private List<IEnumerable<GameHistory>> list1 = new List<IEnumerable<GameHistory>> {bgh0, bgh1, bgh2, bgh3};
        private List<char[,]> list2 = new List<char[,]> {b0, b1, b2, b3};
        public IEnumerator<object[]> GetEnumerator()
        {
            for (int i = 0; i < list1.Count; i++)
            {
                yield return new object[] { list2[i], list1[i] };
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
