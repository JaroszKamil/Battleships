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
        public void PlayerShoot_ShipCoordinates_ShouldReturnShipWithOneHit()
        {
            //Arrange
            var shipCoordinates = board.TargetGrid.OfType<GridCell<Ship>>().GroupBy(x => x.CellContent.ShipGuid).First();
            var shipCell = shipCoordinates.First();
            var aspectedShipOnGridSize = shipCell.As<GridCell<Ship>>().CellContent.As<Ship>().SizeOnGrid - 1;

            //Act
            var result = this.gameplayManager.PlayerShoot(shipCell);
            
            //Assert
            result.Should().BeOfType<GridCell<Ship>>();
            result.OcenCellStatus.Should().Be(CellStatusEnum.hits);
            result.As<GridCell<Ship>>().CellContent.As<Ship>().SizeOnGrid.Should().Be(aspectedShipOnGridSize);
        }
    }
}
