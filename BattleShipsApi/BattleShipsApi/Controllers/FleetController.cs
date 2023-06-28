using BattleShipsApi.Models;
using BattleShipsApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BattleShipsApi.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FleetController: ControllerBase
    {
        private readonly IFleetManager fleetManager;
        public FleetController(IFleetManager fleetManager)
        {
            this.fleetManager = fleetManager;
        }

        [HttpGet]
        public async Task<ActionResult<Fleet>> GetFleet()
        {
            // Call the QuizService to get the quizModel
            var fleet = fleetManager.GetFleet();

            return fleet;
        }
    }
}
