using System;

namespace OrbitSystem.Shell
{
    using System.IO;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            var chartBuilder = new AstronomicalChartBuilder();
            var input = File.ReadAllText(args[0]);
            
            var chart = chartBuilder.Build(input);

            var exporter = new DotExporter();
            exporter.Export(args[0] + ".exported.dot", chart);
        }
    }

    internal class DotExporter 
    {
        public async void Export(string outputFile, AstronomicalChart chart)
        {
            using (var writer = new StreamWriter(outputFile))
            {
                await writer.WriteLineAsync("digraph D {");

                await Write(chart.Root, writer);

                await writer.WriteLineAsync("}");
            }
        }

        private async Task Write(AstronomicalObject item, StreamWriter writer)
        {
            foreach (var satellite in item.Satellites)
            {
                await writer.WriteLineAsync($"   n_{item.Name} -> n_{satellite.Name}");

                await Write(satellite, writer);
            }
        }
    }
}
