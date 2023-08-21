using Planetary.Interfaces;
using System.Collections.Generic;
using UnityEngine;

namespace Planetary.Core
{
    public class PlanetarySystemFactory : MonoBehaviour, IPlanetarySystemFactory
    {
        const float PlanetMassMultiplier = 1.5f;

        [SerializeField] MassClassSpecifications massClassSpecifications;
        [SerializeField] PlanetarySystem planetSystemPrefab;
        [SerializeField] PlanetaryObject planetPrefab;

        List<IPlanetaryObject> planetObjects = new List<IPlanetaryObject>();

        public IPlanetarySystem Create(double mass)
        {
            ClearPlanetList();
            PlanetarySystem planetarySystem = CreatePlanetarySystem();

            CreatePlanets(mass, planetarySystem);

            return planetarySystem;
        }

        private void ClearPlanetList()
        {
            planetObjects.Clear();
        }

        private PlanetarySystem CreatePlanetarySystem()
        {
            PlanetarySystem instance = Instantiate(planetSystemPrefab);
            return instance;
        }

        private void CreatePlanets(double mass, IPlanetarySystem planetarySystem)
        {
            double currentTotalMass = 0;
            double maxPlanetMass = CalculateMaxPlanetMass(mass);

            while (currentTotalMass < mass)
            {
                double planetMass = GenerateRandomPlanetMass(maxPlanetMass);
                MassClassSpecifications.MassClass massClass = massClassSpecifications.GenerateClassByMass((float)planetMass);

                if (CanAddPlanet(currentTotalMass, planetMass, mass))
                {
                    CreatePlanet(planetarySystem, massClass, (float)planetMass);
                    currentTotalMass += planetMass;
                }
                else
                {
                    planetMass = mass - currentTotalMass;
                    massClass = massClassSpecifications.GenerateClassByMass((float)planetMass);
                    CreatePlanet(planetarySystem, massClass, (float)planetMass);
                    break;
                }
            }
            planetarySystem.planetaryObjects = planetObjects;
        }

        private void CreatePlanet(IPlanetarySystem planetarySystem, MassClassSpecifications.MassClass massClass, float mass)
        {
            IPlanetaryObject planetObject = InstantiatePlanet(planetarySystem);
            ConfigurePlanet(planetObject, massClass, mass);
            planetObjects.Add(planetObject);
        }

        private double CalculateMaxPlanetMass(double totalMass)
        {
            if (totalMass < massClassSpecifications.GetMaxMass())
            {
                return totalMass / PlanetMassMultiplier;
            }
            else
            {
                return massClassSpecifications.GetMaxMass();
            }
        }

        private bool CanAddPlanet(double currentTotalMass, double planetMass, double totalMass)
        {
            return currentTotalMass + planetMass <= totalMass;
        }

        private double GenerateRandomPlanetMass(double maxPlanetMass)
        {
            return Random.Range(massClassSpecifications.GetMinMass(), (float)maxPlanetMass);
        }



        private IPlanetaryObject InstantiatePlanet(IPlanetarySystem planetarySystem)
        {
            return Instantiate(planetPrefab, planetarySystem.transform);
        }

        private void ConfigurePlanet(IPlanetaryObject planetObject, MassClassSpecifications.MassClass massClass, float mass)
        {
            planetObject.mass = mass;
            planetObject.massClass = massClass.massClassEnum;

            float massPercentage = (mass - massClass.massFrom) / (massClass.massTo - massClass.massFrom);
            planetObject.radius = Mathf.Lerp(massClass.radiusFrom, massClass.radiusTo, massPercentage) / 2;

            if (planetObjects.Count == 0)
            {
                planetObject.orbitalOffset = planetObject.radius * PlanetMassMultiplier;
            }
            else
            {
                IPlanetaryObject prevPlanet = planetObjects[planetObjects.Count - 1];
                planetObject.orbitalOffset = prevPlanet.radius + prevPlanet.orbitalOffset + planetObject.radius * PlanetMassMultiplier;
            }
        }
    }
}