using System.Collections.Generic;

namespace FuelManagementSystem
{
    public interface IMapper
    {
        void MapRoute(int routeId, string directions);
        IEnumerable<MapPoint> GetPoints();
        IEnumerable<Coords> GetIntersections();

        IEnumerable<Route> GetRoutes();
    }
}