using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.BL.Enum;
using TicTacToe.BL.Models;
using TicTacToe.DL.Models;
using TicTacToe.BL.Extensions;

namespace TicTacToe.BL.Services.Implementation
{
    public class FIeldChecker : IFieldChecker
    {
        private IEnumerable<GameHistoryBL> _gameHistory;
        private GameHistoryBL _nextMove;
        private Guid? firstPlayerId;

        public char[,] Board { get; set; }
        public GameHistoryBL NextMove
        {
            set => _nextMove = value;
        }

        public void BoardInit(IEnumerable<GameHistoryBL> gameHistories)
        {
            this._gameHistory = gameHistories;
            Board = new char[IFieldChecker.BOARD_SIZE, IFieldChecker.BOARD_SIZE];

            if (_gameHistory.Any())
            {
                firstPlayerId = gameHistories.First().PlayerId;
                foreach (var item in gameHistories)
                {
                    if (item.PlayerId == firstPlayerId)
                    {
                        Board[item.YAxis, item.XAxis] = 'X';
                    }
                    else
                    {
                        Board[item.YAxis, item.XAxis] = 'O';
                    }
                }
            }
        }
        public bool DCheck()
        {
            return Board.CheckDiagonals();
        }

        public bool DoubleCellCheck()
        {
            return Board[_nextMove.YAxis, _nextMove.XAxis] != '\0';
        }

        public bool EndGameCheck(bool game)
        {
            return game || _gameHistory.Count() >= Math.Pow(IFieldChecker.BOARD_SIZE, 2);
        }

        public bool IndexCheck()
        {
            return _nextMove.XAxis > IFieldChecker.BOARD_SIZE - 1 || _nextMove.YAxis > IFieldChecker.BOARD_SIZE - 1;
        }

        public bool LinesCheck()
        {

            for (int i = 0; i < IFieldChecker.BOARD_SIZE; i++)
            {
                if (Board.CheckRow(i) || Board.CheckColumn(i))
                {
                    return true;
                }
            }

            return false;
        }

        public void MakeMove()
        {
            if (firstPlayerId.HasValue)
            {
                Board[_nextMove.YAxis, _nextMove.XAxis] = firstPlayerId.Value == _nextMove.PlayerId ? 'X' : 'O';
            }
            else
            {
                Board[_nextMove.YAxis, _nextMove.XAxis] = 'X';
            }
        }

        public bool LastPlayerCheck()
        {
            if (_gameHistory.Any())
            {
                var last = _gameHistory.Last();
                if (last.PlayerId == _nextMove.PlayerId)
                {
                    return true;
                }

                if (last.IsBot == _nextMove.IsBot)
                {
                    if (last.IsBot)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool GamePlayerCheck(GameBL game)
        {
            if (_nextMove.IsBot)
            {
                return !game.IsPlayer2Bot;
            }

            return _nextMove.PlayerId != game.Player1Id && _nextMove.PlayerId != game.Player2Id.Value;
        }
    }
}
