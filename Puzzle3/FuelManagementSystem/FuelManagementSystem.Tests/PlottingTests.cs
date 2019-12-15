using System.Collections;
using System.Linq;
using System.Runtime.InteropServices;
using NUnit.Framework;

namespace FuelManagementSystem.Tests
{
    [TestFixture]
    public class PlottingTests
    {
        [Test]
        public void Straight_wire()
        {
            var routePlanner = new Plotter();
            var route = routePlanner.CreateRoute("R2").ToArray();
            CollectionAssert.AreEqual(new[] {Coords.From(0, 0), Coords.From(1, 0), Coords.From(2, 0)}, route);
        }

        [Test]
        public void Wire_with_turn()
        {
            var routePlanner = new Plotter();
            var route = routePlanner.CreateRoute("R1,D1").ToArray();
            CollectionAssert.AreEqual(new[] {Coords.From(0, 0), Coords.From(1, 0), Coords.From(1, 1)}, route);
        }

        [Test]
        public void Wire_with_two_turns()
        {
            var routePlanner = new Plotter();
            var route = routePlanner.CreateRoute("R1,D1,L1").ToArray();
            CollectionAssert.AreEqual(new[] {Coords.From(0, 0), Coords.From(1, 0), Coords.From(1, 1), Coords.From(0, 1)}, route);
        }

        [Test]
        public void Wire_with_three_turns()
        {
            var routePlanner = new Plotter();
            var route = routePlanner.CreateRoute("R1,D1,L1,U1").ToArray();
            CollectionAssert.AreEqual(new[] {Coords.From(0, 0), Coords.From(1, 0), Coords.From(1, 1), Coords.From(0, 1), Coords.From(0, 0)}, route);
        }
    }
}