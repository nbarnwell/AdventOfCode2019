namespace Intcode.Tests
{
    using Inforigami.Regalo.Testing;

    public class QueuedInputSenderBuilder : TestDataBuilderBase<QueuedInputSender>
    {
        protected override QueuedInputSender CreateInstance()
        {
            return new QueuedInputSender();
        }

        public QueuedInputSenderBuilder WithQueuedValues(params int[] values)
        {
            AddAction(
                x =>
                    {
                        foreach (var value in values)
                        {
                            x.Enqueue(value);
                        }
                    },
                "Input sender with preset values: " + string.Join(',', values));

            return this;
        }
    }
}