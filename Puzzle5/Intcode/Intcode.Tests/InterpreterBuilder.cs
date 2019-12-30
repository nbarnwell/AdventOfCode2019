namespace Intcode.Tests
{
    using Inforigami.Regalo.Testing;

    public class InterpreterBuilder : TestDataBuilderBase<Interpreter>
    {
        private IInputSender _inputSender;
        private IOutputReceiver _outputReceiver;

        public InterpreterBuilder WithInputSender(IInputSender inputSender)
        {
            _inputSender = inputSender;

            return this;
        }

        public InterpreterBuilder WithOutputReceiver(IOutputReceiver outputReceiver)
        {
            _outputReceiver = outputReceiver;

            return this;
        }

        protected override Interpreter CreateInstance()
        {
            return new Interpreter(
                _inputSender ?? new QueuedInputSenderBuilder().Build(),
                _outputReceiver ?? new QueuedOutputReceiverBuilder().Build());
        }
    }
}