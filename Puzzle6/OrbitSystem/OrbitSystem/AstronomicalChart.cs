namespace OrbitSystem
{
    public class AstronomicalChart
    {
        public AstronomicalObject Root { get; }

        public AstronomicalChart(AstronomicalObject root)
        {
            Root = root;
        }
    }
}