namespace Intcode
{
    using System.Collections.Generic;
    using System.Linq;

    public class Instruction
    {
        private readonly ParameterMode[] _parameterModes;

        public int Opcode { get; }

        public Instruction(int opcode, IEnumerable<ParameterMode> parameterModes)
        {
            _parameterModes = parameterModes.ToArray();
            Opcode = opcode;
        }

        public ParameterMode GetParameterMode(int index)
        {
            if (index <= _parameterModes.GetUpperBound(0))
            {
                return _parameterModes[index];
            }

            return ParameterMode.Position;
        }
    }
}