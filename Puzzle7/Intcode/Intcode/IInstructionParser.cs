namespace Intcode {
    public interface IInstructionParser
    {
        Instruction Parse(int instructionPointer);
    }
}