namespace Intcode
{
    using System;
    using System.Linq;

    public class Interpreter
    {
        private readonly IInputSender _inputSender;

        public Interpreter(IInputSender inputSender)
        {
            if (inputSender == null) throw new ArgumentNullException(nameof(inputSender));
            _inputSender = inputSender;
        }

        public IntcodeComputer Interpret(string code)
        {
            var memory =
                code.Split(',')
                    .Select(x => Convert.ToInt32(x))
                    .ToArray();
            var program = new IntcodeComputer(memory, _inputSender);
            return program;
        }
    }
}