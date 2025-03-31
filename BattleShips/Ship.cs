namespace BattleShips
{
    public class Ship
    {
        public string Name { get; private set; }
        public int Size { get; private set; }
        public int HitCount { get; private set; }
        public bool IsSunk { get; private set; }
        public List<Coordinate> Coordinates { get; private set; }


        public Ship(string name, int size) 
        {
            Name = name;
            Size = size;
            HitCount = 0;
            IsSunk = false;
            Coordinates = new List<Coordinate>();
        }

        public void RegisterHit()
        {
            HitCount++;
            if(HitCount == Size)
            {
                IsSunk = true;
            }
        }

        public void SetCoordinates(List<Coordinate> coordinates)
        {
            Coordinates = coordinates;
        }
    }
}
