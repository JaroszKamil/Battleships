using BattleShipsApi.Models.ShipModels;

namespace BattleShipsApi.Models
{
    public class Fleet
    {
        public List<Ship>? Ships { get; set; }
        public int AvalibleShootsPerTurn { get; set; } = 1;
        public Dictionary<ShipTypes, int> MaxAllowedPerShipType { get; set; }

        public Fleet()
        {
            MaxAllowedPerShipType = new Dictionary<ShipTypes, int>
            {
                { ShipTypes.Battleship, 1 },
                { ShipTypes.Destroyers, 2 }
            };
        }
    }
}
