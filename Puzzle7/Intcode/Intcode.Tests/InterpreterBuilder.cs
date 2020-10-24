namespace Intcode.Tests
{
    using Inforigami.Regalo.Testing;

    public class InterpreterBuilder : TestDataBuilderBase<Interpreter>
    {
        protected override Interpreter CreateInstance()
        {
            return new Interpreter();
        }
    }
}