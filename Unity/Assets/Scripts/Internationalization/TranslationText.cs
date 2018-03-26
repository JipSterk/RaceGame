using TMPro;
using UnityEngine;

namespace RaceGame.Internationalization
{
    public class TranslationText : MonoBehaviour
    {
        public string Key;

        private TextMeshProUGUI _textMeshProUgui;

        private void OnEnable() => I18N.OnLocaleChanged.AddListener(SetText);

        private void Start()
        {
            if (!_textMeshProUgui)
                _textMeshProUgui = GetComponent<TextMeshProUGUI>();

            SetText();
        }

        private void SetText() => _textMeshProUgui.__(Key);

        private void OnDisable() => I18N.OnLocaleChanged.RemoveListener(SetText);
    }
}