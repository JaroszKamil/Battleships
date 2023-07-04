using BattleShipsApi.Models;

namespace BattleShipsApi.Services
{
    public interface IFleetManager
    {
        List<GridCoordinates> SetShipsOnOceanGrid(Fleet fleet, List<GridCoordinates> oceanCells);
        Fleet CreateFleet();
    }
}
