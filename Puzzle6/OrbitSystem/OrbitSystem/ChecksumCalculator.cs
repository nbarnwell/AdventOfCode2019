using System;

namespace OrbitSystem
{
    using System.Collections;
    using System.Collections.Generic;

    public class ChecksumCalculator
    {
        public int GetChecksum(AstronomicalChart chart)
        {
            var total = GetChecksum(chart.Root, 0);

            return total;
        }

        private int GetChecksum(AstronomicalObject start, int depth)
        {
            int total = 0;
            foreach (var satellite in start.Satellites)
            {
                total += GetChecksum(satellite, depth + 1);
            }

            return depth + total;
        }
    }
}