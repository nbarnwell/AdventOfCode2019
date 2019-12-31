namespace Intcode 
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;

    public class MemoryBank
    {
        private readonly List<int> _memory = new List<int>();

        public int MaxIndex => _memory.Count - 1;

        public void Initialise(int[] memory)
        {
            _memory.Clear();
            _memory.AddRange(memory);
        }

        public bool IsValidAddress(int address)
        {
            return address >= 0 && address <= MaxIndex;
        }

        public int GetValue(int address)
        {
            return IsValidAddress(address) ? _memory[address] : 0;
        }

        public void SetValue(int address, int value)
        {
            var maxIndex = _memory.Count - 1;
            if (address > maxIndex)
            {
                _memory.AddRange(Enumerable.Repeat(0, address - maxIndex));
            }
            _memory[address] = value;
        }

        public void SetDereferencedValue(int address, int value)
        {
            SetValue(GetValue(address), value);
        }

        public int GetDereferencedValue(int address)
        {
            return GetValue(GetValue(address));
        }

        public int GetValue(int address, ParameterMode parameterMode)
        {
            switch (parameterMode)
            {
                case ParameterMode.Position:
                    return GetDereferencedValue(address);
                case ParameterMode.Immediate:
                    return GetValue(address);
                default:
                    throw new InvalidEnumArgumentException(nameof(parameterMode), (int)parameterMode, typeof(ParameterMode));
            }
        }

        public void SetValue(int address, int value, ParameterMode parameterMode)
        {
            switch (parameterMode)
            {
                case ParameterMode.Position:
                    SetDereferencedValue(address, value);
                    break;
                case ParameterMode.Immediate:
                    SetValue(address, value);
                    break;
                default:
                    throw new InvalidEnumArgumentException(nameof(parameterMode), (int)parameterMode, typeof(ParameterMode));
            }
        }
    }
}