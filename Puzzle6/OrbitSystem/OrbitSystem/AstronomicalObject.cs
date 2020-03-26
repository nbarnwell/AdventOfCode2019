using System.Collections.Generic;

namespace OrbitSystem
{
    using System;

    public class AstronomicalObject
    {
        private readonly IList<AstronomicalObject> satellites = new List<AstronomicalObject>();

        public string Name { get; }
        public AstronomicalObject Primary { get; private set; }
        public IEnumerable<AstronomicalObject> Satellites => satellites;

        public AstronomicalObject(string name)
        {
            Name = name;
        }

        public void SetPrimary(AstronomicalObject primary)
        {
            Primary = primary ?? throw new ArgumentNullException(nameof(primary));
        }

        public void AddSatellite(AstronomicalObject satellite)
        {
            if (satellite == null) throw new ArgumentNullException(nameof(satellite));

            satellite.SetPrimary(this);
            this.satellites.Add(satellite);
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}";
        }
    }
}