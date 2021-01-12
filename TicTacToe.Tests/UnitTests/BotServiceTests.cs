using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moq;
using TicTacToe.BL.Models;
using TicTacToe.DL.Models;
using TicTacToe.BL.Services;
using TicTacToe.BL.Services.Implementation;
using TicTacToe.DL.Services;
using TicTacToe.Tests.TestData.Bot;
using Xunit;

namespace TicTacToe.Tests.UnitTests
{
    public class BotServiceTests
    {
        public IBotService GetService(Mock<IGameServiceDL> mock)
        {
            IFieldChecker fieldChecker = new FIeldChecker();

            return new BotService(fieldChecker, mock.Object);
        }

        [Theory]
        [ClassData(typeof(BotTestData1))]
        public void Test1_NextMove(GameHistoryBL move1, GameHistoryDL move2, char[,] board)
        {
            var mock = new Mock<IGameServiceDL>();
            mock.Setup(cfg => cfg.SavePlayerMoveAsync(move2)).Returns(Task.FromResult(BL.Enum.CheckStateBL.None));

            var botService = GetService(mock);
            botService.Board = board;
            botService.GameHistoryBl = move1;
            botService.MakeNextMove(false);

            Assert.True(board == botService.Board);
        }
    }
}
