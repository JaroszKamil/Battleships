namespace BattleShipsApi.Models
{
    public class GridCoordinates
    {
        public CellStatusEnum CellStatus { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
    }
}
