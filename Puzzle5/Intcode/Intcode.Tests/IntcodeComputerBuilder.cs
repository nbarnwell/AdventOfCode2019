namespace Intcode.Tests 
{
    using Inforigami.Regalo.Testing;

    public class IntcodeComputerBuilder : TestDataBuilderBase<IntcodeComputer>
    {
        private IInputSender _inputSender;
        private IOutputReceiver _outputReceiver;
        private IInstructionParser _instructionParser;

        public IntcodeComputerBuilder WithInputSender(IInputSender inputSender)
        {
            _inputSender = inputSender;

            return this;
        }

        public IntcodeComputerBuilder WithOutputReceiver(IOutputReceiver outputReceiver)
        {
            _outputReceiver = outputReceiver;

            return this;
        }

        public IntcodeComputerBuilder WithInstructionParser(IInstructionParser instructionParser)
        {
            _instructionParser = instructionParser;

            return this;
        }

        protected override IntcodeComputer CreateInstance()
        {
            return new IntcodeComputer(
                _inputSender ?? new QueuedInputSenderBuilder().Build(),
                _outputReceiver ?? new QueuedOutputReceiverBuilder().Build(),
                _instructionParser ?? new InstructionParserBuilder().Build());
        }
    }
}