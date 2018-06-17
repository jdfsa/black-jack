using Jdfs.BlackJack.Api.Business;
using Jdfs.BlackJack.Api.Model;
using Jdfs.BlackJack.Api.Util;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Jdfs.BlackJack.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    public class GameController : Controller
    {
        private GameFlow game;

        /// <summary>
        /// Constructor
        /// </summary>
        public GameController()
        {
            this.game = new GameFlow(new GameRandomEngine());
        }

        /// <summary>
        /// Inicialize game
        /// </summary>
        /// <returns>Game response</returns>
        [HttpPost]
        [ActionName("initialize")]
        public GameResponse Inicialize()
        {
            var data = this.game.Initialize();
            return new GameResponse
            {
                Token = Crypto.Encrypt(JsonConvert.SerializeObject(data)),
                Data = this.game.GetclientView(data)
            };
        }

        /// <summary>
        /// Perform a hit action
        /// </summary>
        /// <param name="token">Required token which has the game state data</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("hit")]
        public GameResponse Hit([Required][FromHeader(Name = "X-Token")]string token)
        {
            token = Crypto.Decrypt(token);
            var data = JsonConvert.DeserializeObject<GameData>(token);
            game.Hit(data);
            return new GameResponse
            {
                Token = Crypto.Encrypt(JsonConvert.SerializeObject(data)),
                Data = this.game.GetclientView(data)
            };
        }

        /// <summary>
        /// Perform a stand action
        /// </summary>
        /// <param name="token">Required token chich has the game state data</param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("stand")]
        public GameResponse Stand([Required][FromHeader(Name = "X-Token")]string token)
        {
            token = Crypto.Decrypt(token);
            var data = JsonConvert.DeserializeObject<GameData>(token);
            game.Stand(data);
            return new GameResponse
            {
                Token = Crypto.Encrypt(JsonConvert.SerializeObject(data)),
                Data = this.game.GetclientView(data)
            };
        }
    }
}
