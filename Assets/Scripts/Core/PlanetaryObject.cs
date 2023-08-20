using Planetary.Interfaces;
using UnityEngine;

namespace Planetary.Core
{
    public class PlanetaryObject : MonoBehaviour, IPlanetaryObject
    {
        public MassClassEnum massClass { get => massClassEnum; set => massClassEnum = value; }
        public double mass { get => planetMass; set => planetMass = value; }
        public float radius { get => planetRadius; set => planetRadius = value; }
        public float orbitalOffset { get => orbitalOffsetValue; set => orbitalOffsetValue = value; }

        [SerializeField] float rotationSpeed = 20f;
        [SerializeField] GameObject planet;
        [SerializeField] MassClassEnum massClassEnum;
        [SerializeField] double planetMass;
        [SerializeField] float planetRadius;
        [SerializeField] float orbitalOffsetValue;

        private void Start()
        {
            rotationSpeed = Random.Range(50, 250);
            planet.transform.position = new Vector3(transform.position.x, transform.position.y, orbitalOffset);
            planet.transform.localScale = Vector3.one * radius * 2;
        }

        public void RotationUpdate(float deltaTime)
        {
            transform.Rotate(Vector3.up * rotationSpeed * deltaTime);
        }
    }
}