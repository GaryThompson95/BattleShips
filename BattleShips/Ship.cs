namespace BattleShips
{
    internal class Ship
    {
        public string Name;
        public int Size, HitCount;
        public bool IsSunk;
        public List<Coordinate> Coordinates;


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
    }
}
