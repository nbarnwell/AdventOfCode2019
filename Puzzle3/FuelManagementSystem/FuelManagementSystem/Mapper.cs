using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;

namespace FuelManagementSystem
{
    public class Mapper : IMapper
    {
        private readonly IPlotter _plotter;
        private readonly IDictionary<Coords, ISet<int>> _points = new Dictionary<Coords, ISet<int>>();

        public Mapper(IPlotter plotter)
        {
            if (plotter == null) throw new ArgumentNullException(nameof(plotter));
            _plotter = plotter;
        }

        public void MapRoute(int routeId, string directions)
        {
            var coords = _plotter.CreateRoute(directions);

            foreach (var item in coords)
            {
                ISet<int> routeIds;
                if (!_points.TryGetValue(item, out routeIds))
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

        public IEnumerable<Coords> GetCrossovers()
        {
            return _points.Where(x => !x.Key.Equals(Coords.From(0, 0)))
                          .Where(x => x.Value.Count > 1)
                          .Select(x => x.Key);
        }
    }
}