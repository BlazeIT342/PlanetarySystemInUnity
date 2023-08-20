using Planetary.Interfaces;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Planetary.Core
{
    public class PlanetarySystemFactory : MonoBehaviour, IPlanetarySystemFactory
    {
        const int MaxPlanetSystemAllowableMass = 1000000;
        const float PlanetMassMultiplier = 1.5f;

        [SerializeField] TMP_InputField inputField;
        [SerializeField] MassClassSpecifications massClassSpecifications;
        [SerializeField] PlanetarySystem planetSystemPrefab;
        [SerializeField] PlanetaryObject planetPrefab;

        List<PlanetarySystem> planetSystems = new List<PlanetarySystem>();
        List<IPlanetaryObject> planetObjects = new List<IPlanetaryObject>();

        public IPlanetarySystem Create(double mass)
        {
            ClearPlanetLists();

            PlanetarySystem planetarySystem = CreatePlanetarySystem();
            planetSystems.Add(planetarySystem);

            CreatePlanets(mass, planetarySystem);

            return planetarySystem;
        }

        private void ClearPlanetLists()
        {
            planetObjects.Clear();
            if (planetSystems.Count != 0)
            {
                Destroy(planetSystems[0].gameObject);
                planetSystems.Remove(planetSystems[0]);
            }
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
        }

        private void CreatePlanet(IPlanetarySystem planetarySystem, MassClassSpecifications.MassClass massClass, float mass)
        {
            IPlanetaryObject planetObject = InstantiatePlanet(planetarySystem);
            ConfigurePlanet(planetarySystem, planetObject, massClass, mass);
            AddPlanetToSystem(planetarySystem, planetObject);
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

        public void CreateSystem()
        {
            if (double.TryParse(inputField.text, out double maxTotalMass))
            {
                if (maxTotalMass > MaxPlanetSystemAllowableMass)
                {
                    Debug.Log("Very high value!");
                    return;
                }
                Create(maxTotalMass);
            }
            else
            {
                Debug.Log("Invalid input for value");
            }

            double mass = 0;
            foreach (var item in planetSystems)
            {
                foreach (var planet in item.GetComponentsInChildren<PlanetaryObject>())
                {
                    mass += planet.mass;
                }
            }
            Debug.Log(mass);
        }

        private IPlanetaryObject InstantiatePlanet(IPlanetarySystem planetarySystem)
        {
            return Instantiate(planetPrefab, planetarySystem.transform);
        }

        private void ConfigurePlanet(IPlanetarySystem planetarySystem, IPlanetaryObject planetObject, MassClassSpecifications.MassClass massClass, float mass)
        {
            planetObject.mass = mass;
            planetObject.massClass = massClass.massClassEnum;

            float massPercentage = (mass - massClass.massFrom) / (massClass.massTo - massClass.massFrom);
            planetObject.radius = Mathf.Lerp(massClass.radiusFrom, massClass.radiusTo, massPercentage) / 2;

            if (planetarySystem.planetaryObjects.Count() == 0)
            {
                planetObject.orbitalOffset = planetObject.radius * PlanetMassMultiplier;
            }
            else
            {
                IPlanetaryObject prevPlanet = planetarySystem.planetaryObjects.Last();
                planetObject.orbitalOffset = prevPlanet.radius + prevPlanet.orbitalOffset + planetObject.radius * PlanetMassMultiplier;
            }
        }

        private void AddPlanetToSystem(IPlanetarySystem planetarySystem, IPlanetaryObject planetObject)
        {
            planetObjects.Add(planetObject);
            planetarySystem.planetaryObjects = planetObjects;
        }
    }
}