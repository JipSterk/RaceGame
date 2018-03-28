using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace RaceGame.Menu
{
    public class PlayPanel : MenuPanel
    {
        [SerializeField] private PlayerRacerSetup _playerRacerSetup;
        [SerializeField] private Transform _playerRacerSetupTransform;
        [SerializeField] private MenuPanelDialogue _menuPanelDialogue;

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
                _menuPanelDialogue.MoveInToViewPort(null, null);
                return;
            }

            MoveOutOfViewPort();
        }

        private void Play()
        {
            if (_playerRacerSetups.Where(x => x.Kind == Kind.Player).All(x => x.Joined))
            {
                Debug.Log("Play");
            }
        }

        protected override bool HasChanges() => _playerRacerSetups.Where(x => x.Kind == Kind.Player).All(x => x.Joined);
    }
}