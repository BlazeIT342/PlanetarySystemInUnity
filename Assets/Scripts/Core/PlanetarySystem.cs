using Planetary.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Planetary.Core
{
    public class PlanetarySystem : MonoBehaviour, IPlanetarySystem
    {
        public IEnumerable<IPlanetaryObject> planetaryObjects { get => planetaryObjectsList; set => planetaryObjectsList = (List<IPlanetaryObject>)value; }
        Transform IPlanetarySystem.transform { get => transform; }

        public List<IPlanetaryObject> planetaryObjectsList = new List<IPlanetaryObject>();

        private void Update()
        {
            UpdateSystem(Time.deltaTime);
        }

        public void UpdateSystem(float deltaTime)
        {
            foreach (IPlanetaryObject planetaryObject in planetaryObjects)
            {
                planetaryObject.RotationUpdate(deltaTime);
            }
        }
    }
}