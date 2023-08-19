using System.Collections.Generic;

namespace Planetary.Interfaces
{
    public interface IPlanetarySystem
    {
        public IEnumerable<IPlanetaryObject> planetaryObjects { get; set; }
        public void UpdateSystem(float deltaTime);
    }
}