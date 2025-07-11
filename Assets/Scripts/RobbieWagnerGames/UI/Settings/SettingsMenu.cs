using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

namespace RobbieWagnerGames.UI
{
    public class SettingsMenu : Menu
    {
        [SerializeField] private AudioMixer mixer;
        [SerializeField] [SerializedDictionary("slider","mixer")] private SerializedDictionary<Slider, string> volumeSettings;

		protected override void Awake()
		{
			InitializeMenu();

			base.Awake();
		}

		private void InitializeMenu()
		{
			foreach (KeyValuePair<Slider, string> setting in volumeSettings)
			{
				setting.Key.maxValue = 0;
				setting.Key.minValue = -80;
				setting.Key.value = 0;

				float volume = PlayerPrefs.GetFloat(setting.Value, setting.Key.value);
				setting.Key.value = volume;
				mixer.SetFloat(setting.Value, volume);
			}
		}

		protected override void OnEnable()
        {
            base.OnEnable();

            backButton.onClick.AddListener(SaveSettings);

			foreach (KeyValuePair<Slider, string> setting in volumeSettings)
				setting.Key.onValueChanged.AddListener((value) => SetMixerVolume(value, setting.Value));
		}

        protected override void OnDisable()
        {
            base.OnDisable();

            backButton.onClick.RemoveListener(SaveSettings);

            foreach (KeyValuePair<Slider, string> setting in volumeSettings)
                setting.Key.onValueChanged.RemoveListener((value) => SetMixerVolume(value, setting.Value));
		}

        public void SaveSettings()
        {
            foreach (KeyValuePair<Slider, string> setting in volumeSettings)
                PlayerPrefs.SetFloat(setting.Value, setting.Key.value);
		}

        private void SetMixerVolume(float value, string parameterName)
        {
			mixer.SetFloat(parameterName, value);
		}
    }
}