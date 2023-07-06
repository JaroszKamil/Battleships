using BattleShipsApi.Models;
using BattleShipsApi.Models.ShipModels;

namespace BattleShipsApi.Services
{
    public class FleetManager : IFleetManager
    {
        private readonly Random random = new Random();

        public List<GridCoordinates> SetShipsOnOceanGrid(Fleet fleet, List<GridCoordinates> oceanCells)
        {
            if (fleet.Ships == null || fleet.Ships.Count == 0)
            {
                throw new Exception("There are no ships in the fleet.");
            }

            foreach (var ship in fleet.Ships)
            {
                do
                {
                    SetShipOnGrid(ship, oceanCells);

                } while (ship.IsSetOnGrid != true);
            }

            return oceanCells;
        }


        public Fleet CreateFleet()
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

        public List<List<GridCoordinates>> CheckPossibleShipPlaces<T>(List<GridCoordinates> oceanGrid, GridCoordinates cell, int shipSize)
        {
            var possiblePlaces = new List<List<GridCoordinates>>();
            var directions = new List<Predicate<GridCoordinates>>
            {
                x => x.Column >= cell.Column && x.Column < cell.Column + shipSize && cell.Row == x.Row, // right
                x => x.Column <= cell.Column && x.Column > cell.Column - shipSize && cell.Row == x.Row, // left
                x => x.Row >= cell.Row && x.Row < cell.Row + shipSize && cell.Column == x.Column, // up
                x => x.Row <= cell.Row && x.Row > cell.Row - shipSize && cell.Column == x.Column, // down
            };

            foreach (var direction in directions)
            {
                var cells = GetCoordinates<T>(oceanGrid, shipSize, direction);

                if (cells != null)
                {
                    possiblePlaces.Add(cells);
                }
            }

            return possiblePlaces;
        }

        private List<GridCoordinates>? GetCoordinates<T>(List<GridCoordinates> oceanGrid, int shipSize, Predicate<GridCoordinates> condition)
        {
            var cells = oceanGrid.FindAll(condition);

            if (cells.Any(x => x is T || cells.Count() != shipSize))
            {
                return null;
            }

            else return cells;
        }

        private List<GridCoordinates> SetShipOnGrid(Ship ship, List<GridCoordinates> oceanCells)
        {
            while (!ship.IsSetOnGrid)
            {
                var randomCell = oceanCells[random.Next(oceanCells.Count)];

                var possiblePlacesToSetShip = CheckPossibleShipPlaces<GridCell<Ship>>(oceanCells, randomCell, ship.Size);

                if (possiblePlacesToSetShip.Any())
                {
                    var randomPosition = possiblePlacesToSetShip[random.Next(possiblePlacesToSetShip.Count())];

                    UpdateOceanCellsWithShip(randomPosition, oceanCells, ship);

                    ship.IsSetOnGrid = true;
                    ship.SizeOnGrid = ship.Size;
                }
            }

            return oceanCells;
        }

        private void UpdateOceanCellsWithShip(List<GridCoordinates> shipPosition, List<GridCoordinates> oceanCells, Ship ship)
        {
            foreach (var cell in shipPosition)
            {
                var gridIndex = oceanCells.IndexOf(oceanCells.First(x => x.Column == cell.Column && x.Row == cell.Row));

                oceanCells[gridIndex] = new GridCell<Ship>()
                {
                    CellContent = ship,
                    Column = cell.Column,
                    Row = cell.Row,
                    OcenCellStatus = CellStatusEnum.ship,
                };
            }
        }
    }
}