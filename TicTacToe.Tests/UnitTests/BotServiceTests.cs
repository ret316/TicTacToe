using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Moq;
using TicTacToe.BusinessComponent.Models;
using TicTacToe.DataComponent.Models;
using TicTacToe.BusinessComponent.Services;
using TicTacToe.BusinessComponent.Services.Implementation;
using TicTacToe.DataComponent.Services;
using TicTacToe.Tests.TestData.Bot;
using Xunit;

namespace TicTacToe.Tests.UnitTests
{
    public class BotServiceTests
    {
        public IBotService GetService(Mock<DataComponent.Services.IGameService> mock)
        {
            IFieldChecker fieldChecker = new FIeldChecker();

            return new BotService(fieldChecker, mock.Object);
        }

        [Theory]
        [ClassData(typeof(BotTestData1))]
        public void Test1_NextMove(BusinessComponent.Models.GameHistory move1, DataComponent.Models.GameHistory move2, char[,] board)
        {
            var mock = new Mock<DataComponent.Services.IGameService>();
            mock.Setup(cfg => cfg.SavePlayerMoveAsync(move2)).Returns(Task.FromResult(BusinessComponent.Enum.CheckState.None));

            var botService = GetService(mock);
            botService.Board = board;
            botService.GameHistory = move1;
            botService.MakeNextMove(false);

            Assert.True(board == botService.Board);
        }
    }
}
