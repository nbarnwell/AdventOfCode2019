using System;
using System.Collections.Generic;
using System.Linq;

namespace Intcode
{
    public class ReceiverSenderConnector : IOutputReceiver, IInputSender
    {
        private readonly Queue<int> _queue = new Queue<int>();

        public void Enqueue(int input)
        {
            _queue.Enqueue(input);
        }

        public int Dequeue()
        {
            return _queue.Dequeue();
        }

        public bool IsEmpty()
        {
            return _queue.Any();
        }
    }
}