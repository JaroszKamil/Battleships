namespace BattleShipsApi.Models.ShipModels
{
    public class Ship
    {
        public Guid ShipGuid { get; set; }
        public ShipTypes Type { get; set; }
        public int Size { get; set; }
        public int SizeOnGrid { get; set; }
        public bool IsAlive { get; set; } = true;
        public bool IsSetOnGrid { get; set; } = false;

    }
}
