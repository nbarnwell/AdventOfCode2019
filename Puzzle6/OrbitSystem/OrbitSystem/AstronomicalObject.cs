using System.Collections.Generic;

namespace OrbitSystem
{
    public class AstronomicalObject
    {
        private IList<AstronomicalObject> satellites = new List<AstronomicalObject>();

        public string Name { get; }
        public IEnumerable<AstronomicalObject> Satellites => satellites;

        public AstronomicalObject(string name)
        {
            Name = name;
        }

        public void AddSatellite(AstronomicalObject satellite)
        {
            this.satellites.Add(satellite);
        }
    }
}