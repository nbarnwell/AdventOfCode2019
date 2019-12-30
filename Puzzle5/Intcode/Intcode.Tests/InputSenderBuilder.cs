namespace Intcode.Tests
{
    using Inforigami.Regalo.Testing;

    public class InputSenderBuilder : TestDataBuilderBase<IInputSender>
    {
        protected override IInputSender CreateInstance()
        {
            return new QueuedInputSender();
        }
    }
}