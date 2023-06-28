namespace BattleShipsApi.Models
{
    public class Ship
    {
        public int  ShipId { get; set; }
        public ShipTypes Type { get; set; }
        public int Size { get; set; }
        public List<ShipCell> ShipCells { get; set; } = new List<ShipCell>();
        public bool IsAlive { get; set; } = true;
        public bool IsSetOnGrid { get; set; } = false;
    }
}
