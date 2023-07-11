using BattleShipsApi.Models;
using BattleShipsApi.Models.ShipModels;

namespace BattleShipsApi.Services
{
    public class GameplayManager : IGameplayManager
    {
        IFleetManager fleetManager;
        Board board;
        Random random= new Random();
        public GameplayManager(IFleetManager fleetManager, Board board)
        {
            this.fleetManager = fleetManager;
            this.board = board;
        }

        public GridCoordinates ComputerShoot(GridCoordinates? coordinates = null)
        {

            if (board.ComputerPossibleTargets?.Count > 0)
            {
                var index = random.Next(this.board.ComputerPossibleTargets.Count);
                coordinates = this.board.ComputerPossibleTargets[index];
            }
            else if(coordinates== null)
            {
                var index = random.Next(this.board.OceanGrid.Count);
                coordinates = this.board.OceanGrid[index];
            }

            var result = Shoot(coordinates, this.board.OceanGrid);

            if (result.CellStatus == CellStatusEnum.hits)
            {
                var ship = (result as GridCell<Ship>).CellContent as Ship;

                var possiblePlacesOfShip = fleetManager.CheckPossibleShipPlaces<GridCell<GridCoordinates>>(this.board.OceanGrid, coordinates, ship.Size);

                if(possiblePlacesOfShip != null && possiblePlacesOfShip.Count() > 0)
                {
                    var randomPosition = possiblePlacesOfShip[random.Next(possiblePlacesOfShip.Count())];
                    this.board.ComputerPossibleTargets = randomPosition;
                }
            
            }
            else if (result.CellStatus == CellStatusEnum.sinks)
            {
                board.ComputerPossibleTargets.Clear();
            }

            return result;
        }
        public GridCoordinates PlayerShoot(GridCoordinates cell)
        {
            var targetCell = Shoot(cell, this.board.TargetGrid);
       
            return targetCell;
        }

        public Board PrepareTheBoard()
        {
            var fleet = this.fleetManager.CreateFleet();

            var ocen = MakeEmptOcean();

            this.board.OceanGrid = this.fleetManager.SetShipsOnOceanGrid(fleet, ocen);

            var computerFleet = this.fleetManager.CreateFleet();
            var computerOcean = MakeEmptOcean();

            this.board.TargetGrid = this.fleetManager.SetShipsOnOceanGrid(computerFleet, computerOcean);

            return this.board;
        }

        public List<GridCoordinates> MakeEmptOcean()
        {
            var oceanGrid = new List<GridCoordinates>();
            int columns = 0;
            int rows = 0;
            do
            {
                oceanGrid.Add(new GridCell<GridCoordinates>()
                {
                    Row = rows,
                    Column = columns,
                    CellStatus = CellStatusEnum.Ocean
                });

                if (columns == 9)
                {
                    columns = 0;
                    rows++;
                    continue;
                }

                columns++;
            } while (oceanGrid.Count() != 100);

            return oceanGrid;
        }

        private GridCoordinates Shoot(GridCoordinates cell, List<GridCoordinates> gridCoordinates)
        {
            var targetCell = gridCoordinates.Find(x => x.Column == cell.Column && x.Row == cell.Row);

            if (targetCell == null)
            {
                throw new Exception("There is no cell with that coordinates on the board");
            }

            if (targetCell is GridCell<Ship> ship)
            {
                ship.CellContent.SizeOnGrid--;
                ship.CellStatus = CellStatusEnum.hits;

                if (ship.CellContent.SizeOnGrid == 0)
                {
                    ship.CellContent.IsAlive = false;
                    ship.CellStatus = CellStatusEnum.sinks;
                }

                return ship;
            }

            targetCell.CellStatus = CellStatusEnum.misses;
            return targetCell;
        }
    }
}
