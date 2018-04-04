using Rewired;
using UnityEngine;

namespace RaceGame.Input
{
    public class PlayerCarController : BaseCarController
    {
        private Rewired.Player _player;

        public void Init(int playerId)
        {
            _player = ReInput.players.GetPlayer(playerId);

            transform.name = $"Player {playerId}";
        }

        protected override void FixedUpdate()
        {
            var horizontal = _player.GetAxis("Horizontal");
            var vertical = _player.GetAxis("Vertical");

            CarController.Steer(vertical, horizontal);
        }
    }
}