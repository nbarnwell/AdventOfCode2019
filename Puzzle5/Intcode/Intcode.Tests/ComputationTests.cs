namespace Intcode.Tests
{
    using System;
    using System.IO;
    using System.Linq;

    using NUnit.Framework;

    [TestFixture]
    public class ComputationTests
    {
        [Test]
        public void Program_with_no_exit_instruction()
        {
            var instructions = new[] { 1, 0, 0, 0 };
            var computer = new IntcodeComputerBuilder().Build();
            
            Assert.Throws<IndexOutOfRangeException>(() => computer.Run(instructions));
        }

        [Test]
        public void Simple_addition()
        {
            var instructions = new[] { 1, 0, 0, 0, 99 };
            var computer = new IntcodeComputerBuilder().Build();
            computer.Run(instructions);

            Assert.AreEqual(2, computer.Memory.GetValueImmediate(0));
        }

        [Test]
        public void Simple_addition_using_parameter_modes()
        {
            var instructions = new[] { 1101, 100, -1, 4, 0 };
            var computer = new IntcodeComputerBuilder().Build();
            computer.Run(instructions);

            Assert.AreEqual(99, computer.Memory.GetValueImmediate(4));
        }

        [Test]
        public void Simple_multiplication()
        {
            var instructions = new[] { 2, 0, 0, 0, 99 };
            var computer = new IntcodeComputerBuilder().Build();
            computer.Run(instructions);

            Assert.AreEqual(4, computer.Memory.GetValueImmediate(0));
        }

        [Test]
        public void Multiple_instructions()
        {
            var instructions = new[] { 1, 1, 1, 4, 99, 5, 6, 0, 99 };
            var computer = new IntcodeComputerBuilder().Build();
            computer.Run(instructions);

            Assert.AreEqual(30, computer.Memory.GetValueImmediate(0));
        }

        [Test]
        [TestCase(new[] { -1, 0, 0, 0, 99 }, Description = "First instruction is invalid")]
        [TestCase(new[] { 1, 0, 0, 0, -1 }, Description = "Second instruction is invalid")]
        public void Invalid_opcode_shouldthrow(int[] instructions)
        {
            var computer = new IntcodeComputerBuilder().Build();
            Assert.Throws<InvalidOperationException>(() => computer.Run(instructions));
        }
    }
}