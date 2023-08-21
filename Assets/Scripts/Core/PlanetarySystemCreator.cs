using Planetary.Interfaces;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Planetary.Core
{
    public class PlanetarySystemCreator : MonoBehaviour
    {
        const int MaxPlanetSystemAllowableMass = 1000000;

        [SerializeField] PlanetarySystemFactory planetarySystemFactory;
        [SerializeField] TMP_InputField inputField;
        [SerializeField] TextMeshProUGUI errorText;

        List<PlanetarySystem> planetSystems = new List<PlanetarySystem>();

        public void CreateNewPlanetarySystem()
        {
            ClearPlanetLists();
            IPlanetarySystem newSystem = planetarySystemFactory.Create(GetSystemMass());
            planetSystems.Add((PlanetarySystem)newSystem);
        }

        private double GetSystemMass()
        {
            if (double.TryParse(inputField.text, out double maxTotalMass))
            {
                if (maxTotalMass > MaxPlanetSystemAllowableMass)
                {
                    ErrorMessage("Very large mass!");
                    return 0;
                }
                return maxTotalMass;
            }
            else
            {
                ErrorMessage("Wrong mass input!");
                return 0;
            }
        }

        private void ErrorMessage(string text)
        {
            errorText.text = text;
            errorText.GetComponent<Animation>().Stop();
            errorText.GetComponent<Animation>().Play();
        }

        private void ClearPlanetLists()
        {
            if (planetSystems.Count != 0)
            {
                Destroy(planetSystems[0].gameObject);
                planetSystems.Remove(planetSystems[0]);
            }
        }
    }
}