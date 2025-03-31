namespace BattleShips
{
    public class Coordinate
    {
        public int Row { get; }
        public int Column { get; }
        public bool ContainsShip { get; private set; }
        public bool IsHit { get; private set; }
        public Ship? ShipReference { get; private set; }


        public Coordinate(int row, int column)
        {
            Row = row;
            Column = column;
            ContainsShip = false;
            IsHit = false;
        }

        public void SetShip(Ship ship)
        {
            ContainsShip = true;
            ShipReference = ship;
        }

        public void RegisterHit()
        {
            IsHit = true;
        }
    }
}
