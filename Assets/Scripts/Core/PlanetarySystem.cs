using Planetary.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Planetary.Core
{
    public class PlanetarySystem : MonoBehaviour, IPlanetarySystem
    {
        Transform IPlanetarySystem.transform { get => transform; }

        List<IPlanetaryObject> planetaryObjectsList = new List<IPlanetaryObject>();
        public IEnumerable<IPlanetaryObject> planetaryObjects
        {
            get => planetaryObjectsList;
            set => planetaryObjectsList.AddRange(value ?? new List<IPlanetaryObject>());
        }

        private void Update()
        {
            UpdatePlanetarySystem(Time.deltaTime);
        }

        public void UpdatePlanetarySystem(float deltaTime)
        {
            foreach (IPlanetaryObject planetaryObject in planetaryObjects)
            {
                planetaryObject.RotationUpdate(deltaTime);
            }
        }
    }
}