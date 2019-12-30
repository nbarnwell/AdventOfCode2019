namespace Intcode.Tests 
{
    using Inforigami.Regalo.Testing;

    public class IntcodeComputerBuilder : TestDataBuilderBase<IntcodeComputer>
    {
        private IInputSender _inputSender;
        private IOutputReceiver _outputReceiver;

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

        protected override IntcodeComputer CreateInstance()
        {
            return new IntcodeComputer(
                _inputSender ?? new QueuedInputSenderBuilder().Build(),
                _outputReceiver ?? new QueuedOutputReceiverBuilder().Build());
        }
    }
}