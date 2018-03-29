using System;
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
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _fill;

        private Player _player;
        private float _currentTime;
        private int _playerId;
        private Action _callBack;

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
                        _currentTime -= Time.deltaTime;
                        _slider.value += Time.deltaTime;
                        if (_currentTime <= 0) Join();
                    }
                    else if (!Joined)
                    {
                        if (!(_currentTime < 2)) return;
                        _currentTime += Time.deltaTime;
                        _slider.value -= Time.deltaTime;
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
            Kind = _player.controllers.joystickCount > 0 ? Kind.Player : Kind.Ai;

            transform.name = Kind == Kind.Player ? $"Player {_playerId + 1}" : $"AI {_playerId + 1}";
            _textMeshProUgui.__(Kind == Kind.Player ? "menu-play-player" : "menu-play-ai", _playerId + 1);

            _slider.maxValue = _joinTime;

            _currentTime = _joinTime;
            _callBack = callBack;

            AssignController();
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
            _callBack();
        }

        public void Discard()
        {
            Joined = false;
            _fill.color = new Color(255, 255, 255, 255);
            _slider.value = 0;
            _currentTime = _joinTime;
        }
    }
}