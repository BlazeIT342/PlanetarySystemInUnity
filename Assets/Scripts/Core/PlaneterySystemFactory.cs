using Planetery.Interfaces;
using UnityEngine;

namespace Planetery.Core
{
    public class PlaneterySystemFactory : MonoBehaviour, IPlaneterySystemFactory
    {
        public IPlaneterySystem Create(double mass)
        {
            throw new System.NotImplementedException();
        }
    }
}