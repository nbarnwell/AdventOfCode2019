namespace Intcode.Tests
{
    using System;
    using System.Diagnostics;
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
        [TestCase(new[] { 5, 10, 11,  104, 0,  99,  104, 1,  99,  0, 1, 6 }, ExpectedResult = 1)]
        [TestCase(new[] { 5, 9, 11,   104, 0,  99,  104, 1,  99,  0, 1, 6 }, ExpectedResult = 0)]
        [TestCase(new[] { 1105, 1, 6,  104, 0,  99,  104, 1,  99 }, ExpectedResult = 1)]
        [TestCase(new[] { 1105, 0, 6,  104, 0,  99,  104, 1,  99 }, ExpectedResult = 0)]
        public int Jump_if_true(int[] instructions)
        {
            var output = new QueuedOutputReceiverBuilder().Build();
            var computer = new IntcodeComputerBuilder().WithOutputReceiver(output).Build();

            computer.Run(instructions);
            
            return output.Dequeue();
        }

        [Test]
        [TestCase(new[] { 6, 9, 11,  104, 0,  99,  104, 1,  99,  0, 1, 6 }, ExpectedResult = 1)]
        [TestCase(new[] { 6, 10, 11,   104, 0,  99,  104, 1,  99,  0, 1, 6 }, ExpectedResult = 0)]
        [TestCase(new[] { 1106, 0, 6,  104, 0,  99,  104, 1,  99 }, ExpectedResult = 1)]
        [TestCase(new[] { 1106, 1, 6,  104, 0,  99,  104, 1,  99 }, ExpectedResult = 0)]
        public int Jump_if_false(int[] instructions)
        {
            var output = new QueuedOutputReceiverBuilder().Build();
            var computer = new IntcodeComputerBuilder().WithOutputReceiver(output).Build();

            computer.Run(instructions);
            
            return output.Dequeue();
        }

        [Test]
        [TestCase(0, 9, ExpectedResult = 1)]
        [TestCase(9, 0, ExpectedResult = 0)]
        [TestCase(0, 0, ExpectedResult = 0)]
        [TestCase(0, -9, ExpectedResult = 0)]
        [TestCase(-9, 0, ExpectedResult = 1)]
        public int Less_than_ByLocation(int x, int y)
        {
            var instructions = new[] { 7, 5, 6, 7, 99, x, y };
            var computer = new IntcodeComputerBuilder().Build();
            computer.Run(instructions);

            return computer.Memory.GetValueImmediate(7);
        }

        [Test]
        [TestCase(0, 9, ExpectedResult = 1)]
        [TestCase(9, 0, ExpectedResult = 0)]
        [TestCase(0, 0, ExpectedResult = 0)]
        [TestCase(0, -9, ExpectedResult = 0)]
        [TestCase(-9, 0, ExpectedResult = 1)]
        public int Less_than_immediate(int x, int y)
        {
            var instructions = new[] { 1107, x, y, 7, 99 };
            var computer = new IntcodeComputerBuilder().Build();
            computer.Run(instructions);

            return computer.Memory.GetValueImmediate(7);
        }

        [Test]
        [TestCase(0, 9, ExpectedResult = 0)]
        [TestCase(9, 0, ExpectedResult = 0)]
        [TestCase(0, 0, ExpectedResult = 1)]
        [TestCase(0, -9, ExpectedResult = 0)]
        [TestCase(-9, 0, ExpectedResult = 0)]
        [TestCase(-9, -9, ExpectedResult = 1)]
        [TestCase(9, 9, ExpectedResult = 1)]
        public int Equals_ByLocation(int x, int y)
        {
            var instructions = new[] { 8, 5, 6, 7, 99, x, y };
            var computer = new IntcodeComputerBuilder().Build();
            computer.Run(instructions);

            return computer.Memory.GetValueImmediate(7);
        }

        [Test]
        [TestCase(0, 9, ExpectedResult = 0)]
        [TestCase(9, 0, ExpectedResult = 0)]
        [TestCase(0, 0, ExpectedResult = 1)]
        [TestCase(0, -9, ExpectedResult = 0)]
        [TestCase(-9, 0, ExpectedResult = 0)]
        [TestCase(-9, -9, ExpectedResult = 1)]
        [TestCase(9, 9, ExpectedResult = 1)]
        public int Equals_immediate(int x, int y)
        {
            var instructions = new[] { 1108, x, y, 7, 99 };
            var computer = new IntcodeComputerBuilder().Build();
            computer.Run(instructions);

            return computer.Memory.GetValueImmediate(7);
        }

        [Test]
        [TestCase(new[] { 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9 }, -1, ExpectedResult = 1)]
        [TestCase(new[] { 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9 }, 0, ExpectedResult = 0)]
        [TestCase(new[] { 3, 12, 6, 12, 15, 1, 13, 14, 13, 4, 13, 99, -1, 0, 1, 9 }, 1, ExpectedResult = 1)]
        [TestCase(new[] { 3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1 }, -1, ExpectedResult = 1)]
        [TestCase(new[] { 3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1 }, 0, ExpectedResult = 0)]
        [TestCase(new[] { 3, 3, 1105, -1, 9, 1101, 0, 0, 12, 4, 12, 99, 1 }, 1, ExpectedResult = 1)]
        [TestCase(new[] { 3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,
                            1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,
                            999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99 }, 7, ExpectedResult = 999)]
        [TestCase(new[] { 3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,
                            1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,
                            999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99 }, 8, ExpectedResult = 1000)]
        [TestCase(new[] { 3,21,1008,21,8,20,1005,20,22,107,8,21,20,1006,20,31,
                            1106,0,36,98,0,0,1002,21,125,20,4,20,1105,1,46,104,
                            999,1105,1,46,1101,1000,1,20,4,20,1105,1,46,98,99 }, 9, ExpectedResult = 1001)]
        public int Jump_tests_examples(int[] instructions, int input)
        {
            var outputReceiver = new QueuedOutputReceiverBuilder().Build();
            var inputSender = new QueuedInputSenderBuilder().WithQueuedValues(input).Build();
            var computer = new IntcodeComputerBuilder().WithOutputReceiver(outputReceiver).WithInputSender(inputSender).Build();

            computer.Run(instructions);

            return outputReceiver.Dequeue();
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