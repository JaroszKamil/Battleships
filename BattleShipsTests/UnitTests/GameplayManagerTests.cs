using BattleShipsApi.Models;
using BattleShipsApi.Models.ShipModels;
using BattleShipsApi.Services;
using FluentAssertions;

namespace BattleShipsApi.Tests.UnitTests
{
    public class GameplayManagerTests
    {
        private FleetManager fleetManager;
        private List<GridCoordinates> oceanGrid;
        private GameplayManager gameplayManager;
        private Board board;
        public GameplayManagerTests()
        {
            this.fleetManager = new FleetManager();
            this.gameplayManager = new GameplayManager(fleetManager, new Board());
            this.board = this.gameplayManager.PrepareTheBoard();
        }

        [Fact]
        public void PrepareTheBoard_ShouldReturnTwoDiffrentBoards()
        {
            //Arange
            var oceanGrid = this.board.OceanGrid;
            var ocenGridShips = this.board.OceanGrid.OfType<GridCell<Ship>>().GroupBy(x => x.CellContent.ShipGuid);

            var targetGrid = this.board.TargetGrid;
            var targetGridShips = this.board.TargetGrid.OfType<GridCell<Ship>>().GroupBy(x => x.CellContent.ShipGuid);

            //Assert
            oceanGrid.Should().NotBeEquivalentTo(targetGrid);
            ocenGridShips.Should().HaveCountGreaterThan(1);
            targetGridShips.Should().HaveCountGreaterThan(1);
            ocenGridShips.Should().NotBeSameAs(targetGridShips);
        }

        [Fact]
        public void PlayerShoot_OnlyOneShipCoordinate_ShouldReturnShipWithOneHit()
        {
            //Arrange
            var shipCoordinates = board.TargetGrid.OfType<GridCell<Ship>>().GroupBy(x => x.CellContent.ShipGuid).First();
            var shipCell = shipCoordinates.First();
            var aspectedShipOnGridSize = shipCell.As<GridCell<Ship>>().CellContent.As<Ship>().SizeOnGrid - 1;

            //Act
            var result = this.gameplayManager.PlayerShoot(shipCell);

            var ship = result.As<GridCell<Ship>>().CellContent.As<Ship>();

            //Assert
            result.Should().BeOfType<GridCell<Ship>>();
            result.CellStatus.Should().Be(CellStatusEnum.hits);
            ship.SizeOnGrid.Should().Be(aspectedShipOnGridSize);
            ship.IsAlive.Should().BeTrue();
        }

        [Fact]
        public void PlayerShoot_AllShipCoordinates_ShipShouldBeSink()
        {
            //Arrange
            var shipCoordinates = board.TargetGrid.OfType<GridCell<Ship>>().GroupBy(x => x.CellContent.ShipGuid).First();
            var shipCell = shipCoordinates.First();
            var aspectedShipOnGridSize = shipCell.As<GridCell<Ship>>().CellContent.As<Ship>().SizeOnGrid - 1;

            //Act
            GridCoordinates? result = null;

            shipCoordinates.ToList().ForEach(x =>
            {
                result = this.gameplayManager.PlayerShoot(shipCell);
            });

            var ship = result.As<GridCell<Ship>>().CellContent.As<Ship>();

            //Assert
            result.Should().BeOfType<GridCell<Ship>>();
            result?.CellStatus.Should().Be(CellStatusEnum.sinks);
            ship.IsAlive.Should().BeFalse();
            ship.SizeOnGrid.Should().Be(0);
        }

        [Fact]
        public void PlayerShoot_NoCoordinates_ShipThrowException()
        {
            //Arrange
            GridCoordinates shipCoordinates = null;

            //Act
            var act = () => this.gameplayManager.PlayerShoot(shipCoordinates);

            //Assert
            act.Should().Throw<Exception>("There is no cell with that coordinates on the board");
        }

        [Fact]
        public void ComputerShoot_ShipCoordinates_ShouldSetComputerPossibleTargets()
        {
            //Arrange
            var shipCoordinates = board.OceanGrid.OfType<GridCell<Ship>>().GroupBy(x => x.CellContent.ShipGuid).First();
            var shipCell = shipCoordinates.First();
            var aspectedShipOnGridSize = shipCell.As<GridCell<Ship>>().CellContent.As<Ship>().SizeOnGrid - 1;

            //Act
            var result = this.gameplayManager.ComputerShoot(shipCell);

            var ship = result.As<GridCell<Ship>>().CellContent.As<Ship>();

            //Assert
            result.Should().BeOfType<GridCell<Ship>>();
            this.board.ComputerPossibleTargets.Should().HaveCount(ship.Size);
        }

        [Fact]
        public void ComputerShoot_AllShipCoordinates_ShouldSinkShip()
        {
            //Arrange
            var shipCoordinates = board.OceanGrid.OfType<GridCell<Ship>>().GroupBy(x => x.CellContent.ShipGuid).First();
            var shipCell = shipCoordinates.First();
            var aspectedShipOnGridSize = shipCell.As<GridCell<Ship>>().CellContent.As<Ship>().SizeOnGrid - 1;

            //Act
            GridCoordinates? result = null;

            shipCoordinates.ToList().ForEach(x =>
            {
                result = this.gameplayManager.ComputerShoot(shipCell);
            });
 
            var ship = result.As<GridCell<Ship>>().CellContent.As<Ship>();

            //Assert
            result.Should().BeOfType<GridCell<Ship>>();
            this.board.ComputerPossibleTargets.Should().HaveCount(0);
            result?.CellStatus.Should().Be(CellStatusEnum.sinks);
            ship.IsAlive.Should().BeFalse();
            ship.SizeOnGrid.Should().Be(0);
        }
    }
}
