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
using Xunit;

namespace TicTacToe.Tests.UnitTests
{
    public class BotServiceTests
    {

        private static Guid GameId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc10");
        private static Guid PlayerId1 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc11");

        public IBotService GetService(Mock<IGameServiceDL> mock)
        {
            IFieldChecker fieldChecker = new FIeldChecker();

            return new BotService(fieldChecker, mock.Object);
        }

        [Fact]
        public void NextMoveTest()
        {
            var user = new GameHistoryDL
            {
                PlayerId = PlayerId1,
                GameId = GameId,
                IsBot = false,
                XAxis = 1,
                YAxis = 1,
                MoveDate = DateTime.Parse("2020-10-10")
            };
            var user2 = new GameHistoryBL
            {
                PlayerId = PlayerId1,
                GameId = GameId,
                IsBot = false,
                XAxis = 1,
                YAxis = 1,
                MoveDate = DateTime.Parse("2020-10-10")
            };

            var mock = new Mock<IGameServiceDL>();
            mock.Setup(cfg => cfg.SavePlayerMoveAsync(user)).Returns(Task.FromResult(BL.Enum.CheckStateBL.None));

            var botService = GetService(mock);
            var b0 = new char[3, 3]
            {
                {'\0', '\0', '\0'}, {'\0', 'X', '\0'}, {'\0', '\0', '\0'}
            };
            botService.Board = b0;
            botService.GameHistoryBl = user2;
            botService.MakeNextMove(false);
            Assert.True(b0 == botService.Board);
        }
    }
}
