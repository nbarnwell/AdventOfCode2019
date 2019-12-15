using System;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace Intcode.Tests
{
    [TestFixture]
    public class ComputationTests
    {
        [Test]
        public void Simple_addition()
        {
            var code = "1,0,0,0,99";
            var interpreter = new Interpreter();
            var program = interpreter.Interpret(code);
            program.Run();

            Assert.AreEqual(2, program.GetValue(0));
        }

        [Test]
        public void Simple_multiplication()
        {
            var code = "2,0,0,0,99";
            var interpreter = new Interpreter();
            var program = interpreter.Interpret(code);
            program.Run();

            Assert.AreEqual(4, program.GetValue(0));
        }

        [Test]
        public void Multiple_instructions()
        {
            var code = "1,1,1,4,99,5,6,0,99";
            var interpreter = new Interpreter();
            var program = interpreter.Interpret(code);
            program.Run();

            Assert.AreEqual(30, program.GetValue(0));
        }

        [Test]
        [TestCase("-1,0,0,0,99", Description = "First instruction is invalid")]
        [TestCase("1,0,0,0,-1", Description = "Second instruction is invalid")]
        public void Invalid_opcode_shouldthrow(string code)
        {
            var interpreter = new Interpreter();
            var program = interpreter.Interpret(code);

            Assert.Throws<InvalidOperationException>(() => program.Run());
        }

        [Test]
        public void Restore_gravity_assist()
        {
            var code = File.ReadLines("C:\\Code\\github\\AdventOfCode2019\\Puzzle2\\input.txt").First();
            var interpreter = new Interpreter();
            var program = interpreter.Interpret(code);
            program.SetValue(1, 12);
            program.SetValue(2, 2);
            program.Run();

            Assert.AreEqual(4576384, program.GetValue(0));
        }

        [Test]
        public void Puzzle2_day2_answer()
        {
            var code = File.ReadLines("C:\\Code\\github\\AdventOfCode2019\\Puzzle2\\input.txt").First();
            var interpreter = new Interpreter();
            var program = interpreter.Interpret(code);
            program.SetValue(1, 53);
            program.SetValue(2, 98);
            program.Run();

            Assert.AreEqual(19690720, program.GetValue(0));
        }
    }
}