using System.Collections.Generic;

namespace Planetery.Interfaces
{
    public interface IPlaneterySystem
    {
        public IEnumerable<IPlaneteryObject> planeteryObjects { get; set; }
        public void UpdateSystem(float deltaTime);
    }
}