namespace FuelManagementSystem
{
    public struct Coords
    {
        public int X { get; }
        public int Y { get; }

        private Coords(in int x, in int y)
        {
            X = x;
            Y = y;
        }

        public bool Equals(Coords other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            return obj is Coords other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public override string ToString()
        {
            return $"{nameof(X)}: {X}, {nameof(Y)}: {Y}";
        }

        public static Coords From(int x, int y)
        {
            return new Coords(x, y);
        }
    }
}