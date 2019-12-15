namespace FuelManagementSystem
{
    public class MapPoint
    {
        public int RouteId { get; }
        public Coords Coords { get; }

        private MapPoint(int routeId, Coords coords)
        {
            RouteId = routeId;
            Coords = coords;
        }

        protected bool Equals(MapPoint other)
        {
            return RouteId == other.RouteId && Coords.Equals(other.Coords);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((MapPoint) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (RouteId * 397) ^ Coords.GetHashCode();
            }
        }

        public static MapPoint From(int routeId, Coords coords)
        {
            return new MapPoint(routeId, coords);
        }
    }
}