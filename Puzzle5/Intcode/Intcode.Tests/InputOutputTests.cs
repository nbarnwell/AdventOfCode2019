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
            IInputSender inputSender = new QueuedInputSender();
            inputSender.Enqueue(input);
            var interpreter = new Interpreter(inputSender);
            var program = interpreter.Interpret($"3,{location},99"); // Take an input and put the value at position 0
            program.Run();

            return program.GetValue(location);
        }
    }
}