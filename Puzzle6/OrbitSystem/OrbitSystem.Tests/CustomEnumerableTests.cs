namespace OrbitSystem.Tests
{
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using NUnit.Framework;

    [TestFixture]
    public class CustomEnumerableTests
    {
        [Test]
        public void Enumerable_TakeUntil()
        {
            var input = new[] { "A", "B", "C" };
            var result = input.TakeUntil(x => x == "B");

            CollectionAssert.AreEqual(new [] { "A", "B" }, result.ToArray());
        }
    }
}