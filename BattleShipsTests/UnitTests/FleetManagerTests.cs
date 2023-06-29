using BattleShipsApi.Models;
using BattleShipsApi.Services;
using FluentAssertions;

namespace BattleShipsApi.Tests.UnitTests
{
    public class FleetManagerTests
    {
        private FleetManager fleetManager;
        public FleetManagerTests()
        {
            this.fleetManager = new FleetManager();
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

            //Act
            var gridCoordinates = fleetManager.SetShipsOnOceanGrid(fleet);

            //Assert
            fleet.Ships.Should().HaveCount(fleet.MaxAllowedPerShipType.Sum(x => x.Value));
            gridCoordinates.Should().HaveCount(100);
            gridCoordinates[99].Column.Should().Be(9);
            gridCoordinates[99].Row.Should().Be(9);
        }
    }
}
