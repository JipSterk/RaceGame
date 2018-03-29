using System.Collections.Generic;
using System.Linq;
using RaceGame.Input;
using UnityEngine;

namespace RaceGame.Menu
{
    public class PlayPanel : MenuPanel
    {
        [SerializeField] private PlayerRacerSetup _playerRacerSetup;
        [SerializeField] private Transform _playerRacerSetupTransform;
        [SerializeField] private ControllerMapLoader _controllerMapLoader;
        [SerializeField] private MenuPanelDialogue _menuStartPanelDialogue;
        [SerializeField] private MenuPanelDialogue _menuStopPanelDialogue;

        private readonly List<PlayerRacerSetup> _playerRacerSetups = new List<PlayerRacerSetup>();

        private const int PlayerIds = 3;

        protected override void Start()
        {
            base.Start();

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

        protected override bool HasChanges() => _playerRacerSetups.Where(x => x.Kind == Kind.Player).All(x => x.Joined);
    }
}