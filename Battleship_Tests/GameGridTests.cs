using BattleShips;

namespace Battleship_Tests
{
    public class GameGridTests
    {
        [Fact]
        public void AreCoordinatesWithinBounds_FallsWithinGrid()
        {
            // Arrange
            GameGrid grid = new GameGrid(10);
            List<Coordinate> coordinates = new List<Coordinate>
            {
                new Coordinate(0, 0),
                new Coordinate(0, 1),
                new Coordinate(0, 2),
                new Coordinate(0, 3)
            };
            // Act
            bool result = grid.AreCoordinatesWithinBounds(coordinates);
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AreCoordinatesWithinBounds_FallsOutsideRows()
        {
            // Arrange
            GameGrid grid = new GameGrid(10);
            List<Coordinate> coordinates = new List<Coordinate>
            {
                new Coordinate(11, 0),
                new Coordinate(11, 1),
                new Coordinate(11, 2),
                new Coordinate(11, 3)
            };
            // Act
            bool result = grid.AreCoordinatesWithinBounds(coordinates);
            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AreCoordinatesWithinBounds_FallsOutsideColumns()
        {
            // Arrange
            GameGrid grid = new GameGrid(10);
            List<Coordinate> coordinates = new List<Coordinate>
            {
                new Coordinate(1, 11),
                new Coordinate(1, 11),
                new Coordinate(1, 11),
                new Coordinate(1, 11)
            };
            // Act
            bool result = grid.AreCoordinatesWithinBounds(coordinates);
            // Assert
            Assert.False(result);
        }

        [Fact]
        public void AreCoordinatesFreeOfOtherShips_NoShips()
        {
            // Arrange
            GameGrid grid = new GameGrid(10);
            List<Coordinate> coordinates = new List<Coordinate>
            {
                new Coordinate(0, 0),
                new Coordinate(0, 1),
                new Coordinate(0, 2),
                new Coordinate(0, 3)
            };
            // Act
            bool result = grid.AreCoordinatesFreeOfOtherShips(coordinates);
            // Assert
            Assert.True(result);
        }

        [Fact]
        public void AreCoordinatesFreeOfOtherShips_HasShip()
        {
            // Arrange
            GameGrid grid = new GameGrid(10);
            List<Coordinate> coordinates = new List<Coordinate>
            {
                new Coordinate(0, 0),
                new Coordinate(0, 1),
                new Coordinate(0, 2),
                new Coordinate(0, 3)
            };
            coordinates[0].ContainsShip = true;
            // Act
            bool result = grid.AreCoordinatesFreeOfOtherShips(coordinates);
            // Assert
            Assert.False(result);
        }

        [Fact]
        public void GenerateProposedCoordinates_GoingNorth()
        {
            // Arrange
            GameGrid grid = new GameGrid(10);
            var shipSize = 3;
            // Act
            List<Coordinate> result = grid.GenerateProposedCoordinates(5, 5, 1, shipSize);
            // Assert
            Assert.True(result.Count == shipSize);
            Assert.Equal(5, result[0].Row);
            Assert.Equal(5, result[0].Column);
            Assert.Equal(4, result[1].Row);
            Assert.Equal(5, result[1].Column);
            Assert.Equal(3, result[2].Row);
            Assert.Equal(5, result[2].Column);
        }

        [Fact]
        public void GenerateProposedCoordinates_GoingSouth()
        {
            // Arrange
            GameGrid grid = new GameGrid(10);
            var shipSize = 3;
            // Act
            List<Coordinate> result = grid.GenerateProposedCoordinates(5, 5, 2, shipSize);
            // Assert
            Assert.True(result.Count == shipSize);
            Assert.Equal(5, result[0].Row);
            Assert.Equal(5, result[0].Column);
            Assert.Equal(6, result[1].Row);
            Assert.Equal(5, result[1].Column);
            Assert.Equal(7, result[2].Row);
            Assert.Equal(5, result[2].Column);
        }

        [Fact]
        public void GenerateProposedCoordinates_GoingLeft()
        {
            // Arrange
            GameGrid grid = new GameGrid(10);
            var shipSize = 3;
            // Act
            List<Coordinate> result = grid.GenerateProposedCoordinates(5, 5, 3, shipSize);
            // Assert
            Assert.True(result.Count == shipSize);
            Assert.Equal(5, result[0].Row);
            Assert.Equal(5, result[0].Column);
            Assert.Equal(5, result[1].Row);
            Assert.Equal(4, result[1].Column);
            Assert.Equal(5, result[2].Row);
            Assert.Equal(3, result[2].Column);
        }

        [Fact]
        public void GenerateProposedCoordinates_GoingRight()
        {
            // Arrange
            GameGrid grid = new GameGrid(10);
            var shipSize = 3;
            // Act
            List<Coordinate> result = grid.GenerateProposedCoordinates(5, 5, 4, shipSize);
            // Assert
            Assert.True(result.Count == shipSize);
            Assert.Equal(5, result[0].Row);
            Assert.Equal(5, result[0].Column);
            Assert.Equal(5, result[1].Row);
            Assert.Equal(6, result[1].Column);
            Assert.Equal(5, result[2].Row);
            Assert.Equal(7, result[2].Column);
        }
    }
}