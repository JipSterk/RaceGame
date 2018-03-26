using System;
using System.Collections.Generic;
using RaceGame.Internationalization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RaceGame.Menu
{
    public class SettingsPanel : MenuPanel
    {
        [SerializeField] private Toggle _fullScreenToggle;
        [SerializeField] private TMP_Dropdown _localeDropdown;
        [SerializeField] private TMP_Dropdown _resolutionDropdown;
        [SerializeField] private MenuPanelDialogue _menuPanelDialogue;

        private string _locale;
        private Resolution _resolution;
        private bool _fullScreen;
        private float _volume;

        protected override void Start()
        {
            base.Start();

            SetupDropDown(_resolutionDropdown, Screen.resolutions,
                resolution => $"{resolution.width} X {resolution.height}",
                resolution => resolution.width == Screen.width && resolution.height == Screen.height);

            SetupDropDown(_localeDropdown, I18N.Locales,
                locale => locale,
                locale => locale == I18N.CurrentLocale);
        }

        public override void MoveInToViewPort()
        {
            base.MoveInToViewPort();

            _locale = I18N.CurrentLocale;
            _fullScreenToggle.isOn = Screen.fullScreen;
        }

        private bool HasChanges() => _locale != I18N.CurrentLocale ||
                                       _resolution.width != Screen.width &&
                                       _resolution.height != Screen.height || _fullScreen != Screen.fullScreen;

        public void Close()
        {
            if (HasChanges())
            {
                _menuPanelDialogue.MoveInToViewPort(Save);
                return;
            }

            MoveOutOfViewPort();
        }

        private static void SetupDropDown<T>(TMP_Dropdown tmpDropdown, IReadOnlyList<T> items, Func<T, string> setLabel,
            Func<T, bool> callback)
        {
            var dropDownValue = 0;

            var options = new List<string>(items.Count);

            for (var i = 0; i < items.Count; i++)
            {
                options.Add(setLabel(items[i]));

                if (callback(items[i]))
                    dropDownValue = i;
            }

            tmpDropdown.AddOptions(options);
            tmpDropdown.value = dropDownValue;
            tmpDropdown.RefreshShownValue();
        }

        public void SetLocale(int index) => _locale = I18N.Locales[index];

        public void SetResolution(int index) => _resolution = Screen.resolutions[index];

        public void SetFullScreen(bool value) => _fullScreen = value;

        public void SetVolume(float value) => _volume = value;

        private void Save()
        {
            Screen.SetResolution(_resolution.width, _resolution.height, _fullScreen);
            I18N.SetLocale(_locale);

            new Settings
            {
                Locale = _locale,
                Volume = _volume
            }.Save();
        }
    }
}