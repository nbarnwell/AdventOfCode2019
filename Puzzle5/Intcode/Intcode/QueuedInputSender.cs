namespace Intcode
{
    using System.Collections.Generic;

    public class QueuedInputSender : IInputSender
    {
        private readonly Queue<int> _queue = new Queue<int>();

        public void Enqueue(int input)
        {
            this._queue.Enqueue(input);
        }

        public int Dequeue()
        {
            return _queue.Dequeue();
        }
    }
}