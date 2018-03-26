using UnityEngine;

namespace RaceGame.Menu
{
    public class MenuLoader : MonoBehaviour
    {
        [SerializeField] private MenuPanel _menuPanel;

        private void Start() => _menuPanel.MoveInToViewPort();
    }
}