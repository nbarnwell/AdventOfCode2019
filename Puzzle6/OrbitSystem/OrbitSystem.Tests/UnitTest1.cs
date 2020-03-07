using NUnit.Framework;
using OrbitSystem;

namespace OrbitSystem.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Checksum_is_correct()
        {
            var com = new AstronomicalObject("COM");
            var a = new AstronomicalObject("a");
            var orbit = new Orbit(com, a);

            var calculator = new ChecksumCalculator();
            var checksum = calculator.GetChecksum(com);

            Assert.AreEqual(0, checksum);
        }
    }
}