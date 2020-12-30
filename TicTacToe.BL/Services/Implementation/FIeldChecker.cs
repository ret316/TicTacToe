using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TicTacToe.BL.Enum;
using TicTacToe.BL.Models;
using TicTacToe.DL.Models;

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
                        Board[item.XAxis, item.YAxis] = 'X';
                    }
                    else
                    {
                        Board[item.XAxis, item.YAxis] = 'O';
                    }
                }

            }
        }
        public bool DCheck()
        {
            bool oneToNine = false;
            bool threeToSeven = false;
            for (int i = 0, j = IFieldChecker.BOARD_SIZE - 1; i < IFieldChecker.BOARD_SIZE; i++, j--)
            {
                if (i > 0)
                {
                    oneToNine = CompareChar(Board[i, i], Board[i - 1, i - 1]);
                    threeToSeven = CompareChar(Board[j, i], Board[i - 1, j + 1]);
                }
            }

            return oneToNine || threeToSeven;
        }

        public bool DoubleCellCheck()
        {
            return Board[_nextMove.XAxis, _nextMove.YAxis] != '\0';
        }

        public bool EndGameCheck(bool game)
        {
            return game || _gameHistory.Count() > Math.Pow(IFieldChecker.BOARD_SIZE, 2);
        }

        public bool IndexCheck()
        {
            return _nextMove.XAxis > IFieldChecker.BOARD_SIZE - 1 || _nextMove.YAxis > IFieldChecker.BOARD_SIZE - 1;
        }

        public bool LinesCheck()
        {
            bool xLine = false;
            bool yLine = false;

            for (int i = 0; i < IFieldChecker.BOARD_SIZE; i++)
            {
                for (int j = 0; j < IFieldChecker.BOARD_SIZE; j++)
                {
                    if (j > 0)
                    {
                        yLine = CompareChar(Board[j, i], Board[j - 1, i]);
                    }

                    if (i > 0)
                    {
                        xLine = CompareChar(Board[i, j], Board[i - 1, j]);
                    }
                }

                if (xLine || yLine)
                {
                    break;
                }
            }

            return xLine || yLine;
        }

        private bool CompareChar(char current, char previous)
        {
            if (previous == '\0')
            {
                return false;
            }

            return current == previous;
        }

        public void MakeMove()
        {
            if (firstPlayerId.HasValue)
            {
                Board[_nextMove.XAxis, _nextMove.YAxis] = firstPlayerId.Value == _nextMove.PlayerId ? 'X' : 'O';
            }
            else
            {
                Board[_nextMove.XAxis, _nextMove.YAxis] = 'X';
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
                return game.IsPlayer2Bot;
            }

            return _nextMove.PlayerId != game.Player1Id && _nextMove.PlayerId != game.Player2Id.Value;
        }
    }
}
