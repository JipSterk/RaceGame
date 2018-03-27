using Newtonsoft.Json;
using UnityEngine;

namespace RaceGame.Internationalization
{
    public struct Settings
    {
        public string Locale { get; set; }
        public float Volume { get; set; }

        private const string Key = "Settings";

        private static readonly Settings DefaultSettings = new Settings
        {
            Locale = "English",
            Volume = 0.7f
        };

        public void Save() => PlayerPrefs.SetString(Key, JsonConvert.SerializeObject(this));

        public static Settings Load() =>
            JsonConvert.DeserializeObject<Settings>(PlayerPrefs.GetString(Key,
                JsonConvert.SerializeObject(DefaultSettings)));
    }
}