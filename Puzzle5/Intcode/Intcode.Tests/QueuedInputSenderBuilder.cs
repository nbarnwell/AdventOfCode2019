namespace Intcode.Tests
{
    using Inforigami.Regalo.Testing;

    public class QueuedInputSenderBuilder : TestDataBuilderBase<QueuedInputSender>
    {
        protected override QueuedInputSender CreateInstance()
        {
            return new QueuedInputSender();
        }
    }
}