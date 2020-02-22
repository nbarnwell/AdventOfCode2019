namespace Intcode 
{
    using System;
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

        public int GetValueImmediate(int address)
        {
            var value = GetValue(address);
            Console.WriteLine("GetValueImmediate({0}) = {1}", address, value);
            return value;
        }

        public void SetValueImmediate(int address, int value)
        {
            Console.WriteLine("SetValueImmediate({0}, {1})", address, value);
            SetValue(address, value);
        }

        public void SetValueByLocation(int address, int value)
        {
            var dereferencedAddress = GetValue(address);
            Console.WriteLine("SetValueByLocation({0}={1}), {2}", address, dereferencedAddress, value);
            SetValue(dereferencedAddress, value);
        }

        public int GetValueByLocation(int address)
        {
            var dereferencedAddress = GetValue(address);
            var result = GetValue(dereferencedAddress);
            Console.WriteLine("GetValueByLocation({0}={1}) = {2}", address, dereferencedAddress, result);
            return result;
        }

        public int GetValue(int address, ParameterMode parameterMode)
        {
            switch (parameterMode)
            {
                case ParameterMode.Position:
                    return GetValueByLocation(address);
                case ParameterMode.Immediate:
                    return GetValueImmediate(address);
                default:
                    throw new InvalidEnumArgumentException(nameof(parameterMode), (int)parameterMode, typeof(ParameterMode));
            }
        }

        public void SetValue(int address, int value, ParameterMode parameterMode)
        {
            switch (parameterMode)
            {
                case ParameterMode.Position:
                    SetValueByLocation(address, value);
                    break;
                case ParameterMode.Immediate:
                    SetValueImmediate(address, value);
                    break;
                default:
                    throw new InvalidEnumArgumentException(nameof(parameterMode), (int)parameterMode, typeof(ParameterMode));
            }
        }

        private int GetValue(int address)
        {
            return IsValidAddress(address) ? _memory[address] : 0;
        }

        private void SetValue(int address, int value)
        {
            var maxIndex = _memory.Count - 1;
            if (address > maxIndex)
            {
                var difference = address - maxIndex;
                Console.WriteLine("Expanding Memory {0}+{1}={2}", _memory.Count, difference, _memory.Count + difference);
                _memory.AddRange(Enumerable.Repeat(0, difference));
            }

            _memory[address] = value;
        }
    }
}