namespace OrbitSystem.Tests
{
    using System.IO;
    using NUnit.Framework;

    public class ChecksumTests
    {
        [Test]
        public void Build_a_chart_1_satellite_at_depth_1()
        {
            var chart = new AstronomicalChart(new AstronomicalObject("com"));
            var a = new AstronomicalObject("a");
            chart.Root.AddSatellite(a);

            var calculator = new ChecksumCalculator();
            var checksum = calculator.GetChecksum(chart);

            Assert.AreEqual(1, checksum);
        }

        [Test]
        public void Build_a_chart_2_satellites_at_depth_2()
        {
            var chart = new AstronomicalChart(new AstronomicalObject("com"));
            var a = new AstronomicalObject("a");
            chart.Root.AddSatellite(a);
            var a2 = new AstronomicalObject("a2");
            chart.Root.AddSatellite(a2);

            var calculator = new ChecksumCalculator();
            var checksum = calculator.GetChecksum(chart);

            Assert.AreEqual(2, checksum);
        }

        [Test]
        public void Build_a_chart_1_satellites_at_depth_1_and_2()
        {
            var chart = new AstronomicalChart(new AstronomicalObject("com"));
            var a = new AstronomicalObject("a");
            chart.Root.AddSatellite(a);
            var a2 = new AstronomicalObject("a2");
            a.AddSatellite(a2);

            var calculator = new ChecksumCalculator();
            var checksum = calculator.GetChecksum(chart);

            Assert.AreEqual(3, checksum);
        }

        [Test]
        public void Build_a_chart_4_satellites_at_depth_1_and_2()
        {
            var chart = new AstronomicalChart(new AstronomicalObject("com"));
            var a = new AstronomicalObject("a");
            chart.Root.AddSatellite(a);
            var a2 = new AstronomicalObject("a");
            a.AddSatellite(a2);
            var b = new AstronomicalObject("b");
            chart.Root.AddSatellite(b);
            var b2 = new AstronomicalObject("b2");
            a.AddSatellite(b2);

            var calculator = new ChecksumCalculator();
            var checksum = calculator.GetChecksum(chart);

            Assert.AreEqual(6, checksum);
        }

        [Test]
        [TestCase("Example1", ExpectedResult = 42)]
        [TestCase("Example2", ExpectedResult = 3)]
        [TestCase("Example3", ExpectedResult = 5)]
        [TestCase("Example4", ExpectedResult = 45)]
        public int Complex_chart_from_DSL(string inputFile)
        {
            var chartBuilder = new AstronomicalChartBuilder();
            var input = File.ReadAllText($".\\{inputFile}.txt");
            
            var chart = chartBuilder.Build(input);

            var calculator = new ChecksumCalculator();
            var checksum = calculator.GetChecksum(chart);

            return checksum;
        }
    }
}