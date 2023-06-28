namespace BattleShipsApi.Models
{
    public class GridCell<T> where T : class
    {
        public bool IsShooted(T cell)
        {
            if (cell is ShipCell)
            {
                return true;
            }
            
            return false;
        }
    }
}
