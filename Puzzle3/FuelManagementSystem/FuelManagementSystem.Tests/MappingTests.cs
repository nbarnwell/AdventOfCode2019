using System.Linq;
using NUnit.Framework;

namespace FuelManagementSystem.Tests
{
    [TestFixture]
    public class MappingTests
    {
        [Test]
        public void Map_single_line()
        {
            var plotter = new Plotter();
            var mapper = new Mapper(plotter);
            var routeId = 1;
            mapper.MapRoute(routeId, "R1");

            CollectionAssert.AreEqual(
                new[]
                {
                    MapPoint.From(routeId, Coords.From(0, 0)), 
                    MapPoint.From(routeId, Coords.From(1, 0))
                },
                mapper.GetPoints());
        }

        [Test]
        public void Detect_crossovers_above_right()
        {
            var plotter = new Plotter();
            var mapper = new Mapper(plotter);
            mapper.MapRoute(1, "R1,U1");
            mapper.MapRoute(2, "U1,R1");

            var crossovers = mapper.GetIntersections();

            CollectionAssert.AreEqual(new[] { Coords.From(1, -1)}, crossovers.ToArray());
        }

        [Test]
        public void Detect_crossovers_below_left()
        {
            var plotter = new Plotter();
            var mapper = new Mapper(plotter);
            mapper.MapRoute(1, "L1,D1");
            mapper.MapRoute(2, "D1,L1");

            var crossovers = mapper.GetIntersections();

            CollectionAssert.AreEqual(new[] { Coords.From(-1, 1)}, crossovers.ToArray());
        }
    }
}