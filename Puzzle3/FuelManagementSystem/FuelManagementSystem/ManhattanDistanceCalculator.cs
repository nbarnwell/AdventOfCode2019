using System;

namespace FuelManagementSystem
{
    public class ManhattanDistanceCalculator : IDistanceCalculator
    {
        public int GetDistance(Coords start, Coords end)
        {
            return Math.Abs(end.X - start.X) + Math.Abs(end.Y - start.Y);
        }
    }
}