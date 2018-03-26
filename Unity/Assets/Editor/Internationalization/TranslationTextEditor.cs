using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;
using RaceGame.Internationalization;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace RaceGame.Editor.Internationalization
{
    public class TranslationTextEditor : EditorWindow
    {
        private IEnumerable<TextMeshProUGUI> _textsWith;
        private IEnumerable<TextMeshProUGUI> _textsWithout;
        private Vector2 _scrollPosition;
        private string _path;

        [MenuItem("Tools/Translations")]
        private static void ShowWindow() => GetWindow<TranslationTextEditor>("Translations Text Editor");

        private void OnFocus()
        {
            _path = $"{Application.dataPath}/translationTextEditorSave.json";
            UpdateTexts();
        }

        private void OnGUI()
        {
            EditorGUILayout.BeginHorizontal();

            if (GUILayout.Button("Update Text items", GUILayout.Height(20), GUILayout.Width(200)))
                UpdateTexts();

            if (GUILayout.Button("Export to JSON", GUILayout.Height(20), GUILayout.Width(200)))
                ExportToJson();

            if (GUILayout.Button("Import from JSON", GUILayout.Height(20), GUILayout.Width(200)))
                ImportFromJson();

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginVertical();
            _scrollPosition = EditorGUILayout.BeginScrollView(_scrollPosition);

            EditorGUILayout.LabelField($"Text items({_textsWithout.Count()}) without TranslationText",
                EditorStyles.boldLabel);

            foreach (var textWithout in _textsWithout)
            {
                if (!textWithout)
                    continue;

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField($"Path: {GetPath(textWithout.transform)}");

                if (GUILayout.Button("Focus", GUILayout.Height(20), GUILayout.Width(200)))
                {
                    EditorGUIUtility.PingObject(textWithout);
                    Selection.activeObject = textWithout;
                }

                if (GUILayout.Button("Add TranslationText", GUILayout.Height(20), GUILayout.Width(200)))
                {
                    textWithout.gameObject.AddComponent<TranslationText>();
                    UpdateTexts();
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.LabelField($"Text items({_textsWith.Count()}) with TranslationText",
                EditorStyles.boldLabel);

            foreach (var textWith in _textsWith)
            {
                if (!textWith)
                    continue;

                EditorGUILayout.BeginHorizontal();

                var format = $"Path: {GetPath(textWith.transform)}";

                EditorGUIUtility.labelWidth = format.Length * 5f;
                EditorGUILayout.LabelField(format);

                var translationText = textWith.GetComponent<TranslationText>();
                EditorGUIUtility.labelWidth = 30;
                translationText.Key = translationText.GetComponent<TextMeshProUGUI>().text = EditorGUILayout.TextField("Key", translationText.Key, GUILayout.Width(300));

                if (GUILayout.Button("Focus", GUILayout.Height(20), GUILayout.Width(200)))
                {
                    EditorGUIUtility.PingObject(translationText);
                    Selection.activeObject = translationText;
                }

                if (GUILayout.Button("Remove TranslationText", GUILayout.Height(20), GUILayout.Width(200)))
                {
                    DestroyImmediate(translationText);
                    UpdateTexts();
                }

                EditorGUILayout.EndHorizontal();
            }

            EditorGUILayout.EndScrollView();
            EditorGUILayout.EndVertical();
        }

        private void ImportFromJson()
        {
            var json = File.ReadAllText(_path);
            var dictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);

            foreach (var data in dictionary)
            {
                var gameObject = GameObject.Find(data.Value);
                var translationText = gameObject.GetComponent<TranslationText>() ??
                                      gameObject.AddComponent<TranslationText>();
                translationText.Key = data.Key;
                translationText.GetComponent<TextMeshProUGUI>().text = data.Key;
            }

            UpdateTexts();
        }

        private void ExportToJson()
        {
            var data = _textsWith.Select(text => new
            {
                text.GetComponent<TranslationText>().Key,
                Path = GetPath(text.transform)
            }).ToDictionary(x => x.Key, y => y.Path);

            var json = JsonConvert.SerializeObject(data);
            File.WriteAllText(_path, json);
        }

        private void UpdateTexts()
        {
            var textWithTemp = new List<TextMeshProUGUI>();
            var textWithoutTemp = new List<TextMeshProUGUI>();

            foreach (var text in GetAllComponentsFromRoot<TextMeshProUGUI>())
            {
                if (text.GetComponent<TranslationText>())
                    textWithTemp.Add(text);
                else
                    textWithoutTemp.Add(text);
            }

            _textsWith = textWithTemp;
            _textsWithout = textWithoutTemp;
        }

        private static string GetPath(Transform transform) => transform.parent
            ? $"{GetPath(transform.transform.parent)}/{transform.name}"
            : $"/{transform.name}";


        public static List<T> GetAllComponentsFromRoot<T>() where T : Component
        {
            var list = new List<T>();
            for (var i = 0; i < SceneManager.sceneCount; i++)
            {
                var scene = SceneManager.GetSceneAt(i);
                if (!scene.isLoaded)
                    continue;

                foreach (var rootGameObject in scene.GetRootGameObjects())
                    list.AddRange(rootGameObject.GetComponentsInChildren<T>(true));
            }

            return list;
        }
    }
}