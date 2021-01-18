using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using TicTacToe.Bot.BusinessComponent.Models;

namespace TicTacToe.Bot.BusinessComponent.Services.Implementation
{
    public class BotService : IBotService
    {
        private readonly HttpClient _httpClient;
        private GameState _gameState;

        public BotService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public void CalculateMove(GameState gameState)
        {
            _gameState = gameState;
            GetNextMove();
        }

        public void GetNextMove()
        {
            Random rnd = new Random();
            int xAxis; int yAxis;

            while (true)
            {
                xAxis = rnd.Next(0, _gameState.Board.GetLength(0));
                yAxis = rnd.Next(0, _gameState.Board.GetLength(1));
                if (_gameState.Board[yAxis, xAxis] == '\0')
                {
                    break;
                }
            }

            SendNextMove(xAxis, yAxis);
        }

        /// <summary>
        /// Method for sending coordinates to web api
        /// </summary>
        /// <param name="xAxis">x</param>
        /// <param name="yAxis">y</param>
        public void SendNextMove(int xAxis, int yAxis)
        {
            var move = new
            {
                GameId = _gameState.GameId,
                PlayerId = _gameState.Player2Id,
                XAxis = xAxis,
                YAxis = yAxis,
                MoveDate = DateTime.Now
            };
            var json = JsonConvert.SerializeObject(move);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", GetToken());
            var result = _httpClient.PostAsync("api/games/move", content).Result;
        }

        /// <summary>
        /// Get token for bot
        /// </summary>
        /// <returns></returns>
        private string GetToken()
        {
            var user = new { Email = "bot1@gmail.com", Password = "123456" };
            var json = JsonConvert.SerializeObject(user);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var res = _httpClient.PostAsync("api/users/authenticate", content).Result;
            var d = res.Content.ReadAsStringAsync().Result;
            var r1 = JsonConvert.DeserializeObject<Auth>(d);
            return r1.Token;
        }
    }
}
