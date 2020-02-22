namespace Intcode.Tests
{
    using System;
    using NUnit.Framework;

    [TestFixture]
    public class InputOutputTests
    {
        [Test]
        [TestCase(1, 0, ExpectedResult = 1)]
        [TestCase(7, 50, ExpectedResult = 7)]
        [TestCase(70, 1000000, ExpectedResult = 70)]
        public int Takes_input_and_saves_at_correct_location(int input, int location)
        {
            var inputSender = new QueuedInputSenderBuilder().Build();
            inputSender.Enqueue(input);
            var computer = new IntcodeComputerBuilder().WithInputSender(inputSender).Build();
            computer.Run(new[] { 3, location, 99 }); // Take an input and put the value at position 0

            return computer.Memory.GetValueImmediate(location);
        }

        [Test]
        [TestCase(3, ExpectedResult = 50)]
        [TestCase(4, ExpectedResult = 60)]
        public int Returns_output_defined_by_program(int location)
        {
            IOutputReceiver outputReceiver = new QueuedOutputReceiverBuilder().Build();
            var computer = new IntcodeComputerBuilder().WithOutputReceiver(outputReceiver).Build();
            computer.Run(new[] { 4, location, 99, 50, 60 });

            var result = outputReceiver.Dequeue();
            Assert.IsTrue(outputReceiver.IsEmpty());

            return result;
        }

        [Test]
        [TestCase(new[] { 3, 0, 4, 0, 99 }, 50, ExpectedResult = 50)]
        [TestCase(new[] { 3, 0, 4, 0, 99 }, 60, ExpectedResult = 60)]
        public int Outputs_whatever_was_input(int[] instructions, int input)
        {
            var inputSender = new QueuedInputSenderBuilder().Build();
            inputSender.Enqueue(input);
            var outputReceiver = new QueuedOutputReceiverBuilder().Build();
            var computer = 
                new IntcodeComputerBuilder()
                    .WithInputSender(inputSender)
                    .WithOutputReceiver(outputReceiver)
                    .Build();
            
            computer.Run(instructions);

            var result = outputReceiver.Dequeue();
            Assert.IsTrue(outputReceiver.IsEmpty());

            return result;
        }
    }
}