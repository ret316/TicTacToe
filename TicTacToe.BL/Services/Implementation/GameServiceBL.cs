using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.BL.Enum;
using TicTacToe.BL.Extensions;
using TicTacToe.BL.Models;
using TicTacToe.DL.Models;
using TicTacToe.DL.Services;

namespace TicTacToe.BL.Services.Implementation
{
    public class GameServiceBL : IGameServiceBL
    {
        private readonly IGameServiceDL _gameServiceDL;
        private readonly IFieldChecker _fieldChecker;
        private readonly IBotService _botService;
        private readonly IStatisticServiceBL _statisticServiceBL;
        public GameServiceBL(IGameServiceDL gameServiceDL, IFieldChecker fieldChecker, IBotService botService, IStatisticServiceBL statisticServiceBL)
        {
            this._gameServiceDL = gameServiceDL;
            this._fieldChecker = fieldChecker;
            this._botService = botService;
            this._statisticServiceBL = statisticServiceBL;
        }
        public async Task CreateGameAsync(GameBL game)
        {
            if (game.IsPlayer2Bot && !game.Player2Id.HasValue)
            {
                return;
            }

            await _gameServiceDL.CreateGameAsync(new GameDL
            {
                Id = Guid.NewGuid(),
                GameId = Guid.NewGuid(),
                Player1Id = game.Player1Id,
                Player2Id = game.Player2Id,
                IsPlayer2Bot = game.IsPlayer2Bot,
                IsGameFinished = false
            });
        }

        public async Task<GameBL> GetGameByGameIdAsync(Guid id)
        {
            var game = await _gameServiceDL.GetGameByGameIdAsync(id);
            return new GameBL
            {
                GameId = game.GameId,
                Player1Id = game.Player1Id,
                Player2Id = game.Player2Id,
                IsPlayer2Bot = game.IsPlayer2Bot,
                IsGameFinished = game.IsGameFinished
            };
        }

        public async Task<IEnumerable<GameBL>> GetGamesByUserAsync(Guid id)
        {
            var games = await _gameServiceDL.GetGamesByUserAsync(id);
            return games.Select(g => new GameBL
            {
                GameId = g.GameId,
                Player1Id = g.Player1Id,
                Player2Id = g.Player2Id,
                IsPlayer2Bot = g.IsPlayer2Bot,
                IsGameFinished = g.IsGameFinished
            });
        }

