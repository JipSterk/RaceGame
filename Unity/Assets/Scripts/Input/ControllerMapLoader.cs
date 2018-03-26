using Rewired;
using UnityEngine;

namespace RaceGame.Input
{
    public class ControllerMapLoader : MonoBehaviour
    {
        [SerializeField] private int _playerId;
        [SerializeField] private string _categoryName;

        private void Start() => LoadMap();

        public void LoadMap()
        {
            var mapHelper = ReInput.players.GetPlayer(_playerId).controllers.maps;

            mapHelper.ClearMapsForController(ControllerType.Joystick, 0, _categoryName, true);
            mapHelper.LoadMap(ControllerType.Joystick, 0, _categoryName, "Default", true);
        }
    }
}