namespace Intcode.Tests
{
    using System.IO;
    using System.Linq;
    using NUnit.Framework;

    [TestFixture]
    public class PuzzleAnswers
    {
        [Test]
        public void Puzzle2_day1_answer()
        {
            var code = File.ReadLines("C:\\Code\\github\\AdventOfCode2019\\Puzzle2\\input.txt").First();
            var interpreter = new InterpreterBuilder().Build();
            var instructions = interpreter.Interpret(code);
            instructions[1] = 12;
            instructions[2] = 2;
            var computer = new IntcodeComputerBuilder().Build();
            computer.Run(instructions);

            Assert.AreEqual(4576384, computer.Memory.GetValueImmediate(0));
        }

        [Test]
        public void Puzzle2_day2_answer()
        {
            var code = File.ReadLines("C:\\Code\\github\\AdventOfCode2019\\Puzzle2\\input.txt").First();
            var interpreter = new InterpreterBuilder().Build();
            var instructions = interpreter.Interpret(code);
            instructions[1] = 53;
            instructions[2] = 98;
            var computer = new IntcodeComputerBuilder().Build();
            computer.Run(instructions);

            Assert.AreEqual(19690720, computer.Memory.GetValueImmediate(0));
        }

        [Test]
        public void Puzzle5_day1_answer()
        {
            var code = File.ReadLines("C:\\Code\\github\\AdventOfCode2019\\Puzzle5\\input.txt").First();
            var interpreter = new InterpreterBuilder().Build();
            var instructions = interpreter.Interpret(code);
            var inputSender = new QueuedInputSenderBuilder().Build();
            var outputReceiver = new QueuedOutputReceiverBuilder().ThatIgnoresZeros().Build();
            var computer = new IntcodeComputerBuilder().WithInputSender(inputSender).WithOutputReceiver(outputReceiver).Build();

            inputSender.Enqueue(1);
            computer.Run(instructions);

            Assert.AreEqual(6761139, outputReceiver.Dequeue());
        }
    }
}