        public async Task<CheckStateBL> SavePlayerMoveAsync(GameHistoryBL historyBL)
        {


            var historyDL = await _gameServiceDL.GetGameHistoriesAsync(historyBL.GameId);
            var history = historyDL.Select(h => new GameHistoryBL
            {
                GameId = h.GameId,
                PlayerId = h.PlayerId,
                IsBot = h.IsBot,
                XAxis = h.XAxis,
                YAxis = h.YAxis,
                MoveDate = h.MoveDate
            });
            var game = await _gameServiceDL.GetGameByGameIdAsync(historyBL.GameId);

            Guid GetWinner()
            {
                var list = _fieldChecker.Board.Cast<char>();
                var x = list.Count(x => x == 'X');
                var y = list.Count(x => x == 'O');
                return x > y ? game.Player1Id : game.Player2Id.Value;
            }

            if (!historyBL.IsBot)
            {

                _fieldChecker.BoardInit(history);
                _fieldChecker.NextMove = historyBL;

                var modelForSave = new GameHistoryDL
                {
                    Id = Guid.NewGuid(),
                    GameId = historyBL.GameId,
                    PlayerId = historyBL.PlayerId,
                    IsBot = historyBL.IsBot,
                    XAxis = historyBL.XAxis,
                    YAxis = historyBL.YAxis,
                    MoveDate = historyBL.MoveDate
                };

                #region monad

                //var check = _fieldChecker.GamePlayerCheck(new GameBL
                //                        {
                //                            Player1Id = game.Player1Id,
                //                            Player2Id = game.Player2Id,
                //                            IsPlayer2Bot = game.IsPlayer2Bot
                //                        })
                //    .Bind(gp => _fieldChecker.LastPlayerCheck()
                //        .Bind(lp => _fieldChecker.EndGameCheck(game.IsGameFinished)
                //            .Bind(eg => _fieldChecker.IndexCheck()
                //                .Bind(ic => _fieldChecker.DoubleCellCheck()
                //                    .Bind(dc => _fieldChecker.LinesCheck(() => _gameServiceDL.SavePlayerMoveAsync(modelForSave))
                //                        .Bind(lc => _fieldChecker.DCheck(() => _gameServiceDL.SavePlayerMoveAsync(modelForSave))))))));

                //if (check.HasValue)
                //{
                //    return check.Value;
                //}

                #endregion

                if (_fieldChecker.GamePlayerCheck(new GameBL
                {
                    Player1Id = game.Player1Id,
                    Player2Id = game.Player2Id,
                    IsPlayer2Bot = game.IsPlayer2Bot
                }))
                {
                    return CheckStateBL.GamePlayerCheck;
                }

                if (_fieldChecker.LastPlayerCheck())
                {
                    return CheckStateBL.PreviousPlayerCheck;
                }

                if (_fieldChecker.EndGameCheck(game.IsGameFinished))
                {
                    return CheckStateBL.EndGameCheck;
                }

                if (_fieldChecker.IndexCheck())
                {
                    return CheckStateBL.IndexCheck;
                }

                if (_fieldChecker.DoubleCellCheck())
                {
                    return CheckStateBL.DoubleCellCheck;
                }

                _fieldChecker.MakeMove();
                await _gameServiceDL.SavePlayerMoveAsync(modelForSave);

                if (_fieldChecker.LinesCheck())
                {
                    await SetGameAsFinished(game);
                    var winner = GetWinner();
                    await SaveGameResult(new GameResultBL
                    {
                        Id = Guid.NewGuid(),
                        GameId = game.GameId,
                        PlayerId = winner,
                        Result = Models.ResultStatus.Won
                    });
                    await SaveGameResult(new GameResultBL
                    {
                        Id = Guid.NewGuid(),
                        GameId = game.GameId,
                        PlayerId = winner == game.Player1Id ? game.Player1Id : game.Player2Id.Value,
                        Result = Models.ResultStatus.Lost
                    });
                    return CheckStateBL.LineCheck;
                }

                if (_fieldChecker.DCheck())
                {
                    await SetGameAsFinished(game);
                    var winner = GetWinner();
                    await SaveGameResult(new GameResultBL
                    {
                        Id = Guid.NewGuid(),
                        GameId = game.GameId,
                        PlayerId = winner,
                        Result = Models.ResultStatus.Won
                    });
                    await SaveGameResult(new GameResultBL
                    {
                        Id = Guid.NewGuid(),
                        GameId = game.GameId,
                        PlayerId = winner == game.Player1Id ? game.Player1Id : game.Player2Id.Value,
                        Result = Models.ResultStatus.Lost
                    });
                    return CheckStateBL.DiagonalCheck;
                }


                if (history.Count() + 1 > (Math.Pow(IFieldChecker.BOARD_SIZE, 2) - 1))
                {
                    await SetGameAsFinished(game);
                    await _statisticServiceBL.SaveStatisticAsync(new GameResultBL
                    {
                        Id = Guid.NewGuid(),
                        GameId = game.GameId,
                        PlayerId = game.Player1Id,
                        Result = Models.ResultStatus.Draw
                    });
                    if (!game.IsPlayer2Bot)
                    {
                        await _statisticServiceBL.SaveStatisticAsync(new GameResultBL
                        {
                            Id = Guid.NewGuid(),
                            GameId = game.GameId,
                            PlayerId = game.Player2Id.Value,
                            Result = Models.ResultStatus.Draw
                        });

                    }
                    return CheckStateBL.EndGameCheck;
                }
            }
            else
            {
                _botService.Board = (char[,])_fieldChecker.Board.Clone();
                _botService.GameHistoryBl = historyBL;
                _botService.MakeNextMove();

                if (history.Count() + 2 > (Math.Pow(IFieldChecker.BOARD_SIZE, 2) - 1))
                {
                    await SetGameAsFinished(game);
                    await _statisticServiceBL.SaveStatisticAsync(new GameResultBL
                    {
                        Id = Guid.NewGuid(),
                        GameId = game.GameId,
                        PlayerId = game.Player1Id,
                        Result = Models.ResultStatus.Draw
                    });
                }
            }

            return CheckStateBL.None;
        }

        public async Task SetGameAsFinished(GameDL game)
        {
            await _gameServiceDL.SetGameAsFinisheed(game);
        }

        public async Task SetGameAsFinished(Guid gameId)
        {
            var game = await _gameServiceDL.GetGameByGameIdAsync(gameId);
            game.IsGameFinished = true;
            await _gameServiceDL.SetGameAsFinisheed(game);
        }


        public async Task SaveGameResult(GameResultBL gameResult)
        {
            await _statisticServiceBL.SaveStatisticAsync(gameResult);
        }
    }
}
