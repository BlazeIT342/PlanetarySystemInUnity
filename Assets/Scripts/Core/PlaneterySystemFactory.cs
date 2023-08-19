using Planetery.Interfaces;
using UnityEngine;

namespace Planetery.Core
{
    public class PlaneterySystemFactory : MonoBehaviour, IPlaneterySystemFactory
    {
        public MassClassSpecifications massClassSpecifications;
        public PlaneteryObject planetPrefab;
        public PlaneterySystem planetSystemPrefab;

        public IPlaneterySystem Create(double mass)
        {
            PlaneterySystem planeterySystem = CreatePlanetarySystem();
            double totalMass = 0;
            while (mass > totalMass)
            {
                MassClassSpecifications.MassClass randomMassClass = massClassSpecifications.GetRandomMassClass();

                float randomMass = Random.Range(randomMassClass.massFrom, randomMassClass.massTo);
                CreatePlanet(planeterySystem, randomMassClass, randomMass);
                totalMass += randomMass;
            }
            return planeterySystem;
        }

        public void CreateSystem()
        {
            Create(1000);
        }

        private PlaneterySystem CreatePlanetarySystem()
        {
            PlaneterySystem instance = Instantiate(planetSystemPrefab);
            return instance;
        }

        private void CreatePlanet(PlaneterySystem planeterySystem, MassClassSpecifications.MassClass massClass, float mass)
        {
            PlaneteryObject newPlanet = Instantiate(planetPrefab, planeterySystem.transform); 

            IPlaneteryObject planetComponent = newPlanet.GetComponent<IPlaneteryObject>();

            planetComponent.mass = mass; 

            float massPercentage = (mass - massClass.massFrom) / (massClass.massTo - massClass.massFrom);
            float newSize = Mathf.Lerp(massClass.radiusFrom, massClass.radiusTo, massPercentage);
            newPlanet.transform.localScale = Vector3.one * newSize;
            planeterySystem.planeteryObjectsList.Add(planetComponent);
        }
    }
}