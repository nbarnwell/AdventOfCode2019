namespace FuelManagementSystem
{
    using System.Linq;

    public class Optimiser
    {
        public int LeastDistanceToIntersection(IMapper mapper)
        {
            /*
            Get all intersections
            Foreach intersection
                Get all routes at that intersection
                Sum the steps back to the origin of each route
                Return the lowest number
            */
            var intersections = mapper.GetIntersections();
            var routes = mapper.GetRoutes();
            return intersections.Select(
                                    intersection =>
                                        {
                                            return routes
                                                   .Where(x => x.Coords.Contains(intersection))
                                                   .Select(x => x.StepsUntil(intersection))
                                                   .Sum(x => x);
                                        })
                                .Min(x => x);
        }
    }
}