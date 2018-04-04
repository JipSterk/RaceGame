using Rewired;
using UnityEngine;

namespace RaceGame.Input
{
    public class ControllerMapLoader : MonoBehaviour
    {
        [SerializeField] private string _categoryName;
        [SerializeField] private bool _startAwake;

        private const int PlayerIds = 3;

        private void Start()
        {
            if (_startAwake)
                LoadMap(_categoryName);
        }

        public void LoadMap(string categoryName)
        {
            for (var i = 0; i <= PlayerIds; i++)
            {
                var mapHelper = ReInput.players.GetPlayer(i).controllers.maps;

                if (i == 0)
                    mapHelper.LoadMap(ControllerType.Keyboard, i, categoryName, "Default", true);

                mapHelper.LoadMap(ControllerType.Joystick, i, categoryName, "Default", true);
            }
        }
    }
}