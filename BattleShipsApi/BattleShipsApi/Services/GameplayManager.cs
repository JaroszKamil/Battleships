using BattleShipsApi.Models;

namespace BattleShipsApi.Services
{
    public class GameplayManager : IGameplayManager
    {
        IFleetManager fleetManager;
        Board board;
        public GameplayManager(IFleetManager fleetManager, Board board)
        {
            this.fleetManager = fleetManager;
            this.board = board;
        }

        public Board PrepareTheBoard()
        {
            var fleet = this.fleetManager.CreateFleet();

            this.board.OceanGrid = this.fleetManager.SetShipsOnOceanGrid(fleet);

           

            return this.board;
        }
    }
}
