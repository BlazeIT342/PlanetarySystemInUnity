using Planetary.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Planetary.Core
{
    public class PlanetarySystemFactory : MonoBehaviour, IPlanetarySystemFactory
    {
        public MassClassSpecifications massClassSpecifications;
        public PlanetaryObject planetPrefab;
        public PlanetarySystem planetSystemPrefab;
        List<PlanetarySystem> planetSystems = new List<PlanetarySystem>();

        public IPlanetarySystem Create(double mass)
        {
            PlanetarySystem planetarySystem = CreatePlanetarySystem();
            planetSystems.Add(planetarySystem);
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
            if (planetSystems.Count != 0)
            {

                Destroy(planetSystems[0].gameObject);
                planetSystems.Remove(planetSystems[0]);
            }
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
            planetComponent.massClass = massClass.massClassEnum;

            float massPercentage = (mass - massClass.massFrom) / (massClass.massTo - massClass.massFrom);
            planetComponent.size = Mathf.Lerp(massClass.radiusFrom, massClass.radiusTo, massPercentage);

            if (planetarySystem.planetaryObjectsList.Count == 0)
            {
                planetComponent.orbitalOffset = planetComponent.size;
                planetarySystem.planetaryObjectsList.Add(planetComponent);
                return;
            }
            IPlanetaryObject prevPlanet = planetarySystem.planetaryObjectsList[planetarySystem.planetaryObjectsList.Count - 1];
            planetComponent.orbitalOffset = prevPlanet.size + prevPlanet.orbitalOffset + planetComponent.size / 1.5f;
            planetarySystem.planetaryObjectsList.Add(planetComponent);
        }
    }
}