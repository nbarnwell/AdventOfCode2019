namespace Intcode
{
    public interface IOutputReceiver
    {
        int Dequeue();

        void Enqueue(int value);

        bool IsEmpty();
    }
}