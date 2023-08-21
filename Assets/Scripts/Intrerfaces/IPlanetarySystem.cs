using System.Collections.Generic;
using UnityEngine;

namespace Planetary.Interfaces
{
    public interface IPlanetarySystem
    {
        public Transform transform { get; }
        public IEnumerable<IPlanetaryObject> planetaryObjects { get; set; }
        public void UpdatePlanetarySystem(float deltaTime);
    }
}