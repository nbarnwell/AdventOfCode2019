namespace Intcode.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class InstructionParameterTests
    {
        [Test]
        public void Think_of_a_name_later()
        {
            var computer = new IntcodeComputerBuilder().Build();
            computer.Run(new[] { 0001, 5, 6, 7, 99, 1, 2 });

            Assert.AreEqual(3, computer.Memory.GetValue(7));
        }
    }
}