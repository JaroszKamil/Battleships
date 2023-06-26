namespace BattleShipsApi.Models
{
    public class Ship
    {
        public ShipTypes Type { get; set; }
        public int Size { get; set; }
        public int Lives { get; set; }
        public bool IsAlive { get; set; }
    }
}
