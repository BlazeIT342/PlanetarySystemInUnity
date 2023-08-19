namespace Planetary.Interfaces
{
    public interface IPlanetarySystemFactory
    {
        public IPlanetarySystem Create(double mass);
    }
}