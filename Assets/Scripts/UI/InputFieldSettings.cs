using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;

namespace Planetary.UI
{
    public class InputFieldSettings : MonoBehaviour
    {
        [SerializeField] TMP_InputField inputField;

        private void Start()
        {
            inputField.onValueChanged.AddListener(OnMassInputValueChanged);
        }

        public void OnMassInputValueChanged(string newValue)
        {
            string cleanedInput = Regex.Replace(newValue, "[^0-9,]", "");
            if (cleanedInput.IndexOf(",") != cleanedInput.LastIndexOf(","))
            {
                cleanedInput = cleanedInput.Remove(cleanedInput.LastIndexOf(","));
            }
            if (cleanedInput != newValue)
            {

                inputField.text = cleanedInput;
            }
        }
    }
}