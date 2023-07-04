using BattleShipsApi.Models;
using BattleShipsApi.Models.ShipModels;
using BattleShipsApi.Services;
using FluentAssertions;

namespace BattleShipsApi.Tests.UnitTests
{
    public class FleetManagerTests
    {
        private FleetManager fleetManager;
        private List<GridCoordinates> oceanGrid;
        public FleetManagerTests()
        {
            this.fleetManager = new FleetManager();
            var gameManager = new GameplayManager(fleetManager, new Board());
            oceanGrid = gameManager.MakeEmptOcean();
        }


        [Fact]
        public void CreateFleet_ShouldReturnFleet()
        {
            //Act
            var fleet = fleetManager.CreateFleet();

            //Assert
            fleet.Ships.Should().HaveCount(fleet.MaxAllowedPerShipType.Sum(x => x.Value));
        }

        [Fact]
        public void SetShipsOnOceanGrid_CorrectFleet_ShouldSetSheepOnBoard()
        {
            //Arrange
            var fleet = fleetManager.CreateFleet();
            var aspectedShipsCells = fleet.Ships?.Sum(x => x.Type == ShipTypes.Battleship ? 5 : 4) ?? 0;

            //Act
            var gridCoordinates = fleetManager.SetShipsOnOceanGrid(fleet, oceanGrid);
            var ships = gridCoordinates.FindAll(x => x is GridCell<Ship>);

            //Assert
            fleet.Ships.Should().HaveCount(fleet.MaxAllowedPerShipType.Sum(x => x.Value));
            gridCoordinates.Should().HaveCount(100);
            gridCoordinates.Should().Contain(x => x.Column == 0 && x.Row == 0);
            gridCoordinates.Should().Contain(x => x.Column == 9 && x.Row == 0);
            gridCoordinates.Should().Contain(x => x.Column == 0 && x.Row == 1);
            gridCoordinates.Should().Contain(x => x.Column == 9 && x.Row == 1);
            gridCoordinates.Should().Contain(x => x.Column == 9 && x.Row == 9);
            gridCoordinates.Should().Contain(x => x.Column == 9 && x.Row == 9);
            ships.Should().HaveCount(aspectedShipsCells);
            fleet.Ships?.All(x => x.IsSetOnGrid).Should().BeTrue();
            ships.Should().OnlyHaveUniqueItems(x => new { x.Row, x.Column });
        }
    }
}
