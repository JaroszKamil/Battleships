using BattleShipsApi.Models;
using BattleShipsApi.Models.ShipModels;
using System.Net;

namespace BattleShipsApi.Services
{
    public class FleetManager : IFleetManager
    {
        public bool Shoot()
        {
            throw new NotImplementedException();
        }

        public List<GridCoordinates> SetShipsOnOceanGrid(Fleet fleet )
        {
            if (fleet.Ships == null || fleet.Ships.Count == 0)
            {
                throw new Exception("There are no ships in the fleet.");
            }

            List<GridCoordinates> oceanCells = MakeEmptOcean();


            foreach (var ship in fleet.Ships)
            {
                do
                {
                  var ble = SetShipOnGrid(ship, oceanCells);

                } while (ship.IsSetOnGrid != true);
            }

            GridCell<Ship> shipCell = new GridCell<Ship>();

            oceanCells.Add(shipCell);


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


        private List<GridCoordinates> MakeEmptOcean()
        {
            var oceanGrid = new List<GridCoordinates>();
            int index = 0;
            do
            {
                oceanGrid.Add(new GridCell<OceanCell>()
                {
                    Row = index,
                    Column = index
                });

                if(index == 10)
                {
                    index = 0;
                }

                index++;
            } while (oceanGrid.Count() != 100);

            return oceanGrid;
        }

        private bool CanPlaceShip(List<GridCoordinates> oceanGrid, GridCoordinates cell, int shipSize)
        {
            int minColumn = cell.Column - shipSize;
            int maxColumn = cell.Column + shipSize;
            int minRow = cell.Row - shipSize;
            int maxRow = cell.Row + shipSize;

            // Check if any of the cells required for the ship placement are out of bounds
            if (minColumn < 0 || maxColumn > 9 || minRow < 0 || maxRow > 9)
            {
                return false;
            }

            for (int column = minColumn; column <= maxColumn; column++)
            {
                for (int row = minRow; row <= maxRow; row++)
                {
                    var targetCell = oceanGrid.Find(x => x.Column == column && x.Row == row);

                    if (!(targetCell is OceanCell))
                    {
                        return false;
                    }
                }
            }
        }

        private List<GridCoordinates> SetShipOnGrid(Ship ship, List<GridCoordinates> oceanCells)
        {
            var randomCell = oceanCells[new Random().Next(oceanCells.Count)];


            if(CanPlaceShip(oceanCells, randomCell, ship.Size))
            {
                List<GridCell<Ship>> shipCoordinates = new List<GridCell<Ship>>();

                oceanCells.Add(shipCoordinates[0]);

            }



            //Random random = new Random();
            //bool isVertical = random.Next(2) == 0;

            //string[] rows = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J" };
            //int row = Array.IndexOf(rows, cell.Row);  // Get the index of the row in the array
            //int column = cell.Column - 1;  // Adjust the column number to a zero-based index

            //List<GridCoordinates> shipCoordinates = new List<GridCoordinates>();

            //for (int i = 0; i < ship.SizeOnGrid; i++)
            //{
            //    int currentRow = row;
            //    int currentColumn = column;

            //    if (isVertical)
            //    {
            //        currentRow += i;
            //    }
            //    else
            //    {
            //        currentColumn += i;
            //    }

            //    var coordinates = new GridCoordinates
            //    {
            //        Row = rows[currentRow],
            //        Column = currentColumn + 1
            //    };

            //    shipCoordinates.Add(new GridCell<Ship>
            //    {
            //        CellContent = ship,
            //        Row = coordinates.Row,
            //        Column = coordinates.Column
            //    });
            //}

            //ship.IsSetOnGrid = true;

            return null;
        }


    }
}