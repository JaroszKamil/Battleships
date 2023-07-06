using BattleShipsApi.Models.ShipModels;

namespace BattleShipsApi
{
    public static class ShipFactory
    {
        public static Ship GetShip(ShipTypes shipType)
        {
            return shipType switch
            {
                ShipTypes.Battleship => new Ship()
                {
                    ShipGuid = Guid.NewGuid(),
                    Type = shipType,
                    Size = 5

                },
                ShipTypes.Destroyers => new Ship()
                {
                    ShipGuid = Guid.NewGuid(),
                    Type = shipType,
                    Size = 4,
                },
                _ => throw new Exception("Wrong ship type")
            };
        }
    }
}
