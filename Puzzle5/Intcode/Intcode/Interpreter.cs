namespace Intcode
{
    using System;
    using System.Linq;

    public class Interpreter
    {
        private readonly IInputSender _inputSender;
        private readonly IOutputReceiver _outputReceiver;

        public Interpreter(IInputSender inputSender, IOutputReceiver outputReceiver)
        {
            //if (inputSender == null) throw new ArgumentNullException(nameof(inputSender));
            //if (outputReceiver == null) throw new ArgumentNullException(nameof(outputReceiver));

            _inputSender = inputSender;
            _outputReceiver = outputReceiver;
        }

        public IntcodeComputer Interpret(string code)
        {
            var memory =
                code.Split(',')
                    .Select(x => Convert.ToInt32(x))
                    .ToArray();
            var program = new IntcodeComputer(memory, _inputSender, _outputReceiver);
            return program;
        }
    }
}