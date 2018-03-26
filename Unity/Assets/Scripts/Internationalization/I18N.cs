using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace RaceGame.Internationalization
{
    public static class I18N
    {
        public static UnityEvent OnLocaleChanged;

        public static string CurrentLocale { get; private set; }

        public const string Key = "Locale";

        public static readonly string[] Locales = { "Dutch", "English", "German" };

        private static Dictionary<string, string> _translations = new Dictionary<string, string>();

        static I18N()
        {
            Locales = Locales.Where(x =>
            {
                var path = $"{Application.streamingAssetsPath}/Locales/{x}.json";
                return File.Exists(path) && !string.IsNullOrEmpty(File.ReadAllText(path));
            }).ToArray();

            //TODO Implement SettingsManger
            var locale = PlayerPrefs.GetString(Key, "English");
            SetLocale(locale);
        }

        private static void SetLocale(string locale)
        {
            if(CurrentLocale == locale) return;

            CurrentLocale = locale;

            var value = File.ReadAllText($"{Application.streamingAssetsPath}/Locales/{CurrentLocale}.json");
            _translations = JsonConvert.DeserializeObject<Dictionary<string, string>>(value);

            //TODO Implement SettingsManger
            PlayerPrefs.SetString(Key, CurrentLocale);

            OnLocaleChanged.Invoke();
        }

        public static void __(this TextMeshProUGUI textMeshProUgui, string key, params object[] args) => textMeshProUgui.text = __(key, args);

        public static string __(string key, params object[] args)
        {
            if (_translations == null) throw new NullReferenceException(nameof(_translations));
            if (_translations[key] == null) throw new KeyNotFoundException(nameof(_translations));
            var translation = _translations[key];

            if (args.Length > 0)
                translation = string.Format(translation, args);

            return translation;
        }
    }
}