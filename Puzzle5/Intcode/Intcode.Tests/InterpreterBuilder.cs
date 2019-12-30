namespace Intcode.Tests
{
    using Inforigami.Regalo.Testing;

    public class InterpreterBuilder : TestDataBuilderBase<Interpreter>
    {
        public ITestDataBuilder<IInputSender> InputSenderBuilder { get; } = new InputSenderBuilder();

        protected override Interpreter CreateInstance()
        {
            return new Interpreter(InputSenderBuilder.Build());
        }
    }
}