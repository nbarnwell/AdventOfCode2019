using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Intcode.Tests
{
    public class EnumerableTests
    {
        [Test]
        [TestCase(new[] { 1,2,3 }, ExpectedResult = new [] { 2,3,1 })]
        [TestCase(new[] { 1,2,3,4 }, ExpectedResult = new [] { 2,3,4,1 })]
        public IEnumerable<int> Rotating_an_enumerable(IEnumerable<int> values)
        {
            return values.Rotate().ToArray();
        }
    }
}