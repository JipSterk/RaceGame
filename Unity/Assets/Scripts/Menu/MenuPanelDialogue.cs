using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RaceGame.Menu
{
    public class MenuPanelDialogue : MonoBehaviour
    {
        [SerializeField] private MenuPanel _previousMenuPanel;
        [SerializeField] private GameObject _firstSelected;
        [SerializeField] private float _duration;

        private Vector3 _resetPosition;

        private Action _save;
        private Action _discard;

        private void Start() => _resetPosition = transform.position;

        internal void MoveInToViewPort(Action save, Action discard)
        {
            _save = save;
            _discard = discard;

            transform.DOMove(new Vector3(0, 0, transform.position.z), _duration);
            EventSystem.current.SetSelectedGameObject(_firstSelected);
        }

        public void MoveOutOfViewPort()
        {
            transform.DOMove(_resetPosition, _duration);
            EventSystem.current.SetSelectedGameObject(_previousMenuPanel.FirstSelected);
        }

        public void Save() => _save?.Invoke();

        public void Discard() => _discard?.Invoke();
    }
}