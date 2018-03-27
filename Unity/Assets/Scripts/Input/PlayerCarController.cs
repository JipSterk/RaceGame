using Rewired;
using UnityEngine;

namespace RaceGame.Input
{
    public class PlayerCarController : BaseCarController
    {
        [SerializeField] private int _playerId;
        [SerializeField] private string _categoryName;

        private Player _player;

        protected override void Start()
        {
            base.Start();

            _player = ReInput.players.GetPlayer(_playerId);

            _player.controllers.maps.ClearMapsForController(ControllerType.Joystick, 0, _categoryName, true);
            _player.controllers.maps.LoadMap(ControllerType.Joystick, 0, _categoryName, "Default", true);
        }

        protected override void FixedUpdate()
        {
            var horizontal = _player.GetAxis("Horizontal");
            var vertical = _player.GetAxis("Vertical");

            CarController.Steer(vertical, horizontal);
        }
    }
}