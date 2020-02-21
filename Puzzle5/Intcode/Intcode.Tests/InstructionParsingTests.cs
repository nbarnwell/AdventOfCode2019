namespace Intcode.Tests
{
    using NUnit.Framework;

    [TestFixture]
    public class InstructionParsingTests
    {
        [Test]
        //[TestCase(1, 1, new[] { ParameterMode.Position, ParameterMode.Position })]
        //[TestCase(1101, 1, new[] { ParameterMode.Immediate, ParameterMode.Immediate })]
        [TestCase(1001, 1, new[] { ParameterMode.Position, ParameterMode.Immediate })]
        //[TestCase(0101, 1, new[] { ParameterMode.Immediate, ParameterMode.Position })]
        public void Correctly_parse_simple_add(int value, int expectedOpcode, ParameterMode[] expectedParameterModes)
        {
            var parser = new InstructionParserBuilder().Build();

            var instruction = parser.Parse(value);

            Assert.AreEqual(expectedOpcode, instruction.Opcode);

            for (int i = 0; i < expectedParameterModes.Length; i++)
            {
                Assert.AreEqual(expectedParameterModes[i], instruction.GetParameterMode(i), $"Parameter {i}");
            }
        }
    }
}