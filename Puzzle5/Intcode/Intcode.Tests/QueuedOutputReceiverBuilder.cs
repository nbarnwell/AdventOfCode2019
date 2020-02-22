namespace Intcode.Tests
{
    using Inforigami.Regalo.Testing;

    public class QueuedOutputReceiverBuilder : TestDataBuilderBase<QueuedOutputReceiver>
    {
        protected override QueuedOutputReceiver CreateInstance()
        {
            return new QueuedOutputReceiver();
        }

        public QueuedOutputReceiverBuilder ThatIgnoresZeros()
        {
            AddAction(x => x.AddFilter(i => i == 0), "ignores zero values");

            return this;
        }
    }
}