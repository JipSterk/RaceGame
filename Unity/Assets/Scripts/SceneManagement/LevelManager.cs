using UnityEngine;
using UnityEngine.SceneManagement;

namespace RaceGame.SceneManagement
{
    public class LevelManager : MonoBehaviour
    {
        public static LevelManager Instance =>
            _instance ?? new GameObject(nameof(LevelManager)).AddComponent<LevelManager>();

        private static LevelManager _instance;
        private static readonly string[] Scenes = {"Interface"};

        private void Awake()
        {
            if (_instance)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
        }

        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);

            foreach (var scene in Scenes)
                SceneManager.LoadScene(scene, LoadSceneMode.Additive);
        }
    }
}