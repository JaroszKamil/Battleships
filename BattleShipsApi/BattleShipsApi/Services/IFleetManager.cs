using BattleShipsApi.Models;

namespace BattleShipsApi.Services
{
    public interface IFleetManager
    {
        List<GridCoordinates> SetShipsOnOceanGrid(Fleet fleet, List<GridCoordinates> oceanCells);
        Fleet CreateFleet();
        List<List<GridCoordinates>> CheckPossibleShipPlaces<T>(List<GridCoordinates> oceanGrid, GridCoordinates cell, int shipSize);
    }
}
