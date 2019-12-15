using System.Collections.Generic;
using System.Linq;

namespace FuelManagementSystem
{
    public class Plotter : IPlotter
    {
        public IEnumerable<Coords> CreateRoute(string directions)
        {
            var vectors =
                directions.Split(',')
                          .Select(Vector.From);

            int x = 0, y = 0;
            yield return Coords.From(x, y);

            foreach (var vector in vectors)
            {
                int xDelta = 0, yDelta = 0;
                switch (vector.Direction)
                {
                    case Direction.Up:
                        xDelta = 0;
                        yDelta = -1;
                        break;
                    case Direction.Down:
                        xDelta = 0;
                        yDelta = 1;
                        break;
                    case Direction.Left:
                        xDelta = -1;
                        yDelta = 0;
                        break;
                    case Direction.Right:
                        xDelta = 1;
                        yDelta = 0;
                        break;
                }

                for (int i = 0; i < vector.Distance; i++)
                {
                    yield return Coords.From(x += xDelta, y += yDelta);
                }
            }
        }
    }
}