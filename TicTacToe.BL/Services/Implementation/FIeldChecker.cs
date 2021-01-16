using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.BusinessComponent.Enum;
using TicTacToe.BusinessComponent.Models;
using TicTacToe.DataComponent.Models;
using TicTacToe.BusinessComponent.Extensions;

namespace TicTacToe.BusinessComponent.Services.Implementation
{
    public class FIeldChecker : IFieldChecker
    {
        private IEnumerable<Models.GameHistory> _gameHistory;
        private Models.GameHistory _nextMove;
        private Guid? _firstPlayerId;
        public char[,] Board { get; set; }
        public Models.GameHistory NextMove
        {
            set => _nextMove = value;
        }

        public void BoardInit(IEnumerable<Models.GameHistory> gameHistories)
        {
            this._gameHistory = gameHistories;
            Board = new char[IFieldChecker.BOARD_SIZE, IFieldChecker.BOARD_SIZE];

            if (_gameHistory.Any())
            {
                _firstPlayerId = gameHistories.First().PlayerId;
                foreach (var item in gameHistories)
                {
                    if (item.PlayerId == _firstPlayerId)
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
            if (_firstPlayerId.HasValue)
            {
                Board[_nextMove.YAxis, _nextMove.XAxis] = _firstPlayerId.Value == _nextMove.PlayerId ? 'X' : 'O';
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

        public bool GamePlayerCheck(Models.Game game)
        {
            if (_nextMove.IsBot)
            {
                return !game.IsPlayer2Bot;
            }

            if (game.Player2Id.HasValue)
            {
                return _nextMove.PlayerId != game.Player1Id && _nextMove.PlayerId != game.Player2Id.Value;
            }
            return _nextMove.PlayerId != game.Player1Id;
        }
    }
}
