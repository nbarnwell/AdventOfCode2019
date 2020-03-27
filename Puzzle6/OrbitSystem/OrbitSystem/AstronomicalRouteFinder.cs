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
            var start = SearchGraph(_chart.Root, startName);
            var destination = SearchGraph(_chart.Root, destinationName);

            var startOrbit = start.Primary;
            var destinationOrbit = destination.Primary;

            var startToRoot       = GetParents(startOrbit);
            var destinationToRoot = GetParents(destinationOrbit);

            var commonAncestor = FindCommonAncestor(startToRoot.Reverse(), destinationToRoot.Reverse());

            var startToCommonAncestor       = startToRoot.TakeWhile(x => x != commonAncestor);
            var destinationToCommonAncestor = destinationToRoot.TakeWhile(x => x != commonAncestor);

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

        private AstronomicalObject FindCommonAncestor(IEnumerable<AstronomicalObject> pathToStart, IEnumerable<AstronomicalObject> pathToDestination)
        {
            var path1 = pathToStart.ToList();
            var path2 = pathToDestination.ToList();

            AstronomicalObject lastMatch = null;
            for (int i = 0; i < Math.Min(path1.Count, path2.Count); i++)
            {
                if (path1[i].Name == path2[i].Name)
                {
                    lastMatch = path1[i];
                }
                else
                {
                    break;
                }
            }

            return lastMatch;
        }

        private AstronomicalObject SearchGraph(AstronomicalObject start, string searchFor)
        {
            if (start.Name == searchFor)
            {
                return start;
            }

            foreach (var satellite in start.Satellites)
            {
                var result = SearchGraph(satellite, searchFor);

                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }
    }
}