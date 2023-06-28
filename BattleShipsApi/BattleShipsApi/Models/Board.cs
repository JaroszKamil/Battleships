namespace BattleShipsApi.Models
{
    public class Board
    {
        public List<GridCell<GridCoordinates>> OceanGrid { get; set; }
        public List<GridCell<GridCoordinates>> TargetGrid { get; set; }
        public Fleet PlayerFleet { get; set; }
    }
}
