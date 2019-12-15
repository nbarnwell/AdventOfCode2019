namespace FuelManagementSystem
{
    public interface IDistanceCalculator
    {
        int GetDistance(Coords start, Coords end);
    }
}