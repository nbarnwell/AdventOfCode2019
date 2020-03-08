using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace OrbitSystem
{
    public class AstronomicalChartBuilder
    {
        public AstronomicalChart Build(string input)
        {
            input = input.Trim();
            var commands = Split(input).ToList();
            var objects = new Dictionary<string, AstronomicalObject>();

            AstronomicalChart result = null;
            
            foreach (var command in commands)
            {
                AstronomicalObject primary;
                if (!objects.TryGetValue(command.Item1, out primary))
                {
                    primary = new AstronomicalObject(command.Item1);
                    objects.Add(primary.Name, primary);
                }

                if (result == null && primary.Name == "COM")
                {
                    result = new AstronomicalChart(primary);
                }

                AstronomicalObject satellite;
                if (!objects.TryGetValue(command.Item2, out satellite))
                {
                    satellite = new AstronomicalObject(command.Item2);
                    objects.Add(satellite.Name, satellite);
                }

                primary.AddSatellite(satellite);
            }

            return result;
        }

        private IEnumerable<Tuple<string, string>> Split(string input)
        {
            return
                Regex.Split(input.Trim(), "\n")
                     .Select(x => x.Trim())
                     .Where(x => x.Length > 0)
                     .Select(x => 
                     {
                         var keys = x.Split(')');
                         return Tuple.Create(keys[0].Trim(), keys[1].Trim());
                     });
        }
    }
}