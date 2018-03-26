using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RaceGame.Menu
{
    public class MenuPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _firstSelected;
        [SerializeField] private float _duration;

        private Vector3 _resetPosition;
        
        private void Start()
        {
            _resetPosition = transform.position;
        }

        public void MoveInToViewPort()
        {
            transform.DOMove(new Vector3(0, 0, transform.position.z), _duration);
            EventSystem.current.SetSelectedGameObject(_firstSelected);
        }

        public void MoveOutOfViewPort() => transform.DOMove(_resetPosition, _duration);
    }
}