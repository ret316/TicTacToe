using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;
        public GameServiceBL(IGameServiceDL gameServiceDL, IFieldChecker fieldChecker, IBotService botService, IStatisticServiceBL statisticServiceBL, IMapper mapper)
        {
            this._gameServiceDL = gameServiceDL;
            this._fieldChecker = fieldChecker;
            this._botService = botService;
            this._statisticServiceBL = statisticServiceBL;
            this._mapper = mapper;
        }
        public async Task<bool> CreateGameAsync(GameBL game)
        {
            if (game.IsPlayer2Bot && !game.Player2Id.HasValue)
            {
                return false;
            }

            try
            {
                await _gameServiceDL.CreateGameAsync(_mapper.Map<GameDL>(game));
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<GameBL> GetGameByGameIdAsync(Guid id)
        {
            var game = await _gameServiceDL.GetGameByGameIdAsync(id);
            return _mapper.Map<GameBL>(game);
        }

        public async Task<IEnumerable<GameBL>> GetGamesByUserAsync(Guid id)
        {
            var games = await _gameServiceDL.GetGamesByUserAsync(id);
            return games.Select(g => _mapper.Map<GameBL>(g));
        }

        private GameDL game;

        private Guid GetWinner()
        {
            var list = _fieldChecker.Board.Cast<char>();
            var x = list.Count(x => x == 'X');
            var y = list.Count(x => x == 'O');
            return x > y ? game.Player1Id : game.Player2Id.Value;
        }

        private async Task WinnerLoser()
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
        }

        public async Task<CheckStateBL> SavePlayerMoveAsync(GameHistoryBL historyBL)
        {
            var historyDL = await _gameServiceDL.GetGameHistoriesAsync(historyBL.GameId);
            var history = historyDL.Select(h => _mapper.Map<GameHistoryBL>(h));
            game = await _gameServiceDL.GetGameByGameIdAsync(historyBL.GameId);

            if (!historyBL.IsBot)
            {
                _fieldChecker.BoardInit(history);
                _fieldChecker.NextMove = historyBL;

                var modelForSave = _mapper.Map<GameHistoryDL>(historyBL);

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

                if (_fieldChecker.GamePlayerCheck(_mapper.Map<GameBL>(game)))
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
                    await WinnerLoser();
                    return CheckStateBL.LineCheck;
                }

                if (_fieldChecker.DCheck())
                {
                    await WinnerLoser();
                    return CheckStateBL.DiagonalCheck;
                }

                if (history.Count() + 1 > (Math.Pow(IFieldChecker.BOARD_SIZE, 2) - 1))
                {
                    await SetGameAsFinished(game);
                    await _statisticServiceBL.SaveStatisticAsync(GameWithBot(game.Player1Id));
                    if (!game.IsPlayer2Bot)
                    {
                        await _statisticServiceBL.SaveStatisticAsync(GameWithBot(game.Player2Id.Value));
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
                    await _statisticServiceBL.SaveStatisticAsync(GameWithBot(game.Player1Id));
                }
            }

            return CheckStateBL.None;
        }

        private GameResultBL GameWithBot(Guid playerId)
        {
            return new GameResultBL
            {
                Id = Guid.NewGuid(),
                GameId = game.GameId,
                PlayerId = playerId,
                Result = Models.ResultStatus.Draw
            };
        }

        public async Task SetGameAsFinished(GameDL game)
        {
            await _gameServiceDL.SetGameAsFinished(game);
        }

        public async Task SetGameAsFinished(Guid gameId)
        {
            var game = await _gameServiceDL.GetGameByGameIdAsync(gameId);
            game.IsGameFinished = true;
            await _gameServiceDL.SetGameAsFinished(game);
        }

        public async Task SaveGameResult(GameResultBL gameResult)
        {
            await _statisticServiceBL.SaveStatisticAsync(gameResult);
        }
    }
}
