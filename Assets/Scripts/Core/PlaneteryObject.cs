using Planetery.Interfaces;
using UnityEngine;

namespace Planetery.Core
{
    public class PlaneteryObject : MonoBehaviour, IPlaneteryObject
    {
        float rotationSpeed = 20f;
        [SerializeField] float orbitRadius = 5f;
        public MassClassEnum massClass { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        public double mass { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

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