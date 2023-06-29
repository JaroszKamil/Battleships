using BattleShipsApi.Models;
using BattleShipsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BattleShipsApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GameplayController: ControllerBase
    {
        private readonly IGameplayManager gameplay;
        public GameplayController(IGameplayManager gameplay)
        {
            this.gameplay = gameplay;
        }

        [HttpGet]
        public async Task<ActionResult<Board>> StartGame()
        {
            var board = gameplay.PrepareTheBoard();

            return board;
        }
    }
}
