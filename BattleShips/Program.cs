using BattleShips;

namespace Battleships;

public class Programe
{
    public static void Main(string[] args)
    {
        Console.WriteLine("Welcome to BattleShips");

        bool continuePlaying = true;
        while (continuePlaying)
        {
            int gridSize = GetGridSizeFromUser();
            var enemyShips = GenerateEnemyShips(gridSize, out int battleShipCont, out int destroyerCount);

            var gameBoard = new GameGrid(gridSize);
            gameBoard.PlaceShipsRandomly(enemyShips);

            EnterGameplayLoop(gameBoard, battleShipCont, destroyerCount);

            continuePlaying = DoesPlayerWantToPlayAnotherGame();
        }

        Console.WriteLine("Thank you for playing Battleships");
    }

    private static void EnterGameplayLoop(GameGrid gameBoard, int battleShipCont, int destroyerCount)
    {
        gameBoard.RenderGrid();
        Console.WriteLine($"There are currently {battleShipCont} battleships and {destroyerCount} destroyers left");

        while (battleShipCont > 0 && destroyerCount > 0)
        {
            Console.WriteLine($"Ready to fire! Enter Coordinates to strike:");
            var newCoordinates = Console.ReadLine();
            Coordinate parsedCoordinates;

            try
            {
                if (string.IsNullOrWhiteSpace(newCoordinates) || newCoordinates.Length < 2)
                {
                    throw new ArgumentException("Invalid input format. Coordinates must be in the format 'A1' or 'AA10'.");
                }
                parsedCoordinates = ParseCoordinates(newCoordinates);
                if(!gameBoard.AreCoordinatesWithinBounds(parsedCoordinates))
                {
                    throw new ArgumentException("Given coordinates fall outside the game");
                }

                if(gameBoard.AttemptStrike(parsedCoordinates, out Ship? hitShip))
                {
                    if(hitShip == null)
                    {
                        throw new Exception("Space containing ship has no ship reference");
                    }
                    if(hitShip.IsSunk)
                    {
                        var shipType = hitShip.Name.Split(' ')[0];
                        Console.WriteLine($"You hit and sunk a {shipType}");
                        if(shipType == "BattleShip")
                        {
                            battleShipCont--;
                        }
                        else
                        {
                            destroyerCount--;
                        }
                    }
                    else
                    {
                        Console.WriteLine($"You hit a ship, congrats but its not down yet");
                    }
                }
                else
                {
                    Console.WriteLine($"Unfortunatly you missed");
                }
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                continue;
            }

            gameBoard.RenderGrid();
            Console.WriteLine($"There are currently {battleShipCont} battleships and {destroyerCount} destroyers left");
        }
        Console.WriteLine("Congratulations you destroyed all enemy ships!");
    }

    private static int GetGridSizeFromUser()
    {
        Console.WriteLine("Enter the size of the playing field (example: '15' for a 15x15 grid) or press enter for minimum 10x10");
        var gridSizeInput = Console.ReadLine();
        int gridSizeInt = 10;

        if (gridSizeInput != null)
        {
            bool validGridSize = false;
            while (!validGridSize)
            {
                if (int.TryParse(gridSizeInput, out int gridParsedInt))
                {
                    //User has given a grid size less than the min
                    if (gridParsedInt < 10)
                    {
                        Console.WriteLine("The grid cannot be less than 10x10");
                    }
                    else
                    {
                        Console.WriteLine($"Creating a playing field of {gridParsedInt}x{gridParsedInt}");
                        validGridSize = true;
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("You have entered an invalid number, please use a valid whole number");
                }

                Console.WriteLine("Enter the size of the playing field (example: '15' for a 15x15 grid) or press enter for minimum 10x10");
                gridSizeInput = Console.ReadLine();
            }
        }
        return gridSizeInt;
    }

    private static List<Ship> GenerateEnemyShips(int gridSize, out int battleshipCount, out int destroyerCount)
    {
        List<Ship> generatedShips = new List<Ship>();
        var totalSpaces = gridSize * gridSize;
        //This is the ratio outlined in the brief, if the user increases the grid size, this should increase the amount of ships.
        //We could create difficultly levels of lower/higher ratios to make this easily configurable
        var shipToGridRatio = 0.13;
        var numberOfEmemyShipSpaces = totalSpaces * shipToGridRatio;

        battleshipCount = 0;
        destroyerCount = 0;

        while (numberOfEmemyShipSpaces > 0)
        {
            if(numberOfEmemyShipSpaces >= 5)
            {
                //Add BattleShip
                battleshipCount++;
                generatedShips.Add(new Ship($"BattleShip {battleshipCount}", 5));
            }
            else if(numberOfEmemyShipSpaces >= 4)
            {
                //Add Destroyer
                destroyerCount++;
                generatedShips.Add(new Ship($"Destroyer {battleshipCount}", 4));
            }
            else
            {
                break;
            }
        }
        return generatedShips;
    }

    private static Coordinate ParseCoordinates(string input)
    {
        int firstDigitIndex = 0;
        while (firstDigitIndex < input.Length && char.IsLetter(input[firstDigitIndex]))
        {
            firstDigitIndex++;
        }

        // Ensure there is at least one letter and one number
        if (firstDigitIndex == 0 || firstDigitIndex == input.Length)
        {
            throw new ArgumentException("Invalid input format. Coordinates must contain letters followed by numbers.");
        }

        string columnLetters = input.Substring(0, firstDigitIndex).ToUpper(); // Extract letters (column)
        string rowNumbers = input.Substring(firstDigitIndex);   // Extract numbers (row)

        // Convert column letters to a number
        int columnValue = 0;
        foreach (char letter in columnLetters)
        {
            //Looks weird but basically converts the char to its numerical value alphabetically 
            //This loops to facilitate larger grids where columns could be AA or ABA
            columnValue = columnValue * 26 + (letter - 'A' + 1);
        }
        columnValue--;

        if (!int.TryParse(rowNumbers, out int rowValue) || rowValue < 1)
        {
            throw new ArgumentException("Invalid row format. Row must be a positive valid number.");
        }
        rowValue--;

        return new Coordinate(rowValue, columnValue);
    }

    private static bool DoesPlayerWantToPlayAnotherGame()
    {
        while (true)
        {
            Console.WriteLine("Would you like to play again? (Y/N)");
            var userContinueResponse = Console.ReadLine();
            if (userContinueResponse != null)
            {
                if (userContinueResponse == "Y")
                {
                    return true;
                }
                else if (userContinueResponse == "N")
                {
                    return false;
                }
            }
            Console.WriteLine("Invalid input. Please enter 'Y' or 'N'");
        }
    }
}