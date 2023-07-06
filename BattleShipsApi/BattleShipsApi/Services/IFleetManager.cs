using BattleShipsApi.Models;

namespace BattleShipsApi.Services
{
    public interface IFleetManager
    {
        List<GridCoordinates> SetShipsOnOceanGrid(Fleet fleet);
        Fleet CreateFleet();
        bool Shoot();
    }
}
