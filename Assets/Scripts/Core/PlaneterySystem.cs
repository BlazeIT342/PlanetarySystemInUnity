using Planetery.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Planetery.Core
{
    public class PlaneterySystem : MonoBehaviour, IPlaneterySystem
    {
        public List<IPlaneteryObject> planeteryObjectsList = new List<IPlaneteryObject>();

        public IEnumerable<IPlaneteryObject> planeteryObjects { get => planeteryObjectsList; set => _ = planeteryObjectsList; }

        private void Update()
        {
            UpdateSystem(Time.deltaTime);
        }

        public void UpdateSystem(float deltaTime)
        {
            foreach (IPlaneteryObject planeteryObject in planeteryObjects)
            {
                planeteryObject.UpdateTransform(transform, deltaTime);               
            }
        }

        public void SetPlanets()
        {
            planeteryObjects = planeteryObjectsList;
        }
    }
}