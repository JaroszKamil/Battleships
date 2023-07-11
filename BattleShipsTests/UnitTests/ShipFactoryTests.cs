using BattleShipsApi.Models.ShipModels;
using FluentAssertions;

namespace BattleShipsApi.Tests.UnitTests
{
    public class ShipFactoryTests: IDisposable
    {
        Ship? aspectedShip;
        public ShipFactoryTests()
        {
            aspectedShip = new()
            {
                Type = ShipTypes.Battleship,
                IsAlive = true,
                Size = 5
            };
        }

        [Fact]
        public void CreateShip_InputShipType_ShouldReturnNewShipClassInstance()
        {
            //Arrange
            ShipTypes shipType = ShipTypes.Battleship;
            //Act
            var ship = ShipFactory.GetShip(shipType);
            ship.ShipGuid = new Guid();
            //Assert
            ship.Should().NotBeNull();
            ship.Should().BeEquivalentTo(aspectedShip);
        }

        [Fact]
        public void CreateShip_InputShipType_ShouldNotMatch()
        {
            //Arrange
            ShipTypes shipType = ShipTypes.Destroyers;
            //Act
            var ship = ShipFactory.GetShip(shipType);
            //Assert
            ship.Should().NotBeNull();
            ship.Should().NotBeEquivalentTo(aspectedShip);
        }

        [Fact]
        public void CreateShip_IncorrectShipType_ShouldThrowNotImplementedException()
        {
            //Arrange
            var shipType = (ShipTypes)100;

            //Act
            Action act = () => ShipFactory.GetShip(shipType);
            //Assert
            Assert.Throws<Exception>(act).Message.Should().Be("Wrong ship type");
        }

        public void Dispose()
        {
            aspectedShip = null;
        }
    }
}