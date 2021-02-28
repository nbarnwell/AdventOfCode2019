using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Intcode.Tests
{
    public class Amplifier
    {
        private readonly IntcodeComputer _computer;
        private readonly int[] _instructions;

        public Amplifier(IntcodeComputer computer, int[] instructions)
        {
            _computer = computer ?? throw new ArgumentNullException(nameof(computer));
            _instructions = instructions ?? throw new ArgumentNullException(nameof(instructions));
        }

        public void Run()
        {
            _computer.Run(_instructions);
        }
    }

    [TestFixture]
    public class ThrusterPowerAmplifierTests
    {
        [Test]
        public void Permutations()
        {
            var permutations =
                new[] {1, 2, 3}.GetPermutations();

            CollectionAssert.AreEquivalent(
                new[]
                {
                    new[] {1, 2, 3},
                    new[] {1, 3, 2},
                    new[] {2, 1, 3},
                    new[] {3, 2, 1}
                },
                permutations);
        }
        [Test]
        public void X()
        {
            //var code = File.ReadLines(".\\Puzzle5.txt").First();
            var code = "3,15,3,16,1002,16,10,16,1,16,15,15,4,15,99,0,0";
            var interpreter = new InterpreterBuilder().Build();
            var instructions = interpreter.Interpret(code);

            int result =
                Enumerable.Range(0, 5)
                          .GetPermutations()
                          .Select(phaseSettingPermutation =>
                          {
                              var test = new List<Amplifier>();
                              IInputSender connector = null;
                              foreach (var phaseSetting in phaseSettingPermutation)
                              {
                                  var inputSender = connector ?? new ReceiverSenderConnector();
                                  var outputReceiver = new ReceiverSenderConnector();

                                  inputSender.Enqueue(phaseSetting);

                                  if (connector == null)
                                  {
                                      inputSender.Enqueue(0);
                                  }

                                  var parser = new InstructionParser();
                                  var computer = new IntcodeComputer(inputSender, outputReceiver, parser);
                                  test.Add(new Amplifier(computer, instructions));

                                  connector = outputReceiver;
                              }

                              foreach (var amplifier in test)
                              {
                                  amplifier.Run();
                              }

                              return connector.Dequeue();
                          })
                          .Largest();

            Assert.AreEqual(43210, result);
        }
    }
}
