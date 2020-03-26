namespace OrbitSystem
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class AstronomicalRouteFinder
    {
        private readonly AstronomicalChart _chart;

        public AstronomicalRouteFinder(AstronomicalChart chart)
        {
            _chart = chart ?? throw new ArgumentNullException(nameof(chart));
        }

        public IEnumerable<AstronomicalObject> CalculateRoute(string startName, string destinationName)
        {
            // Because we're interested in the steps from startName's ORBIT to destinationName's ORBIT, not from
            // startName to destinationName themselves, we use their primaries
            var rootToStart = SearchGraph(_chart.Root, startName).ToList();
            var rootToDestination = SearchGraph(_chart.Root, destinationName).ToList();

            var startOrbit = rootToStart.Last().Primary;
            var destinationOrbit = rootToDestination.Last().Primary;

            var commonAncestor = FindCommonAncestor(rootToStart, rootToDestination);

            var startToCommonAncestor = GetParents(startOrbit).TakeWhile(x => x != commonAncestor);
            var destinationToCommonAncestor = GetParents(destinationOrbit).TakeWhile(x => x != commonAncestor);

            return startToCommonAncestor.Append(commonAncestor).Concat(destinationToCommonAncestor);
        }

        private IEnumerable<AstronomicalObject> GetParents(AstronomicalObject start)
        {
            var current = start;
            do
            {
                yield return current;
                current = current.Primary;
            }
            while (current != null);
        }

        private AstronomicalObject FindCommonAncestor(IList<AstronomicalObject> pathToStart, IList<AstronomicalObject> pathToDestination)
        {
            AstronomicalObject lastMatch = null;
            for (int i = 0; i < Math.Min(pathToStart.Count, pathToDestination.Count); i++)
            {
                if (pathToStart[i].Name == pathToDestination[i].Name)
                {
                    lastMatch = pathToStart[i];
                }
                else
                {
                    break;
                }
            }

            return lastMatch;
        }

        private IEnumerable<AstronomicalObject> SearchGraph(AstronomicalObject start, string searchFor)
        {
            if (start.Name == searchFor)
            {
                foreach (var parent in GetParents(start).Reverse())
                {
                    yield return parent;
                }
            }
            else
            {
                foreach (var satellite in start.Satellites)
                {
                    var results = SearchGraph(satellite, searchFor);

                    if (results.Any())
                    {
                        foreach (var result in results)
                        {
                            yield return result;
                        }
                    }
                }
            }
        }
    }
}