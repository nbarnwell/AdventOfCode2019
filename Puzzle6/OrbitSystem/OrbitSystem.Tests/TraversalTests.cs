using NUnit.Framework;

namespace OrbitSystem.Tests
{
    using System.Linq;

    [TestFixture]
    public class TraversalTests
    {
        [Test]
        [TestCase("COM)A,A)YOU,COM)B,B)SAN", ExpectedResult = 2)]
        [TestCase("COM)A,A)YOU,COM)B,B)C,C)SAN", ExpectedResult = 3)]
        [TestCase("COM)B,B)C,C)D,D)E,E)F,B)G,G)H,D)I,E)J,J)K,K)L,K)YOU,I)SAN", ExpectedResult = 4)]
        public int Route_calculation(string input)
        {
            var chartBuilder = new AstronomicalChartBuilder();
            
            var chart = chartBuilder.Build(input);
            var routeFinder = new AstronomicalRouteFinder(chart);
            var route = routeFinder.CalculateRoute("YOU", "SAN");

            return route.Count() - 1; // Minus one because we're already at the first step, as it were
        }
    }
}