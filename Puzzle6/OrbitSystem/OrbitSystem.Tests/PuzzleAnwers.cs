namespace OrbitSystem.Tests
{
    using System.IO;
    using System.Linq;
    using NUnit.Framework;

    [TestFixture]
    public class PuzzleAnwers
    {
        [Test]
        public void Day6_Part1()
        {
            var chartBuilder = new AstronomicalChartBuilder();
            var input = File.ReadAllText(@".\PuzzleInput.txt");
            
            var chart = chartBuilder.Build(input);

            var calculator = new ChecksumCalculator();
            var checksum = calculator.GetChecksum(chart);

            Assert.AreEqual(154386, checksum);
        }

        [Test]
        public void Day6_Part2()
        {
            var chartBuilder = new AstronomicalChartBuilder();
            var input = File.ReadAllText(@".\PuzzleInput.txt");
            
            var chart = chartBuilder.Build(input);

            var routeFinder = new AstronomicalRouteFinder(chart);
            var route = routeFinder.CalculateRoute("YOU", "SAN");

            Assert.AreEqual(null, route.Count() - 1);
        }
    }
}