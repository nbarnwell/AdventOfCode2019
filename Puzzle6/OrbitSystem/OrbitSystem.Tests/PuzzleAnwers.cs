namespace OrbitSystem.Tests
{
    using System.IO;
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
    }
}