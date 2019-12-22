namespace FuelManagementSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Mapper : IMapper
    {
        private readonly IPlotter _plotter;
        private readonly IDictionary<Coords, ISet<int>> _points = new Dictionary<Coords, ISet<int>>();
        private readonly ICollection<Route> _routes = new List<Route>();

        public Mapper(IPlotter plotter)
        {
            if (plotter == null) throw new ArgumentNullException(nameof(plotter));
            _plotter = plotter;
        }

        public void MapRoute(int routeId, string directions)
        {
            var coords = _plotter.CreateRoute(directions).ToList();

            _routes.Add(new Route(routeId, coords));

            foreach (var item in coords)
            {
                if (!_points.TryGetValue(item, out var routeIds))
                {
                    routeIds = new HashSet<int>();
                    _points.Add(item, routeIds);
                }

                routeIds.Add(routeId);
            }
        }

        public IEnumerable<MapPoint> GetPoints()
        {
            foreach (var kvp in _points)
            {
                foreach (var routeId in kvp.Value)
                {
                    yield return MapPoint.From(routeId, kvp.Key);
                }
            }
        }

        public IEnumerable<Coords> GetIntersections()
        {
            return _points.Where(x => !x.Key.Equals(Coords.From(0, 0)))
                          .Where(x => x.Value.Count > 1)
                          .Select(x => x.Key);
        }

        public IEnumerable<Route> GetRoutes()
        {
            return _routes;
        }
    }
}