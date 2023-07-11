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
        public ActionResult<Board> StartGame()
        {
            var board = gameplay.PrepareTheBoard();

            return board;
        }

        [HttpPut("shoot/player")]
        public ActionResult<GridCoordinates> PlayerShoot(GridCoordinates cell)
        {
            var board = gameplay.PlayerShoot(cell);

            return board;
        }


        [HttpPut("shoot/computer")]
        public ActionResult<GridCoordinates> ComputerShoot()
        {
            var board = gameplay.ComputerShoot();

            return board;
        }
    }
}
