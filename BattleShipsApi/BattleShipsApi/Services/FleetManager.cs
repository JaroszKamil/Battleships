using BattleShipsApi.Models;

namespace BattleShipsApi.Services
{
    public class FleetManager : IFleetManager
    {
        private Board board;
        public FleetManager(Board board) 
        {
            this.board = board;
        }
        public Board SetFleetOnGrid()
        {
            var playerFleet = CreateFleet();
            this.board.PlayerFleet = playerFleet;
            return board;
        }

        public bool Shoot()
        {
            throw new NotImplementedException();
        }

        private void SetShipsOnOceanGrid()
        {

            //this.board.OceanGrid.Add
        }

        private Fleet CreateFleet()
        {
            var fleet = new Fleet();
            fleet.Ships = new List<Ship>();

            foreach (var entry in fleet.MaxAllowedPerShipType)
            {
                var shipsToAdd = Enumerable.Range(0, entry.Value)
                                  .Select(_ => ShipFactory.GetShip(entry.Key));

                fleet.Ships.AddRange(shipsToAdd);
            }

            return fleet;
        }

        public Fleet GetFleet()
        {
            return this.board.PlayerFleet;
        }
    }
}