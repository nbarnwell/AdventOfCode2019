namespace Intcode
{
    using System;
    using System.Collections.Generic;

    public class IntcodeComputer
    {
        private readonly List<int> _memory = new List<int>();
        private int _instructionPointer;
        private bool _exitSignalled;

        public IntcodeComputer(int[] memory)
        {
            if (memory == null) throw new ArgumentNullException(nameof(memory));
            _memory.AddRange(memory);
        }

        public void Run()
        {
            while (!_exitSignalled)
            {
                var opcode = GetValue(_instructionPointer);

                switch (opcode)
                {
                    case 1:
                        Add();
                        break;
                    case 2:
                        Multiply();
                        break;
                    case 99:
                        Exit();
                        break;
                    default:
                        throw new InvalidOperationException(
                            $"Unknown opcode {opcode} at address {_instructionPointer}");
                }
            }

            _exitSignalled = true;
        }

        public void SetValue(int address, int value)
        {
            _memory[address] = value;
        }

        public int GetValue(int address)
        {
            return address > _memory.Count ? 0 : _memory[address];
        }

        private void Exit()
        {
            _exitSignalled = true;
        }

        private void Multiply()
        {
            var term1 = GetDereferencedValue(_instructionPointer + 1);
            var term2 = GetDereferencedValue(_instructionPointer + 2);
            var result = term1 * term2;
            SetDereferencedValue(_instructionPointer + 3, result);
            Goto(_instructionPointer + 4);
        }

        private void Add()
        {
            var term1 = GetDereferencedValue(_instructionPointer + 1);
            var term2 = GetDereferencedValue(_instructionPointer + 2);
            var result = term1 + term2;
            SetDereferencedValue(_instructionPointer + 3, result);
            Goto(_instructionPointer + 4);
        }

        private void Goto(int pointer)
        {
            _instructionPointer = pointer;
        }

        private void SetDereferencedValue(int pointer, int value)
        {
            SetValue(GetValue(pointer), value);
        }

        private int GetDereferencedValue(int pointer)
        {
            return GetValue(GetValue(pointer));
        }
    }
}