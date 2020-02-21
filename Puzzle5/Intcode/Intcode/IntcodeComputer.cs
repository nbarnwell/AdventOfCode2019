namespace Intcode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class IntcodeComputer
    {
        public readonly MemoryBank Memory = new MemoryBank();

        private readonly IInputSender _inputSender;
        private readonly IOutputReceiver _outputReceiver;
        private readonly IInstructionParser _instructionParser;
        private int _instructionPointer;
        private bool _exitSignalled;

        public IntcodeComputer(IInputSender inputSender, IOutputReceiver outputReceiver, IInstructionParser instructionParser)
        {
            _inputSender = inputSender;
            _outputReceiver = outputReceiver;
            _instructionParser = instructionParser;
        }

        public void Run(int[] instructions)
        {
            Memory.Initialise(instructions);

            while (!_exitSignalled)
            {
                var instruction = _instructionParser.Parse(Memory.GetValue(_instructionPointer));

                switch (instruction.Opcode)
                {
                    case 1:
                        Add(instruction);
                        break;
                    case 2:
                        Multiply(instruction);
                        break;
                    case 3:
                        GetInput(instruction);
                        break;
                    case 4:
                        SetOutput(instruction);
                        break;
                    case 99:
                        Exit(instruction);
                        break;
                    default:
                        throw new InvalidOperationException(
                            $"Unknown opcode {instruction.Opcode} at address {_instructionPointer}");
                }
            }

            _exitSignalled = true;
        }

        private void Exit(Instruction instruction)
        {
            _exitSignalled = true;
        }

        private void SetOutput(Instruction instruction)
        {
            var value = Memory.GetValue(_instructionPointer + 1, ParameterMode.Position);
            _outputReceiver.Enqueue(value);
            Goto(_instructionPointer + 2);
        }

        private void GetInput(Instruction instruction)
        {
            // Get input and save at location specified by arg1
            var saveLocation = Memory.GetValue(_instructionPointer + 1, ParameterMode.Immediate);
            var input = _inputSender.Dequeue();
            Memory.SetValue(saveLocation, input);
            Goto(_instructionPointer + 2);
        }

        private void Multiply(Instruction instruction)
        {
            var term1 = Memory.GetValue(_instructionPointer + 1, instruction.GetParameterMode(0));
            var term2 = Memory.GetValue(_instructionPointer + 2, instruction.GetParameterMode(1));
            
            var result = term1 * term2;
            
            Memory.SetDereferencedValue(_instructionPointer + 3, result);
            Goto(_instructionPointer + 4);
        }

        private void Add(Instruction instruction)
        {
            var term1 = Memory.GetValue(_instructionPointer + 1, instruction.GetParameterMode(0));
            var term2 = Memory.GetValue(_instructionPointer + 2, instruction.GetParameterMode(1));
            
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