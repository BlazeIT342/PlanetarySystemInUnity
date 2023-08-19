using Planetery.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Planetery.Core
{
    public class PlaneterySystem : MonoBehaviour, IPlaneterySystem
    {
        public Transform orbitCenter;
        public float orbitRadius;
        public float minDistanceBetweenPlanets = 1f;

        List<IPlaneteryObject> planeteryObjectsList = new List<IPlaneteryObject>();

        public IEnumerable<IPlaneteryObject> planeteryObjects { get => planeteryObjectsList; set => _ = planeteryObjectsList; }

        public void UpdateSystem(float deltaTime)
        {
            foreach (IPlaneteryObject planeteryObject in planeteryObjects)
            {
                planeteryObject.UpdateTransform(transform, deltaTime);
            }
        }
    }
}