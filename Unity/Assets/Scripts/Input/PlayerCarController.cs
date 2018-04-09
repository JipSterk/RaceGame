using RaceGame.Car;
using RaceGame.Level;
using Rewired;
using UnityEngine;

namespace RaceGame.Input
{
    public class PlayerCarController : BaseCarController
    {
        public Level.Player Player { get; private set; }

        private Rewired.Player _player;

        private CarController _carController;

        public void Init(int playerId)
        {
            Player = (Level.Player) playerId;
            _player = ReInput.players.GetPlayer(playerId);

            _carController = GetComponent<CarController>();

            transform.name = $"Player {playerId}";

            var checkPoint = GameManager.Instance.GetCheckPoint(0);
            GameManager.Instance.RegisterCarController(checkPoint, Player, _carController);

            GetComponentInChildren<ParticleSystem>().Play();
        }

        protected override void FixedUpdate()
        {
            var horizontal = _player.GetAxis("Horizontal");
            var vertical = _player.GetAxis("Vertical");

            _carController.Steer(vertical, horizontal);
        }
    }
}