using Battleships;

namespace Battleship_Tests
{
    public class ProgramTests
    {
        [Fact]
        public void ParseCoordinates_ValidCoordinates()
        {
            //Arrange
            var input = "A1";

            //Act
            var parsedCoordinates = Programe.ParseCoordinates(input);

            //Assert
            Assert.True(parsedCoordinates != null);
            Assert.Equal(0, parsedCoordinates.Row);
            Assert.Equal(0, parsedCoordinates.Column);
        }

        [Fact]
        public void ParseCoordinates_HighColumnValue()
        {
            //Arrange
            var input = "AA1";

            //Act
            var parsedCoordinates = Programe.ParseCoordinates(input);

            //Assert
            Assert.True(parsedCoordinates != null);
            Assert.Equal(0, parsedCoordinates.Row);
            Assert.Equal(26, parsedCoordinates.Column);
        }

        [Fact]
        public void ParseCoordinates_NegativeValue()
        {
            //Arrange
            var input = "A-1";

            //Act
            try
            {
                var parsedCoordinates = Programe.ParseCoordinates(input);
            }
            catch (ArgumentException ex)
            {
                //Assert
                Assert.Equal("Invalid row format. Row must be a positive valid number.", ex.Message);
            }
        }

        [Fact]
        public void ParseCoordinates_InvalidColumn()
        {
            //Arrange
            var input = "?1";

            //Act
            try
            {
                var parsedCoordinates = Programe.ParseCoordinates(input);
            }
            catch (ArgumentException ex)
            {
                //Assert
                Assert.Equal("Invalid input format. Coordinates must contain letters followed by numbers.", ex.Message);
            }
        }

        [Fact]
        public void ParseCoordinates_InvalidRow()
        {
            //Arrange
            var input = "A?";

            //Act
            try
            {
                var parsedCoordinates = Programe.ParseCoordinates(input);
            }
            catch (ArgumentException ex)
            {
                //Assert
                Assert.Equal("Invalid row format. Row must be a positive valid number.", ex.Message);
            }
        }
    }
}