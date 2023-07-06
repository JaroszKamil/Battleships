using BattleShipsApi.Models.ShipModels;

namespace BattleShipsApi.Models
{
    public class GridCell<T>: GridCoordinates where T : class
    {
        public T CellContent {get;set;}

        public bool HasShoot(T cell)
        {
            if (cell is Ship ship)
            {
                ship.SizeOnGrid--;

                if(ship.SizeOnGrid == 0)
                {
                    ship.IsAlive = false;
                }

                return true;
            }

            return false;
        }
    }
}
