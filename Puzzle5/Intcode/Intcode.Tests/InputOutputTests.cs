namespace Intcode.Tests
{
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
            var interpreter = new InterpreterBuilder().WithInputSender(inputSender).Build();
            var program = interpreter.Interpret($"3,{location},99"); // Take an input and put the value at position 0
            program.Run();

            return program.GetValue(location);
        }

        [Test]
        [TestCase(3, ExpectedResult = 50)]
        [TestCase(4, ExpectedResult = 60)]
        public int Returns_output_defined_by_program(int location)
        {
            IOutputReceiver outputReceiver = new QueuedOutputReceiverBuilder().Build();
            var interpreter = new InterpreterBuilder().WithOutputReceiver(outputReceiver).Build();
            var program = interpreter.Interpret($"4,{location},99,50,60");
            program.Run();

            var result = outputReceiver.Dequeue();
            Assert.IsTrue(outputReceiver.IsEmpty());

            return result;
        }

        [Test]
        [TestCase("3,0,4,0,99", 50, ExpectedResult = 50)]
        [TestCase("3,0,4,0,99", 60, ExpectedResult = 60)]
        public int Outputs_whatever_was_input(string code, int input)
        {
            var inputSender = new QueuedInputSenderBuilder().Build();
            inputSender.Enqueue(input);
            var outputReceiver = new QueuedOutputReceiverBuilder().Build();
            var interpreter = 
                new InterpreterBuilder()
                    .WithInputSender(inputSender)
                    .WithOutputReceiver(outputReceiver)
                    .Build();
            
            var program = interpreter.Interpret(code);
            program.Run();

            var result = outputReceiver.Dequeue();
            Assert.IsTrue(outputReceiver.IsEmpty());

            return result;
        }
    }
}