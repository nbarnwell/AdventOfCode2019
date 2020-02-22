namespace Intcode.Tests
{
    using System.ComponentModel;
    using NUnit.Framework;

    [TestFixture]
    public class MemoryTests
    {
        [Test]
        [TestCase(-1, ExpectedResult = false)]
        [TestCase(0, ExpectedResult = false)]
        [TestCase(1, ExpectedResult = false)]
        public bool Empty_memory_has_no_valid_address(int address)
        {
            var memory = new MemoryBank();

            Assert.AreEqual(-1, memory.MaxIndex);
            return memory.IsValidAddress(address);
        }

        [Test]
        public void Roundtrip_positional_value_1()
        {
            var memory = new MemoryBank();
            memory.SetValueImmediate(0, 1);
            memory.SetValueByLocation(0, 99);
            Assert.AreEqual(99, memory.GetValueByLocation(0));
        }

        [Test]
        public void Roundtrip_positional_value_2()
        {
            var memory = new MemoryBank();
            memory.SetValueImmediate(0, 1);
            memory.SetValue(0, 99, ParameterMode.Position);
            Assert.AreEqual(99, memory.GetValue(0, ParameterMode.Position));
        }

        [Test]
        public void Roundtrip_immediate_value_1()
        {
            var memory = new MemoryBank();
            memory.SetValueImmediate(0, 99);
            Assert.AreEqual(99, memory.GetValueImmediate(0));
        }

        [Test]
        public void Roundtrip_immediate_value_2()
        {
            var memory = new MemoryBank();
            memory.SetValue(0, 99, ParameterMode.Immediate);
            Assert.AreEqual(99, memory.GetValue(0, ParameterMode.Immediate));
        }

        [Test]
        public void GetValue_with_invalid_parametermode_should_throw()
        {
            var memory = new MemoryBank();
            Assert.Throws<InvalidEnumArgumentException>(() => memory.GetValue(0, (ParameterMode)100));
        }

        [Test]
        public void SetValue_with_invalid_parametermode_should_throw()
        {
            var memory = new MemoryBank();
            Assert.Throws<InvalidEnumArgumentException>(() => memory.SetValue(0, 1, (ParameterMode)100));
        }
    }
}