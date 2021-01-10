using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.BL.Models;
using TicTacToe.BL.Services;
using TicTacToe.BL.Services.Implementation;
using Xunit;

namespace TicTacToe.Tests.UnitTests
{
    public class FieldCheckerTests
    {

        private static Guid Id = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");
        private static Guid GameId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc10");
        private static Guid PlayerId1 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc11");
        private static Guid PlayerId2 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc12");

        public class Colls
        {
            public GameBL bg0;
            public GameBL bg1;
            public GameHistoryBL bh0;
            public GameHistoryBL bh1;
            public GameHistoryBL bh2;
            public GameHistoryBL bh3;
            public IEnumerable<GameHistoryBL> bgh0;
            public IEnumerable<GameHistoryBL> bgh1;
            public IEnumerable<GameHistoryBL> bgh2;
            public IEnumerable<GameHistoryBL> bgh3;
            public IEnumerable<GameHistoryBL> bgh4;
            public char[,] b0;
            public char[,] b1;
            public char[,] b2;
            public char[,] b3;

            public Colls()
            {
                bh0 = new GameHistoryBL
                {
                    PlayerId = PlayerId1,
                    GameId = GameId,
                    IsBot = false,
                    XAxis = 1,
                    YAxis = 1,
                    MoveDate = DateTime.Parse("2020-10-10")
                };
                bh1 = new GameHistoryBL
                {
                    PlayerId = PlayerId1,
                    GameId = GameId,
                    IsBot = false,
                    XAxis = 5,
                    YAxis = 5,
                    MoveDate = DateTime.Parse("2020-10-10")
                };
                bh2 = new GameHistoryBL
                {
                    PlayerId = Guid.NewGuid(),
                    GameId = GameId,
                    IsBot = false,
                    XAxis = 5,
                    YAxis = 5,
                    MoveDate = DateTime.Parse("2020-10-10")
                };
                bh3 = new GameHistoryBL
                {
                    PlayerId = null,
                    GameId = GameId,
                    IsBot = true,
                    XAxis = 2,
                    YAxis = 2,
                    MoveDate = DateTime.Parse("2020-10-10")
                };
                bgh0 = new List<GameHistoryBL> { bh0 };
                bgh1 = new List<GameHistoryBL>
                {
                    bh0,
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
                bgh2 = new List<GameHistoryBL>
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
                bgh3 = new List<GameHistoryBL>
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
                bgh4 = new List<GameHistoryBL>
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
                b0 = new char[3, 3]
                {
                    {'\0', '\0', '\0'}, {'\0', 'X', '\0'}, {'\0', '\0', '\0'}
                };
                b1 = new char[3, 3]
                {
                    {'\0', 'X', '\0'}, {'\0', 'X', 'O'}, {'\0', 'X', 'O'}
                };
                b2 = new char[3, 3]
                {
                    {'\0', 'O', 'X'}, {'\0', 'X', 'O'}, {'X', '\0', '\0'}
                };
                b3 = new char[3, 3]
                {
                    {'O', 'X', 'O'}, {'X', 'X', 'O'}, {'X', 'O', 'X'}
                };
                bg0 = new GameBL
                {
                    Id = Id,
                    GameId = GameId,
                    Player1Id = PlayerId1,
                    Player2Id = PlayerId2,
                    IsPlayer2Bot = false,
                    IsGameFinished = false
                };
                bg1 = new GameBL
                {
                    Id = Id,
                    GameId = GameId,
                    Player1Id = PlayerId1,
                    Player2Id = null,
                    IsPlayer2Bot = true,
                    IsGameFinished = false
                };
            }
        }

        [Fact]
        public void BoardInitTest()
        {
            IFieldChecker _fieldChecker = new FIeldChecker();
            var data = new Colls();
            _fieldChecker.BoardInit(data.bgh0);
            Assert.Equal(data.b0, _fieldChecker.Board);
            _fieldChecker.BoardInit(data.bgh1);
            Assert.Equal(data.b1, _fieldChecker.Board);
            _fieldChecker.BoardInit(data.bgh2);
            Assert.Equal(data.b2, _fieldChecker.Board);
            _fieldChecker.BoardInit(data.bgh3);
            Assert.Equal(data.b3, _fieldChecker.Board);
        }

        [Fact]
        public void LinesTest()
        {
            IFieldChecker _fieldChecker = new FIeldChecker();
            var data = new Colls();
            _fieldChecker.BoardInit(data.bgh0);
            Assert.False(_fieldChecker.LinesCheck());
            _fieldChecker.BoardInit(data.bgh1);
            Assert.True(_fieldChecker.LinesCheck());
            _fieldChecker.BoardInit(data.bgh2);
            Assert.False(_fieldChecker.LinesCheck());
            _fieldChecker.BoardInit(data.bgh3);
            Assert.False(_fieldChecker.LinesCheck());
        }

        [Fact]
        public void DiagonalsTest()
        {
            IFieldChecker _fieldChecker = new FIeldChecker();
            var data = new Colls();
            _fieldChecker.BoardInit(data.bgh0);
            Assert.False(_fieldChecker.DCheck());
            _fieldChecker.BoardInit(data.bgh1);
            Assert.False(_fieldChecker.DCheck());
            _fieldChecker.BoardInit(data.bgh2);
            Assert.False(_fieldChecker.DCheck());
            _fieldChecker.BoardInit(data.bgh3);
            Assert.False(_fieldChecker.DCheck());
        }

        [Fact]
        public void DoubleCellTest()
        {
            IFieldChecker _fieldChecker = new FIeldChecker();
            var data = new Colls();
            
            _fieldChecker.BoardInit(data.bgh4);
            _fieldChecker.NextMove = data.bh0;
            Assert.False(_fieldChecker.DoubleCellCheck());
            _fieldChecker.MakeMove();
            _fieldChecker.BoardInit(data.bgh3);
            Assert.True(_fieldChecker.DoubleCellCheck());
        }

        [Fact]
        public void IndexTest()
        {
            IFieldChecker _fieldChecker = new FIeldChecker();
            var data = new Colls();

            _fieldChecker.BoardInit(data.bgh4);
            _fieldChecker.NextMove = data.bh0;
            Assert.False(_fieldChecker.IndexCheck());

            _fieldChecker.MakeMove();
            _fieldChecker.NextMove = data.bh1;
            Assert.True(_fieldChecker.IndexCheck());
        }

        [Fact]
        public void EndGameTest()
        {
            IFieldChecker _fieldChecker = new FIeldChecker();
            var data = new Colls();

            _fieldChecker.BoardInit(data.bgh3);
            Assert.True(_fieldChecker.EndGameCheck(true));
        }

        [Fact]
        public void LastPlayerTest()
        {
            IFieldChecker _fieldChecker = new FIeldChecker();
            var data = new Colls();

            _fieldChecker.BoardInit(data.bgh0);
            _fieldChecker.NextMove = data.bh0;
            Assert.True(_fieldChecker.LastPlayerCheck());
            _fieldChecker.BoardInit(data.bgh4);
            _fieldChecker.NextMove = data.bh3;
            Assert.True(_fieldChecker.LastPlayerCheck());
        }

        [Fact]
        public void PlayerTest()
        {
            IFieldChecker _fieldChecker = new FIeldChecker();
            var data = new Colls();

            _fieldChecker.BoardInit(data.bgh0);
            _fieldChecker.NextMove = data.bh2;
            Assert.True(_fieldChecker.GamePlayerCheck(data.bg0));
            _fieldChecker.NextMove = data.bh3;
            Assert.False(_fieldChecker.GamePlayerCheck(data.bg1));
        }
    }
}
