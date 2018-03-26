using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RaceGame.Menu
{
    public class MenuPanelDialogue: MonoBehaviour
    {
        [SerializeField] private GameObject _firstSelected;
        [SerializeField] private float _duration;

        private Vector3 _resetPosition;

        private Action _save;
        
        private void Start() => _resetPosition = transform.position;

        internal void MoveInToViewPort(Action save)
        {
            _save = save;

            transform.DOMove(new Vector3(0, 0, transform.position.z), _duration);
            EventSystem.current.SetSelectedGameObject(_firstSelected);
        }

        public void MoveOutOfViewPort() => transform.DOMove(_resetPosition, _duration);

        public void Save() => _save();
    }
}