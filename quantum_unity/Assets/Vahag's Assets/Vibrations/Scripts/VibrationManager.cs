using Lofelt.NiceVibrations;
using UnityEngine;

namespace Vibrations.Scripts
{
    public class VibrationManager : MonoBehaviour
    {
        [SerializeField] private SwitchToggle switchToggle;
    
        private static bool _vibrationOn = true;

        private void Awake()
        {
            switchToggle.Init();
            switchToggle.Toggle.onValueChanged.AddListener(VibrationSetActive);
            switchToggle.Toggle.isOn = PlayerPrefs.GetInt("Vibration", 1) == 1;
            _vibrationOn = switchToggle.Toggle.isOn;
        }

        private void OnDestroy()
        {
            switchToggle.Toggle.onValueChanged.RemoveAllListeners();
        }
    
        public static void VibrateVeryLight()
        {
            if (_vibrationOn)
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.VeryLight);
        }
    
        public static void VibrateLight()
        {
            if (_vibrationOn)
                HapticPatterns.PlayPreset(HapticPatterns.PresetType.LightImpact);
        }

        private static void VibrationSetActive(bool active)
        {
            _vibrationOn = active;

            PlayerPrefs.SetInt("Vibration", _vibrationOn ? 1 : 0);
        }
    }
}