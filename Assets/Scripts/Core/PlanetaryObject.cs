using Planetary.Interfaces;
using UnityEngine;

namespace Planetary.Core
{
    public class PlanetaryObject : MonoBehaviour, IPlanetaryObject
    {
        public MassClassEnum massClass { get => massClassEnum; set => massClassEnum = value; }
        public double mass { get => planetMass; set => planetMass = value; }
        public float orbitalOffset 
        { 
            get => orbitalOffsetValue; 
            set => orbitalOffsetValue = value; 
        }
        public float size { get => planetSize; set => planetSize = value; }

        [SerializeField] float rotationSpeed = 20f;
        [SerializeField] GameObject planet;

        MassClassEnum massClassEnum;
        double planetMass;
        float planetSize;
        public float orbitalOffsetValue;

        private void Start()
        {
            rotationSpeed = Random.Range(20, 200);
            planet.transform.position = new Vector3(transform.position.x, transform.position.y, orbitalOffsetValue);
            planet.transform.localScale = Vector3.one * planetSize;
        }

        public void RotationUpdate(float deltaTime)
        {
            transform.Rotate(Vector3.up * rotationSpeed * deltaTime);
        }
    }
}