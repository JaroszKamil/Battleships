using BattleShipsApi.Models;

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
                    Type = shipType,
                    Size = 5,
                    Lives = 5,

                },
                ShipTypes.Destroyers => new Ship()
                {
                    Type = shipType,
                    Size = 4,
                    Lives = 4,
                },
                _ => throw new NotImplementedException()
            };
        }
    }
}
