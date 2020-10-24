namespace Intcode 
{
    public interface IInterpreter 
    {
        int[] Interpret(string code);
    }
}