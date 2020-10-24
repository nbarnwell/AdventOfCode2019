namespace Intcode
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class QueuedOutputReceiver : IOutputReceiver
    {
        private readonly Queue<int> _queue = new Queue<int>();
        private readonly List<Predicate<int>> _filters = new List<Predicate<int>>();

        public void AddFilter(Predicate<int> filter)
        {
            _filters.Add(filter);
        }

        public int Dequeue()
        {
            return _queue.Dequeue();
        }

        public void Enqueue(int value)
        {
            if (!_filters.Any(x => x(value)))
            {
                _queue.Enqueue(value);
            }
        }

        public bool IsEmpty()
        {
            return _queue.Count == 0;
        }
    }
}