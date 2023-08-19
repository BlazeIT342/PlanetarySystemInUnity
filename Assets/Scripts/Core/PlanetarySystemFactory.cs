using Planetary.Interfaces;
using UnityEngine;

namespace Planetary.Core
{
    public class PlanetarySystemFactory : MonoBehaviour, IPlanetarySystemFactory
    {
        public MassClassSpecifications massClassSpecifications;
        public PlanetaryObject planetPrefab;
        public PlanetarySystem planetSystemPrefab;

        public IPlanetarySystem Create(double mass)
        {
            PlanetarySystem planetarySystem = CreatePlanetarySystem();
            double totalMass = 0;
            while (mass > totalMass)
            {
                MassClassSpecifications.MassClass randomMassClass = massClassSpecifications.GetRandomMassClass();

                float randomMass = Random.Range(randomMassClass.massFrom, randomMassClass.massTo);
                CreatePlanet(planetarySystem, randomMassClass, randomMass);
                totalMass += randomMass;
            }
            return planetarySystem;
        }

        public void CreateSystem()
        {
            Create(100);
        }

        private PlanetarySystem CreatePlanetarySystem()
        {
            PlanetarySystem instance = Instantiate(planetSystemPrefab);
            return instance;
        }

        private void CreatePlanet(PlanetarySystem planetarySystem, MassClassSpecifications.MassClass massClass, float mass)
        {
            PlanetaryObject newPlanet = Instantiate(planetPrefab, planetarySystem.transform); 

            IPlanetaryObject planetComponent = newPlanet.GetComponent<IPlanetaryObject>();

            planetComponent.mass = mass;

            float massPercentage = (mass - massClass.massFrom) / (massClass.massTo - massClass.massFrom);
            float newSize = Mathf.Lerp(massClass.radiusFrom, massClass.radiusTo, massPercentage);
            newPlanet.transform.localScale = Vector3.one * newSize;
            if (planetarySystem.planetaryObjectsList.Count == 0)
            {
                planetComponent.orbitalOffset = newSize;
                planetarySystem.planetaryObjectsList.Add(planetComponent);
                return;
            }
            planetComponent.orbitalOffset = planetarySystem.planetaryObjectsList[planetarySystem.planetaryObjectsList.Count-1].orbitalOffset + newSize;
            planetarySystem.planetaryObjectsList.Add(planetComponent);
        }
    }
}