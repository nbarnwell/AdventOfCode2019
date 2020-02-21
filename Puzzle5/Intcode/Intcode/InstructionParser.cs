namespace Intcode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class InstructionParser : IInstructionParser
    {
        public Instruction Parse(int value)
        {
            var opcode = TakeLastTwoDigits(value);
            var parameterModeValues = TakeEverythingButLastTwoDigits(value);
            var parameterModes = 
                NumberToDigits(parameterModeValues)
                   .Cast<ParameterMode>()
                   .ToArray();

            var result = new Instruction(opcode, parameterModes);

            return result;
        }

        private static int TakeEverythingButLastTwoDigits(int input)
        {
            return Convert.ToInt32(input / 100);
        }

        private static int TakeLastTwoDigits(int input)
        {
            return input % 100;
        }

        private IEnumerable<int> NumberToDigits(int number)
        {
            if (number > 0)
            {
                while (number > 0)
                {
                    var digit = number % 10;

                    yield return digit;

                    number -= digit;
                    number /= 10;
                }
            }
        }
    }
}