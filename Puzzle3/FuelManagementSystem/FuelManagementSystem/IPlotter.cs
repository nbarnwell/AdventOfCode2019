using System.Collections.Generic;

namespace FuelManagementSystem
{
    public interface IPlotter
    {
        IEnumerable<Coords> CreateRoute(string directions);
    }
}