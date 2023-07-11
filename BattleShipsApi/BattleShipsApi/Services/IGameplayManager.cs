using BattleShipsApi.Models;

namespace BattleShipsApi.Services
{
    public interface IGameplayManager
    {
        Board PrepareTheBoard();
        GridCoordinates PlayerShoot(GridCoordinates cell);
        GridCoordinates ComputerShoot(GridCoordinates? cell = null);
        List<GridCoordinates> MakeEmptOcean();
    }
}
