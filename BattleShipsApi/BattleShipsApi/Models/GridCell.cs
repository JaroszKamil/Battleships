using BattleShipsApi.Models.ShipModels;

namespace BattleShipsApi.Models
{
    public class GridCell<T>: GridCoordinates where T : class
    {
        public T CellContent {get;set;}
    }
}
