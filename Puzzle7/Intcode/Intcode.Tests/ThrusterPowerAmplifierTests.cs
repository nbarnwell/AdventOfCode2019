using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace Intcode.Tests
{
    public class Amplifier
    {
        private readonly IntcodeComputer _computer;

        public Amplifier(IntcodeComputer computer)
        {
            _computer = computer ?? throw new ArgumentNullException(nameof(computer));
        }

        public void Run()
        {
            _computer.Run();
        }
    }

    [TestFixture]
    public class ThrusterPowerAmplifierTests
    {
        [Test]
        public void X()
        {
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
                              test.Add(new Amplifier(computer));

                              connector = outputReceiver;
                          }

                          foreach (var amplifier in test)
                          {
                              amplifier.Run();
                          }

                          return test;
                      });
        }
    }
}
