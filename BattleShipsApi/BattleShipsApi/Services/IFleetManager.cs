using BattleShipsApi.Models;

namespace BattleShipsApi.Services
{
    public interface IFleetManager
    {
        Board SetFleetOnGrid();
        Fleet GetFleet();
        bool Shoot();
    }
}
