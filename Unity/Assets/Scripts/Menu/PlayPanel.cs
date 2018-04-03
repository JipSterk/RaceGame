using System.Collections.Generic;
using System.Linq;
using RaceGame.Input;
using Rewired;
using UnityEngine;
using UnityEngine.UI;

namespace RaceGame.Menu
{
    public class PlayPanel : MenuPanel
    {
        [SerializeField] private PlayerRacerSetup _playerRacerSetup;
        [SerializeField] private Transform _playerRacerSetupTransform;
        [SerializeField] private MenuPanelDialogue _menuStartPanelDialogue;
        [SerializeField] private MenuPanelDialogue _menuStopPanelDialogue;
        [SerializeField] private float _cancelTime;
        [SerializeField] private Slider _slider;

        private Player _player;
        private float _cancelCurrentTime;

        private readonly List<PlayerRacerSetup> _playerRacerSetups = new List<PlayerRacerSetup>();

        private const int PlayerIds = 3;

        protected override void Start()
        {
            base.Start();

            _player = ReInput.players.GetPlayer(0);
            _cancelCurrentTime = _cancelTime;

            for (var i = 0; i <= PlayerIds; i++)
            {
                var playerRacerSetup = Instantiate(_playerRacerSetup, _playerRacerSetupTransform);
                playerRacerSetup.Init(Play, i);
                _playerRacerSetups.Add(playerRacerSetup);
            }
        }

        public override void Close()
        {
            if (HasChanges())
            {
                _menuStopPanelDialogue.MoveInToViewPort();
                return;
            }

            MoveOutOfViewPort();
        }

        private void Update()
        {
            if (_player.GetButton("Return"))
            {
                _cancelCurrentTime -= Time.deltaTime;
                _slider.value += Time.deltaTime;
                if (_cancelCurrentTime <= 0) Close();
            }
            else
            {
                if (!(_cancelCurrentTime < _cancelTime)) return;
                _cancelCurrentTime += Time.deltaTime;
                _slider.value -= Time.deltaTime;
            }
        }

        private void Play()
        {
            if (_playerRacerSetups.Where(x => x.Kind == Kind.Player).All(x => x.Joined))
            {
                _menuStartPanelDialogue.MoveInToViewPort();
            }
        }

        public override void Save()
        {
            //load the play scene
        }

        public override void Discard()
        {
            foreach (var playerRacerSetup in _playerRacerSetups.Where(x => x.Kind == Kind.Player))
                playerRacerSetup.Discard();
        }

        protected override bool HasChanges() => _playerRacerSetups.Where(x => x.Kind == Kind.Player).Any(x => x.Joined);
    }
}