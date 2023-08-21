using UnityEngine;
using UnityEngine.UI;

namespace Planetary.UI
{
    public class CameraZoom : MonoBehaviour
    {
        public Slider zoomSlider;

        private void Start()
        {
            zoomSlider.onValueChanged.AddListener(UpdateZoom);
            zoomSlider.value = transform.position.y;
        }

        private void UpdateZoom(float zoomLevel)
        {
            transform.position = new Vector3(transform.position.x, zoomLevel, transform.position.z);
        }
    }
}