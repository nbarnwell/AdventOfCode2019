using System;
using System.Linq;

namespace Intcode
{
    public class Interpreter
    {
        public IntcodeComputer Interpret(string code)
        {
            var memory =
                code.Split(',')
                    .Select(x => Convert.ToInt32((string?) x))
                    .ToArray();
            var program = new IntcodeComputer(memory);
            return program;
        }
    }
}