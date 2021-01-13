using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TicTacToe.BL.Models;
using TicTacToe.DL.Models;

namespace TicTacToe.Tests.TestData.Game
{
    public class GameTestData5 : IEnumerable<object[]>
    {
        private static Guid Id = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc09");
        private static Guid GameId = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc10");
        private static Guid PlayerId1 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc11");
        private static Guid PlayerId2 = Guid.Parse("4c9b3c40-374f-4b67-8c7e-19565107cc12");
        private static DateTime date = DateTime.Parse("2020-10-10");

        private IEnumerable<GameHistoryDL> gde1 = new List<GameHistoryDL>();

        GameDL gd0 = new GameDL
        {
            Id = Id,
            GameId = GameId,
            Player1Id = PlayerId1,
            Player2Id = null,
            IsPlayer2Bot = true,
            IsGameFinished = false
        };

        static GameHistoryDL ghd0 = new GameHistoryDL
        {
            Id = Id,
            GameId = GameId,
            PlayerId = PlayerId1,
            IsBot = false,
            XAxis = 1,
            YAxis = 1,
            MoveDate = date
        };
        GameResultBL grb0 = new GameResultBL
        {
            Id = Id,
            GameId = GameId,
            PlayerId = PlayerId1,
            Result = BL.Models.ResultStatus.Draw
        };

        GameHistoryBL ghb0 = new GameHistoryBL
        {
            GameId = GameId,
            PlayerId = PlayerId1,
            IsBot = false,
            XAxis = 1,
            YAxis = 1,
            MoveDate = date
        };



        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {GameId, gd0, gde1, ghd0, ghb0, grb0};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
