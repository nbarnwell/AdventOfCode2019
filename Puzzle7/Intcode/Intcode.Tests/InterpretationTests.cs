namespace Intcode.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class InterpretationTests
    {
        [Test]
        [TestCase("1,2,3", ExpectedResult = new[] { 1, 2, 3 })]
        [TestCase("-1,2,3", ExpectedResult = new[] { -1, 2, 3 })]
        [TestCase("1,99", ExpectedResult = new[] { 1, 99 })]
        [TestCase("1,999999999", ExpectedResult = new[] { 1, 999999999 })]
        public int[] Interprets_comma_separated_code(string code)
        {
            var interpreter = new InterpreterBuilder().Build();
            var instructions = interpreter.Interpret(code);

            return instructions;
        }
    }
}