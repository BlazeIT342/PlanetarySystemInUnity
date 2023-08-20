using Planetary.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Planetary.Core
{
    public class PlanetarySystem : MonoBehaviour, IPlanetarySystem
    {
        public List<IPlanetaryObject> planetaryObjectsList = new List<IPlanetaryObject>();

        public IEnumerable<IPlanetaryObject> planetaryObjects { get => planetaryObjectsList; set => planetaryObjectsList = (List<IPlanetaryObject>)value; }

        private void Update()
        {
            UpdateSystem(Time.deltaTime);
        }

        public void UpdateSystem(float deltaTime)
        {
            foreach (IPlanetaryObject planeteryObject in planetaryObjects)
            {
                planeteryObject.RotationUpdate(deltaTime);
            }
        }
    }
}