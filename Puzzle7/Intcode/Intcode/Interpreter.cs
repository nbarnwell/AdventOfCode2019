namespace Intcode
{
    using System;
    using System.Linq;

    public class Interpreter : IInterpreter
    {
        public int[] Interpret(string code)
        {
            var memory =
                code.Split(',')
                    .Select(x => Convert.ToInt32(x))
                    .ToArray();

            return memory;
        }
    }
}