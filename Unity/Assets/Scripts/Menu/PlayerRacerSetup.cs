using System;
using System.Linq;
using RaceGame.Internationalization;
using Rewired;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RaceGame.Menu
{
    public class PlayerRacerSetup : MonoBehaviour
    {
        public bool Joined { get; private set; }
        public Kind Kind { get; private set; }

        [SerializeField] private float _joinTime;
        [SerializeField] private TextMeshProUGUI _textMeshProUgui;
        [SerializeField] private Slider _joinSlider;
        [SerializeField] private Image _fill;

        private Rewired.Player _player;
        private float _joinCurrentTime;

        private int _playerId;
        private event Action CallBack;

        private void Start()
        {
            ReInput.ControllerConnectedEvent += OnReInputOnControllerConnectedEvent;
            ReInput.ControllerDisconnectedEvent += OnReInputOnControllerConnectedEvent;
        }

        private void OnReInputOnControllerConnectedEvent(
            ControllerStatusChangedEventArgs controllerStatusChangedEventArgs)
        {
            if (controllerStatusChangedEventArgs.controllerType == ControllerType.Joystick)
            {
                AssignController();
            }

            Kind = _player.controllers.joystickCount > 0 ? Kind.Player : Kind.Ai;

            transform.name = Kind == Kind.Player ? $"Player {_playerId + 1}" : $"AI {_playerId + 1}";
            _textMeshProUgui.__(Kind == Kind.Player ? "menu-play-player" : "menu-play-ai", _playerId + 1);
        }

        private void Update()
        {
            switch (Kind)
            {
                case Kind.Ai:
                    break;
                case Kind.Player:
                    if (_player.GetButton("Join") && !Joined)
                    {
                        _joinCurrentTime -= Time.deltaTime;
                        _joinSlider.value += Time.deltaTime;
                        if (_joinCurrentTime <= 0) Join();
                    }
                    else if (!Joined)
                    {
                        if (!(_joinCurrentTime < _joinTime)) return;
                        _joinCurrentTime += Time.deltaTime;
                        _joinSlider.value -= Time.deltaTime;
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Init(Action callBack, int playerId)
        {
            _playerId = playerId;
            _player = ReInput.players.GetPlayer(_playerId);

            AssignController();

            Kind = _player.controllers.joystickCount > 0 || _player.controllers.hasKeyboard ? Kind.Player : Kind.Ai;

            transform.name = Kind == Kind.Player ? $"Player {_playerId + 1}" : $"AI {_playerId + 1}";
            _textMeshProUgui.__(Kind == Kind.Player ? "menu-play-player" : "menu-play-ai", _playerId + 1);

            _joinSlider.maxValue = _joinTime;

            _joinCurrentTime = _joinTime;
            CallBack = callBack;
        }

        private void AssignController()
        {
            foreach (var joystick in ReInput.controllers.Joysticks)
            {
                if (ReInput.controllers.IsJoystickAssigned(joystick)) continue;

                if (_player.controllers.joystickCount > 0) continue;
                _player.controllers.AddController(joystick, true);
            }
        }

        private void Join()
        {
            Joined = true;
            _fill.color = Color.green;
            CallBack?.Invoke();
        }

        public void Discard()
        {
            Joined = false;
            _fill.color = new Color(255, 255, 255, 255);
            _joinSlider.value = 0;
            _joinCurrentTime = _joinTime;
        }
    }
}