namespace BattleShips
{
    internal class Coordinate
    {
        public int Row, Column;
        public bool ContainsShip, IsHit;
        public Ship? ShipReference;


        public Coordinate(int row, int column)
        {
            Row = row;
            Column = column;
            ContainsShip = false;
            IsHit = false;
        }
    }
}
