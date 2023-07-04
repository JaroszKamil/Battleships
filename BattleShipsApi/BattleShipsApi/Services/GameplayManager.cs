using BattleShipsApi.Models;
using BattleShipsApi.Models.ShipModels;

namespace BattleShipsApi.Services
{
    public class GameplayManager : IGameplayManager
    {
        IFleetManager fleetManager;
        Board board;
        public GameplayManager(IFleetManager fleetManager, Board board)
        {
            this.fleetManager = fleetManager;
            this.board = board;
        }

        public GridCoordinates ComputerShoot(GridCoordinates cell)
        {
            throw new NotImplementedException();
        }
        public GridCoordinates PlayerShoot(GridCoordinates cell)
        {
            var targetCell = this.board.TargetGrid.Find(x => x.Column == cell.Column && x.Row == cell.Row);

            if(targetCell == null)
            {
                throw new Exception("There is no cell with that coordinates on the board");
            }

            if (targetCell is GridCell<Ship> ship)
            {
                ship.CellContent.SizeOnGrid--;
                ship.OcenCellStatus = CellStatusEnum.hits;

                if (ship.CellContent.SizeOnGrid == 0)
                {
                    ship.CellContent.IsAlive = false;
                    ship.OcenCellStatus = CellStatusEnum.sinks;
                }

                return ship;
            }

            targetCell.OcenCellStatus = CellStatusEnum.misses;
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
                    OcenCellStatus = CellStatusEnum.Ocean
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
    }
}
