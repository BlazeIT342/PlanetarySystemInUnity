using Planetary.Interfaces;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Planetary.Core
{
    public class PlanetarySystemFactory : MonoBehaviour, IPlanetarySystemFactory
    {
        [SerializeField] TMP_InputField inputField;
        public MassClassSpecifications massClassSpecifications;
        public PlanetaryObject planetPrefab;
        public PlanetarySystem planetSystemPrefab;
        List<PlanetarySystem> planetSystems = new List<PlanetarySystem>();

        public IPlanetarySystem Create(double maxTotalMass)
        {
            if (planetSystems.Count != 0)
            {
                Destroy(planetSystems[0].gameObject);
                planetSystems.Remove(planetSystems[0]);
            }
            PlanetarySystem planetarySystem = CreatePlanetarySystem();
            planetSystems.Add(planetarySystem);
            double currentTotalMass = 0;
            double maxPlanetMass = 0;
            if (maxTotalMass < 5000)
            {
                maxPlanetMass = maxTotalMass /2;
            }
            else
            {
                maxPlanetMass = 5000;
            }

            while (true)
            {
                double planetMass = Random.Range(0.00001f, (float)maxPlanetMass);
                
                if (maxTotalMass > currentTotalMass + planetMass)
                {
                    currentTotalMass += planetMass;
                    MassClassSpecifications.MassClass massClass = massClassSpecifications.GenerateClassByMass((float)planetMass);
                    CreatePlanet(planetarySystem, massClass, (float)planetMass);
                }
                else
                {
                    planetMass = maxTotalMass - currentTotalMass;
                    MassClassSpecifications.MassClass massClass = massClassSpecifications.GenerateClassByMass((float)planetMass);
                    CreatePlanet(planetarySystem, massClass, (float)planetMass);
                    break;
                }
            }
            return planetarySystem;
            //float numberOfPlanets = Random.Range(2,2);
            //print(numberOfPlanets);
            //if (planetSystems.Count != 0)
            //{
            //    Destroy(planetSystems[0].gameObject);
            //    planetSystems.Remove(planetSystems[0]);
            //}

            //List<float> randomMasses = new List<float>();
            //double totalRandomMass = 0;

            //// Генерация случайных масс для планет
            //for (int i = 0; i < numberOfPlanets; i++)
            //{
            //    float randomMass = Random.Range(0f, (float)maxTotalMass);
            //    randomMasses.Add(randomMass);
            //    totalRandomMass += randomMass;
            //}

            //// Нормализация масс
            //List<float> normalizedMasses = new List<float>();
            //foreach (float randomMass in randomMasses)
            //{
            //    float normalizedMass = randomMass / (float)totalRandomMass;
            //    normalizedMasses.Add(normalizedMass);
            //}

            // Создание планет с нормализованными массами


            //for (int i = 0; i < numberOfPlanets; i++)
            //{
            //    float planetMass = normalizedMasses[i] * (float)maxTotalMass;
            //    MassClassSpecifications.MassClass randomMassClass = massClassSpecifications.GenerateClassByMass(planetMass);
            //    CreatePlanet(planetarySystem, randomMassClass, planetMass);
            //}


        }

        public void CreateSystem()
        {
            if (double.TryParse(inputField.text, out double maxTotalMass))
            {
                if (maxTotalMass > 30000)
                {
                    Debug.Log("Very high value!");
                    return;
                }
                Create(maxTotalMass);
            }
            else
            {
                Debug.Log("Invalid input for maxTotalMass");
            }

            double mass = 0;
            foreach(var item in planetSystems)
            {
                foreach (var planet in item.GetComponentsInChildren<PlanetaryObject>())
                {
                    mass += planet.mass;
                }
            }
            print(mass);
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