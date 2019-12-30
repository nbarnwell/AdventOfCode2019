namespace Intcode
{
    using System;

    public class IntcodeComputer
    {
        public readonly MemoryBank Memory = new MemoryBank();

        private readonly IInputSender _inputSender;
        private readonly IOutputReceiver _outputReceiver;
        private int _instructionPointer;
        private bool _exitSignalled;

        public IntcodeComputer(IInputSender inputSender, IOutputReceiver outputReceiver)
        {
            _inputSender = inputSender;
            _outputReceiver = outputReceiver;
        }

        public void Run(int[] instructions)
        {
            Memory.Initialise(instructions);

            while (!_exitSignalled)
            {
                var opcode = Memory.GetValue(_instructionPointer);

                switch (opcode)
                {
                    case 1:
                        Add();
                        break;
                    case 2:
                        Multiply();
                        break;
                    case 3:
                        GetInput();
                        break;
                    case 4:
                        SetOutput();
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

        private void Exit()
        {
            _exitSignalled = true;
        }

        private void SetOutput()
        {
            var value = Memory.GetDereferencedValue(_instructionPointer + 1);
            _outputReceiver.Enqueue(value);
            Goto(_instructionPointer + 2);
        }

        private void GetInput()
        {
            // Get input and save at location specified by arg1
            var saveLocation = Memory.GetValue(_instructionPointer + 1);
            var input = _inputSender.Dequeue();
            Memory.SetValue(saveLocation, input);
            Goto(_instructionPointer + 2);
        }

        private void Multiply()
        {
            var term1 = Memory.GetDereferencedValue(_instructionPointer + 1);
            var term2 = Memory.GetDereferencedValue(_instructionPointer + 2);
            var result = term1 * term2;
            Memory.SetDereferencedValue(_instructionPointer + 3, result);
            Goto(_instructionPointer + 4);
        }

        private void Add()
        {
            var term1 = Memory.GetDereferencedValue(_instructionPointer + 1);
            var term2 = Memory.GetDereferencedValue(_instructionPointer + 2);
            var result = term1 + term2;
            Memory.SetDereferencedValue(_instructionPointer + 3, result);
            Goto(_instructionPointer + 4);
        }

        private void Goto(int pointer)
        {
            var maxIndex = Memory.MaxIndex;
            if (pointer > maxIndex)
            {
                throw new IndexOutOfRangeException($"There is no value at {pointer}. Are you missing an Exit (99)?");
            }
            _instructionPointer = pointer;
        }
    }
}