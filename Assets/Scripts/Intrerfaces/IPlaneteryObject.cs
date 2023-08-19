using Planetery.Core;
using UnityEngine;

namespace Planetery.Interfaces
{
    public interface IPlaneteryObject
    {
        public MassClassEnum massClass { get; set; }
        public double mass { get; set; }
        public void UpdateTransform(Transform orbitCenter, float deltaTime);
    }
}