using BattleShipsApi.Models;
using BattleShipsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BattleShipsApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BoardController: ControllerBase
    {
        private readonly IFleetManager fleetManager;
        public BoardController(IFleetManager fleetManager)
        {
            this.fleetManager = fleetManager;
        }

        [HttpGet]
        public async Task<ActionResult<Board>> StartGame()
        {
            // Call the QuizService to get the quizModel
            var board = fleetManager.SetFleetOnGrid();

            return board;
        }
    }
}
