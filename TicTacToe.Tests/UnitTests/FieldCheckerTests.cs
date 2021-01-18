using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.BusinessComponent.Models;
using TicTacToe.BusinessComponent.Services;
using TicTacToe.BusinessComponent.Services.Implementation;
using TicTacToe.Tests.TestData.FieldCheck;
using Xunit;

namespace TicTacToe.Tests.UnitTests
{
    public class FieldCheckerTests
    {
        [Theory]
        [ClassData(typeof(FieldTestData1))]
        public void Test1_BoardInit(char[,] board, IEnumerable<GameHistory> list)
        {
            IFieldChecker fieldChecker = new FieldChecker();
            fieldChecker.BoardInit(list);

            Assert.Equal(board, fieldChecker.Board);
        }

        [Theory]
        [ClassData(typeof(FieldTestData2))]
        public void Test2_LinesTest(IEnumerable<GameHistory> list)
        {
            IFieldChecker fieldChecker = new FieldChecker();
            fieldChecker.BoardInit(list);
            Assert.False(fieldChecker.LinesCheck());
        }

        [Theory]
        [ClassData(typeof(FieldTestData2))]
        public void Test3_Diagonals(IEnumerable<GameHistory> list)
        {
            IFieldChecker fieldChecker = new FieldChecker();
            fieldChecker.BoardInit(list);
            Assert.False(fieldChecker.DCheck());
        }

        [Theory]
        [ClassData(typeof(FieldTestData4))]
        public void Test4_DoubleCell(GameHistory bh0, IEnumerable<GameHistory> bgh3, IEnumerable<GameHistory> bgh4)
        {
            IFieldChecker fieldChecker = new FieldChecker();
            
            fieldChecker.BoardInit(bgh4);
            fieldChecker.NextMove = bh0;
            Assert.False(fieldChecker.DoubleCellCheck());

            fieldChecker.MakeMove();
            fieldChecker.BoardInit(bgh3);
            Assert.True(fieldChecker.DoubleCellCheck());
        }

        [Theory]
        [ClassData(typeof(FieldTestData5))]
        public void Test5_Index(GameHistory bh0, GameHistory bh1, IEnumerable<GameHistory> bgh4)
        {
            IFieldChecker fieldChecker = new FieldChecker();

            fieldChecker.BoardInit(bgh4);
            fieldChecker.NextMove = bh0;
            Assert.False(fieldChecker.IndexCheck());

            fieldChecker.MakeMove();
            fieldChecker.NextMove = bh1;
            Assert.True(fieldChecker.IndexCheck());
        }

        [Theory]
        [ClassData(typeof(FieldTestData6))]
        public void Test6_EndGameTest(IEnumerable<GameHistory> bgh3)
        {
            IFieldChecker fieldChecker = new FieldChecker();

            fieldChecker.BoardInit(bgh3);
            Assert.True(fieldChecker.EndGameCheck(true));
        }

        [Theory]
        [ClassData(typeof(FieldTestData7))]
        public void Test7_LastPlayer(IEnumerable<GameHistory> bgh0, GameHistory bh0)
        {
            IFieldChecker fieldChecker = new FieldChecker();

            fieldChecker.BoardInit(bgh0);
            fieldChecker.NextMove = bh0;
            Assert.True(fieldChecker.LastPlayerCheck());
        }

        [Theory]
        [ClassData(typeof(FieldTestData8))]
        public void Test8_PlayerTest(IEnumerable<GameHistory> bgh0, GameHistory bh2, GameHistory bh3, Game bg0, Game bg1)
        {
            IFieldChecker _fieldChecker = new FieldChecker();

            _fieldChecker.BoardInit(bgh0);
            _fieldChecker.NextMove = bh2;
            Assert.True(_fieldChecker.GamePlayerCheck(bg0));
            _fieldChecker.NextMove = bh3;
            Assert.False(_fieldChecker.GamePlayerCheck(bg1));
        }
    }
}
