namespace Intcode.Cli
{
    using System;
    using System.IO;
    using System.Linq;

    public static class Program
    {
        public static void Main()
        {
            var code = File.ReadLines("C:\\Code\\github\\AdventOfCode2019\\Puzzle2\\input.txt").First();
            var interpreter = new Interpreter(new QueuedInputSender(), new QueuedOutputReceiver());

            bool finished = false;
            for (int noun = 0; noun < 99 && !finished; noun++)
            {
                for (int verb = 0; verb < 99 && !finished; verb++)
                {
                    var program = interpreter.Interpret(code);
                    program.SetValue(1, noun);
                    program.SetValue(2, verb);
                    program.Run();

                    if (program.GetValue(0) == 19690720)
                    {
                        var puzzleAnswer = (100 * noun) + verb;
                        Console.WriteLine($"Puzzle 2 part 2 answer: noun = {noun}, verb = {verb}, answer = {puzzleAnswer}");
                        finished = true;
                    }
                }
            }
        }
    }
}
