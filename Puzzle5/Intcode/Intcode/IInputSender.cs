namespace Intcode
{
    public interface IInputSender
    {
        void Enqueue(int input);

        int Dequeue();
    }
}