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

        [SerializeField] GameObject planet;

        MassClassEnum massClassEnum;

        double planetMass;
        float planetRadius;
        float orbitalOffsetValue;

        float rotationSpeed = 100f;

        private void Start()
        {
            InitializePlanet();
        }

        public void RotationUpdate(float deltaTime)
        {
            transform.Rotate(Vector3.up * rotationSpeed * deltaTime);
        }

        private void InitializePlanet()
        {
            rotationSpeed = Random.Range(50, 250);
            planet.transform.position = new Vector3(transform.position.x, transform.position.y, orbitalOffset);
            planet.transform.localScale = Vector3.one * radius * 2;
            GenerateRandomColor();
        }

        private void GenerateRandomColor()
        {
            Color randomColor = Random.ColorHSV(0f, 1f, 0.5f, 1f, 0.8f, 1f, 1f, 1f);
            planet.GetComponent<Renderer>().material.color = randomColor;
            planet.GetComponent<TrailRenderer>().material.color = randomColor;
        }
    }
}