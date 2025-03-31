namespace BattleShips
{
    public class GameGrid
    {
        //2D Array of Coordinate to represent the Game Grid where the ships will be placed
        public Coordinate[,] GameBoard;
        private readonly Random RandomNumberGenerator;

        public GameGrid(int gameBoardSize) 
        {
            GameBoard = new Coordinate[gameBoardSize, gameBoardSize];

            // Populate the board with Coordinate objects
            for (int row = 0; row < gameBoardSize; row++)
            {
                for (int col = 0; col < gameBoardSize; col++)
                {
                    GameBoard[row, col] = new Coordinate(row, col);
                }
            }

            RandomNumberGenerator = new Random();
        }

        public void RenderGrid()
        {
            int gridSize = GameBoard.GetLength(0); // Get the size of the grid (assuming a square grid)

            // Print column headers (A, B, C, etc.)
            Console.Write("   "); // Empty space for row numbers
            for (int col = 0; col < gridSize; col++)
            {
                Console.Write((char)('A' + col) + " ");
            }
            Console.WriteLine();

            // Print each row
            for (int row = 0; row < gridSize; row++)
            {
                // Print the row number
                var rowNumberToPrint = row + 1;
                Console.Write(rowNumberToPrint.ToString("D2") + " "); // Pad row number with leading zero if needed

                for (int col = 0; col < gridSize; col++)
                {
                    // Determine the cell's visual state
                    Coordinate cell = GameBoard[row, col];
                    char cellState;

                    if (cell.IsHit)
                    {
                        cellState = cell.ContainsShip ? 'X' : 'M'; // 'X' for hit ship, 'M' for missed shot
                    }
                    else
                    {
                        cellState = 'O'; // 'O' for open water (not hit)
                    }

                    Console.Write(cellState + " ");
                }

                Console.WriteLine(); // Move to the next row
            }
            Console.WriteLine("Legend: O (Unstruck space), M (Missed/Empty Space), X (Hit target on this space)");
        }

        public void PlaceShipsRandomly(List<Ship> ships)
        {
            foreach (Ship ship in ships)
            {
                bool foundValidPlacement = false;
                while (!foundValidPlacement)
                {
                    var startingRow = RandomNumberGenerator.Next(0, GameBoard.GetLength(0) - 1);
                    var startingColumn = RandomNumberGenerator.Next(0, GameBoard.GetLength(0) - 1);
                    var direction = RandomNumberGenerator.Next(1, 4);
                    var proposedCoordinates = GenerateProposedCoordinates(startingRow, startingColumn, direction, ship.Size);

                    if(AreCoordinatesWithinBounds(proposedCoordinates) && AreCoordinatesFreeOfOtherShips(proposedCoordinates))
                    {
                        ship.Coordinates = proposedCoordinates;
                        foreach(var coordinate in proposedCoordinates)
                        {
                            var selectedSpace = GameBoard[coordinate.Row, coordinate.Column];
                            selectedSpace.ContainsShip = true;
                            selectedSpace.ShipReference = ship;
                        }
                        foundValidPlacement = true;
                    }
                }
            }
        }

        public bool AttemptStrike(Coordinate coordinate, out Ship? hitShip)
        {
            var targetedSpace = GameBoard[coordinate.Row, coordinate.Column];
            if(targetedSpace.IsHit)
            {
                throw new ArgumentException("The coordinates given have already been hit in a previous strike");
            }

            targetedSpace.IsHit = true;

            if(targetedSpace.ContainsShip)
            {
                hitShip = targetedSpace.ShipReference;
                if(hitShip == null)
                {
                    throw new Exception("Space containing ship has no ship reference");
                }
                hitShip.RegisterHit();
                return true;
            }

            hitShip = null;
            return false;
        }


        public List<Coordinate> GenerateProposedCoordinates(int startingRow, int startingColumn, int direction, int shipSize)
        {
            var proposedCoordinates = new List<Coordinate>();
            switch (direction)
            {
                //Up
                case 1:
                    for (int row = 0; row < shipSize; row++)
                    {
                        //Incrase the row each loop to create coordinates going up
                        proposedCoordinates.Add(new Coordinate(startingRow - row, startingColumn));
                    }
                    break;
                //Down
                case 2:
                    for (int row = 0; row < shipSize; row++)
                    {
                        //Decrease the row each loop to create coordinates going down
                        proposedCoordinates.Add(new Coordinate(startingRow + row, startingColumn));
                    }
                    break;
                //Left
                case 3:
                    for (int col = 0; col < shipSize; col++)
                    {
                        //Decrease the columnn each loop to create coordinates going left
                        proposedCoordinates.Add(new Coordinate(startingRow, startingColumn - col));
                    }
                    break;
                //Right
                case 4:
                    for (int col = 0; col < shipSize; col++)
                    {
                        //Incrase the column each loop to create coordinates going right
                        proposedCoordinates.Add(new Coordinate(startingRow, startingColumn + col));
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return proposedCoordinates;
        }

        public bool AreCoordinatesWithinBounds(Coordinate coordinates)
        {
            if (coordinates.Row < 0 || coordinates.Row > GameBoard.GetLength(0) - 1 ||
                coordinates.Column < 0 || coordinates.Column > GameBoard.GetLength(0) - 1)
            {
                return false;
            }
    
            return true;
        }

        public bool AreCoordinatesWithinBounds(List<Coordinate> coordinates)
        {
            foreach (var coordinate in coordinates)
            {
                if (!AreCoordinatesWithinBounds(coordinate))
                {
                    return false;
                }
            }
            // All coordinates are within bounds
            return true;
        }

        public bool AreCoordinatesFreeOfOtherShips(List<Coordinate> coordinates)
        {
            foreach(var coordinate in coordinates)
            {
                if(coordinate.ContainsShip)
                {
                    //At least one space has another ship
                    return false; 
                }
            }

            //No spaces hold other ships
            return true;
        }
    }
}
