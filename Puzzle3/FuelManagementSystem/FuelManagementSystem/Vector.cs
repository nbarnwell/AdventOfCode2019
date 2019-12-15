using System;

namespace FuelManagementSystem
{
    public class Vector
    {
        public Direction Direction { get; }
        public int Distance { get; }

        private Vector(Direction direction, int distance)
        {
            Direction = direction;
            Distance = distance;
        }

        public static Vector From(string instruction)
        {
            var direction = ParseDirection(instruction);
            var distance = ParseDistance(instruction);

            return new Vector(direction, distance);
        }

        private static int ParseDistance(string instruction)
        {
            return Convert.ToInt32(instruction.Substring(1, instruction.Length - 1));
        }

        private static Direction ParseDirection(string instruction)
        {
            var directionCode = instruction.Substring(0, 1);
            switch (directionCode)
            {
                case "U":
                    return Direction.Up;
                case "D":
                    return Direction.Down;
                case "L":
                    return Direction.Left;
                case "R":
                    return Direction.Right;
                default:
                    throw new InvalidOperationException($"Unknown direction code: {directionCode}");
            }
        }

        public override string ToString()
        {
            return $"{nameof(Direction)}: {Direction}, {nameof(Distance)}: {Distance}";
        }

        protected bool Equals(Vector other)
        {
            return Direction == other.Direction && Distance == other.Distance;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Vector) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Direction * 397) ^ Distance;
            }
        }
    }
}