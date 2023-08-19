namespace Planetery.Interfaces
{
    public interface IPlaneterySystemFactory
    {
        public IPlaneterySystem Create(double mass);
    }
}