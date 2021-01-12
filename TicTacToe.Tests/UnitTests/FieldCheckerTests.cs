using System;
using System.Collections.Generic;
using System.Text;
using TicTacToe.BL.Models;
using TicTacToe.BL.Services;
using TicTacToe.BL.Services.Implementation;
using TicTacToe.Tests.TestData.FieldCheck;
using Xunit;

namespace TicTacToe.Tests.UnitTests
{
    public class FieldCheckerTests
    {
        [Theory]
        [ClassData(typeof(FieldTestData1))]
        public void Test1_BoardInit(char[,] board, IEnumerable<GameHistoryBL> list)
        {
            IFieldChecker fieldChecker = new FIeldChecker();
            fieldChecker.BoardInit(list);

            Assert.Equal(board, fieldChecker.Board);
        }

        [Theory]
        [ClassData(typeof(FieldTestData2))]
        public void Test2_LinesTest(IEnumerable<GameHistoryBL> list)
        {
            IFieldChecker fieldChecker = new FIeldChecker();
            fieldChecker.BoardInit(list);
            Assert.False(fieldChecker.LinesCheck());
        }

        [Theory]
        [ClassData(typeof(FieldTestData2))]
        public void Test3_Diagonals(IEnumerable<GameHistoryBL> list)
        {
            IFieldChecker fieldChecker = new FIeldChecker();
            fieldChecker.BoardInit(list);
            Assert.False(fieldChecker.DCheck());
        }

        [Theory]
        [ClassData(typeof(FieldTestData4))]
        public void Test4_DoubleCell(GameHistoryBL bh0, IEnumerable<GameHistoryBL> bgh3, IEnumerable<GameHistoryBL> bgh4)
        {
            IFieldChecker fieldChecker = new FIeldChecker();
            
            fieldChecker.BoardInit(bgh4);
            fieldChecker.NextMove = bh0;
            Assert.False(fieldChecker.DoubleCellCheck());

            fieldChecker.MakeMove();
            fieldChecker.BoardInit(bgh3);
            Assert.True(fieldChecker.DoubleCellCheck());
        }

        [Theory]
        [ClassData(typeof(FieldTestData5))]
        public void Test5_Index(GameHistoryBL bh0, GameHistoryBL bh1, IEnumerable<GameHistoryBL> bgh4)
        {
            IFieldChecker fieldChecker = new FIeldChecker();

            fieldChecker.BoardInit(bgh4);
            fieldChecker.NextMove = bh0;
            Assert.False(fieldChecker.IndexCheck());

            fieldChecker.MakeMove();
            fieldChecker.NextMove = bh1;
            Assert.True(fieldChecker.IndexCheck());
        }

        [Theory]
        [ClassData(typeof(FieldTestData6))]
        public void Test6_EndGameTest(IEnumerable<GameHistoryBL> bgh3)
        {
            IFieldChecker fieldChecker = new FIeldChecker();

            fieldChecker.BoardInit(bgh3);
            Assert.True(fieldChecker.EndGameCheck(true));
        }

        [Theory]
        [ClassData(typeof(FieldTestData7))]
        public void Test7_LastPlayer(IEnumerable<GameHistoryBL> bgh0, GameHistoryBL bh0)
        {
            IFieldChecker fieldChecker = new FIeldChecker();

            fieldChecker.BoardInit(bgh0);
            fieldChecker.NextMove = bh0;
            Assert.True(fieldChecker.LastPlayerCheck());
        }

        [Theory]
        [ClassData(typeof(FieldTestData8))]
        public void Test8_PlayerTest(IEnumerable<GameHistoryBL> bgh0, GameHistoryBL bh2, GameHistoryBL bh3, GameBL bg0, GameBL bg1)
        {
            IFieldChecker _fieldChecker = new FIeldChecker();

            _fieldChecker.BoardInit(bgh0);
            _fieldChecker.NextMove = bh2;
            Assert.True(_fieldChecker.GamePlayerCheck(bg0));
            _fieldChecker.NextMove = bh3;
            Assert.False(_fieldChecker.GamePlayerCheck(bg1));
        }
    }
}
