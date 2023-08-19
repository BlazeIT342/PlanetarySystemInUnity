using UnityEngine;

namespace Planetary.Core
{
    public class PlanetRotation : MonoBehaviour
    {
        [SerializeField] Transform orbitCenter;
        [SerializeField] float rotationSpeed = 20f;
        [SerializeField] float orbitRadius = 5f;

        private void Update()
        {
            Vector3 newPosition = orbitCenter.position + Quaternion.Euler(0f, Time.time * rotationSpeed, 0f) * (Vector3.forward * orbitRadius);
            transform.position = newPosition;
        }
    }
}