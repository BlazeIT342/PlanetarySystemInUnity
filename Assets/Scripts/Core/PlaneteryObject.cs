using Planetery.Interfaces;
using UnityEngine;

namespace Planetery.Core
{
    public class PlaneteryObject : MonoBehaviour, IPlaneteryObject
    {
        [SerializeField] float rotationSpeed = 20f;
        [SerializeField] float orbitRadius = 40f;
        public MassClassEnum massClass { get => massClassEnum; set => _ = massClassEnum; }
        public double mass { get => planetMass; set => _ = planetMass; }

        MassClassEnum massClassEnum;
        double planetMass;

        private void Start()
        {
            rotationSpeed = Random.Range(20, 200);
        }

        public void UpdateTransform(Transform orbitCenter, float deltaTime)
        {
            transform.position = orbitCenter.position + Quaternion.Euler(0f, deltaTime * rotationSpeed, 0f) * (Vector3.forward * orbitRadius);
        }
    }
}