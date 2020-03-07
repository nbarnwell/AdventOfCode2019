namespace OrbitSystem
{
    public class Orbit
    {
        public AstronomicalObject Primary { get; }
        public AstronomicalObject Satellite { get; }

        public Orbit(AstronomicalObject primary, AstronomicalObject satellite)
        {
            this.Satellite = satellite ?? throw new System.ArgumentNullException(nameof(satellite));
            this.Primary = primary ?? throw new System.ArgumentNullException(nameof(primary));
        }
    }
}