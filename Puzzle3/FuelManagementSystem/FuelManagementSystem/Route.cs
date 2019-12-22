namespace FuelManagementSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Route
    {
        public int Id { get; }
        public IEnumerable<Coords> Coords { get; }

        public Route(int routeId, IEnumerable<Coords> coords)
        {
            Id = routeId;
            Coords = coords;
        }

        public int StepsUntil(Coords destination)
        {
            if (!Coords.Any(x => x.Equals(destination)))
            {
                throw new InvalidOperationException($"Destination {destination} is not on this route.");
            }

            int result = 0;
            foreach (var coord in Coords)
            {
                if (coord.Equals(destination))
                {
                    return result;
                }
                result++;
            }

            return result;
        }
    }
}