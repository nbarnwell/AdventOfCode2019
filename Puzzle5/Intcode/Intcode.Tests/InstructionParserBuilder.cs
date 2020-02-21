namespace Intcode.Tests {
    public class InstructionParserBuilder 
    {
        public IInstructionParser Build()
        {
            return new InstructionParser();
        }
    }
}