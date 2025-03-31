using BattleShips;

namespace Battleship_Tests
{
    public class ShipTests
    {
        [Fact]
        public void RegisterHit_ShipIsThenSunk()
        {
            //Arrange
            var ship = new Ship("Test", 1);
            Assert.False(ship.IsSunk);
            Assert.True(ship.HitCount == 0);

            //Act
            ship.RegisterHit();

            //Assert
            Assert.True(ship.IsSunk);
            Assert.True(ship.HitCount == 1);
        }
    }
}