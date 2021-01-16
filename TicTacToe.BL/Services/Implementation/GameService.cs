using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using TicTacToe.BusinessComponent.Extensions;
using TicTacToe.BusinessComponent.Models;
using TicTacToe.BusinessComponent.Enum;
using TicTacToe.BusinessComponent.Helpers;
using TicTacToe.DataComponent.Models;
using TicTacToe.DataComponent.Services;

namespace TicTacToe.BusinessComponent.Services.Implementation
{
    public class GameService : IGameService
    {
        private readonly DataComponent.Services.IGameService _gameService;
        private readonly IFieldChecker _fieldChecker;
        private readonly IBotService _botService;
        private readonly IStatisticService _statisticService;
        private readonly IMapper _mapper;

        public GameService(DataComponent.Services.IGameService gameService, IFieldChecker fieldChecker, IBotService botService, IStatisticService statisticService, IMapper mapper)
        {
            this._gameService = gameService;
            this._fieldChecker = fieldChecker;
            this._botService = botService;
            this._statisticService = statisticService;
            this._mapper = mapper;
        }
        public async Task<bool> CreateGameAsync(Models.Game game)
        {
            if (game.IsPlayer2Bot && game.Player2Id.HasValue)
            {
                return false;
            }

            try
            {
                await _gameService.CreateGameAsync(_mapper.Map<DataComponent.Models.Game>(game));
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<Models.Game> GetGameByGameIdAsync(Guid id)
        {
            var game = await _gameService.GetGameByGameIdAsync(id);
            return _mapper.Map<Models.Game>(game);
        }

        public async Task<IEnumerable<Models.Game>> GetGamesByUserAsync(Guid id)
        {
            var games = await _gameService.GetGamesByUserAsync(id);
            return games.Select(g => _mapper.Map<Models.Game>(g));
        }

        private DataComponent.Models.Game _game;

        private async Task WinnerLoser()
        {
            await SetGameAsFinished();
            var win = new WinHelper(_statisticService)
            {
                Board = _fieldChecker.Board,
                Game = _mapper.Map<Models.Game>(_game)
            };
            await win.FindWinnerAndLoser();
        }

        //TODO use cache or redis
        public async Task<CheckState> SavePlayerMoveAsync(Models.GameHistory newMove)
        {
            var gamHistory = await _gameService.GetGameHistoriesAsync(newMove.GameId);
            var history = gamHistory.Select(h => _mapper.Map<Models.GameHistory>(h));
            _game = await _gameService.GetGameByGameIdAsync(newMove.GameId);

            if (_game is null)
            {
                return CheckState.GameNotExist;
            }

            _fieldChecker.BoardInit(history);
            _fieldChecker.NextMove = newMove;

            var modelForSave = _mapper.Map<DataComponent.Models.GameHistory>(newMove);

            if (_fieldChecker.GamePlayerCheck(_mapper.Map<Models.Game>(_game)))
            {
                return CheckState.GamePlayerCheck;
            }

            if (_fieldChecker.LastPlayerCheck())
            {
                return CheckState.PreviousPlayerCheck;
            }

            if (_fieldChecker.EndGameCheck(_game.IsGameFinished))
            {
                return CheckState.EndGameCheck;
            }

            if (_fieldChecker.IndexCheck())
            {
                return CheckState.IndexCheck;
            }

            if (_fieldChecker.DoubleCellCheck())
            {
                return CheckState.DoubleCellCheck;
            }

            #region monad

            //var check = _fieldChecker.GamePlayerCheck(new GameBL
            //                        {
            //                            Player1Id = _game.Player1Id,
            //                            Player2Id = _game.Player2Id,
            //                            IsPlayer2Bot = _game.IsPlayer2Bot
            //                        })
            //    .Bind(gp => _fieldChecker.LastPlayerCheck()
            //        .Bind(lp => _fieldChecker.EndGameCheck(_game.IsGameFinished)
            //            .Bind(eg => _fieldChecker.IndexCheck()
            //                .Bind(ic => _fieldChecker.DoubleCellCheck()
            //                    .Bind(dc => _fieldChecker.LinesCheck(() => _gameService.SavePlayerMoveAsync(modelForSave))
            //                        .Bind(lc => _fieldChecker.DCheck(() => _gameService.SavePlayerMoveAsync(modelForSave))))))));

            //if (check.HasValue)
            //{
            //    return check.Value;
            //}

            #endregion

            if (!newMove.IsBot)
            {
                _fieldChecker.MakeMove();
                await _gameService.SavePlayerMoveAsync(modelForSave);

                if (_fieldChecker.LinesCheck())
                {
                    await WinnerLoser();
                    return CheckState.LineCheck;
                }

                if (_fieldChecker.DCheck())
                {
                    await WinnerLoser();
                    return CheckState.DiagonalCheck;
                }

                if (history.Count() + 1 > Math.Pow(IFieldChecker.BOARD_SIZE, 2))
                {
                    await SetGameAsFinished();
                    await _statisticService.SaveStatisticAsync(GameResultWithBot(_game.Player1Id));
                    if (!_game.IsPlayer2Bot)
                    {
                        await _statisticService.SaveStatisticAsync(GameResultWithBot(_game.Player2Id.Value));
                    }
                    return CheckState.EndGameCheck;
                }
                //TODO remove in future
                if (_game.IsPlayer2Bot)
                {
                    return await BotMove(history, newMove, false);
                }

                return CheckState.None;
            }
            else
            {
                return await BotMove(history, newMove, true);
            }
        }

        private async Task<CheckState> BotMove(IEnumerable<Models.GameHistory> history, Models.GameHistory newMove, bool isExternalBot)
        {
            _botService.Board = (char[,])_fieldChecker.Board.Clone();
            _botService.GameHistory = newMove;
            var result = _botService.MakeNextMove(isExternalBot);

            if (result != CheckState.None)
            {
                await SetGameAsFinished();
                await _statisticService.SaveStatisticAsync(new Models.GameResult
                {
                    Id = Guid.NewGuid(),
                    GameId = _game.GameId,
                    PlayerId = history.First().PlayerId.Value,
                    Result = ResultStatus.Lost
                });
                return result;
            }

            if (history.Count() + 2 > Math.Pow(IFieldChecker.BOARD_SIZE, 2))
            {
                await SetGameAsFinished();
                await _statisticService.SaveStatisticAsync(GameResultWithBot(_game.Player1Id));

                return CheckState.EndGameCheck;
            }
            return CheckState.None;
        }

        private Models.GameResult GameResultWithBot(Guid playerId)
        {
            return new Models.GameResult
            {
                Id = Guid.NewGuid(),
                GameId = _game.GameId,
                PlayerId = playerId,
                Result = ResultStatus.Draw
            };
        }

        private async Task SetGameAsFinished()
        {
            _game.IsGameFinished = true;
            await _gameService.SetGameAsFinishedAsync(_game);
        }

        public async Task<bool> SetGameAsFinished(Guid gameId)
        {
            try
            {
                var game = await _gameService.GetGameByGameIdAsync(gameId);
                game.IsGameFinished = true;
                await _gameService.SetGameAsFinishedAsync(game);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<IEnumerable<Models.Game>> GetAllGamesAsync()
        {
            var result = await _gameService.GetAllGamesAsync();
            return result.Select(r => _mapper.Map<Models.Game>(r));
        }
    }
}
