using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using NUnit.Framework;

namespace OrbitSystem.Tests
{
    public class Tests
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
        public void Complex_chart_from_DSL()
        {
            var chartBuilder = new AstronomicalChartBuilder();
            var input = @"
                COM)B
                B)C
                C)D
                D)E
                E)F
                B)G
                G)H
                D)I
                E)J
                J)K
                K)L
                ";
            
            var chart = chartBuilder.Build(input);

            var calculator = new ChecksumCalculator();
            var checksum = calculator.GetChecksum(chart);

            Assert.AreEqual(42, checksum);
        }
    }
}