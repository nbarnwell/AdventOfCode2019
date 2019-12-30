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

        public void SetDereferencedValue(int pointer, int value)
        {
            SetValue(GetValue(pointer), value);
        }

        public int GetDereferencedValue(int pointer)
        {
            return GetValue(GetValue(pointer));
        }

        public int GetValue(int pointer, ParameterMode parameterMode)
        {
            switch (parameterMode)
            {
                case ParameterMode.Position:
                    return GetValue(pointer);
                case ParameterMode.Immediate:
                    return GetDereferencedValue(pointer);
                default:
                    throw new InvalidEnumArgumentException(nameof(parameterMode), (int)parameterMode, typeof(ParameterMode));
            }
        }

        public void SetValue(int pointer, int value, ParameterMode parameterMode)
        {
            switch (parameterMode)
            {
                case ParameterMode.Position:
                    SetValue(pointer, value);
                    break;
                case ParameterMode.Immediate:
                    SetDereferencedValue(pointer, value);
                    break;
                default:
                    throw new InvalidEnumArgumentException(nameof(parameterMode), (int)parameterMode, typeof(ParameterMode));
            }
        }
    }
}