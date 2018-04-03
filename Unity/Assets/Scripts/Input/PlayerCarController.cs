using Rewired;
using UnityEngine;

namespace RaceGame.Input
{
    public class PlayerCarController : BaseCarController
    {
        [SerializeField] private int _playerId;
        
        private Player _player;

        protected override void Start()
        {
            base.Start();

            _player = ReInput.players.GetPlayer(_playerId);
        }

        protected override void FixedUpdate()
        {
            var horizontal = _player.GetAxis("Horizontal");
            var vertical = _player.GetAxis("Vertical");

            CarController.Steer(vertical, horizontal);
        }
    }
}