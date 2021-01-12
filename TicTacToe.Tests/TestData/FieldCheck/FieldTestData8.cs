﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TicTacToe.BL.Models;

namespace TicTacToe.Tests.TestData.FieldCheck
{
    public class FieldTestData8 : IEnumerable<object[]>
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

        GameHistoryBL bh2 = new GameHistoryBL
        {
            PlayerId = Guid.NewGuid(),
            GameId = GameId,
            IsBot = false,
            XAxis = 5,
            YAxis = 5,
            MoveDate = DateTime.Parse("2020-10-10")
        };
        GameBL bg0 = new GameBL
        {
            Id = Id,
            GameId = GameId,
            Player1Id = PlayerId1,
            Player2Id = PlayerId2,
            IsPlayer2Bot = false,
            IsGameFinished = false
        };
        GameHistoryBL bh3 = new GameHistoryBL
        {
            PlayerId = null,
            GameId = GameId,
            IsBot = true,
            XAxis = 2,
            YAxis = 2,
            MoveDate = DateTime.Parse("2020-10-10")
        };
        GameBL bg1 = new GameBL
        {
            Id = Id,
            GameId = GameId,
            Player1Id = PlayerId1,
            Player2Id = null,
            IsPlayer2Bot = true,
            IsGameFinished = false
        };

        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] {bgh0, bh2, bh3, bg0, bg1};
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
