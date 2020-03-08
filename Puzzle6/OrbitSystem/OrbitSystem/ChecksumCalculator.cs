namespace OrbitSystem
{
    public class ChecksumCalculator
    {
        public int GetChecksum(AstronomicalChart chart)
        {
            var total = GetChecksum(chart.Root, 1);

            return total;
        }

        private int GetChecksum(AstronomicalObject start, int depth)
        {
            int total = 0;
            foreach (var satellite in start.Satellites)
            {
                total += depth;
                total += GetChecksum(satellite, depth + 1);
            }

            return total;
        }
    }
}