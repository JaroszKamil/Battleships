using BattleShipsApi.Models;

namespace BattleShipsApi.Services
{
    public interface IGameplayManager
    {
        Board PrepareTheBoard();
    }
}
