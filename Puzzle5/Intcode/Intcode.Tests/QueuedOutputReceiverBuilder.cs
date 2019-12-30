namespace Intcode.Tests
{
    using Inforigami.Regalo.Testing;

    public class QueuedOutputReceiverBuilder : TestDataBuilderBase<QueuedOutputReceiver>
    {
        protected override QueuedOutputReceiver CreateInstance()
        {
            return new QueuedOutputReceiver();
        }
    }
}