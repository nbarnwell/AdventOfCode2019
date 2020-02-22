namespace Intcode
{
    using System;
    using System.Collections.Generic;

    public class QueuedOutputReceiver : IOutputReceiver
    {
        private readonly Queue<int> _queue = new Queue<int>();

        public int Dequeue()
        {
            return _queue.Dequeue();
        }

        public void Enqueue(int value)
        {
            if (value == 3)
            {
                throw new InvalidOperationException("Should always be zero for this test");
            }
            _queue.Enqueue(value);
        }

        public bool IsEmpty()
        {
            return _queue.Count == 0;
        }
    }
}