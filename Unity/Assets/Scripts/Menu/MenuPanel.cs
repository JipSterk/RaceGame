using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RaceGame.Menu
{
    public abstract class MenuPanel : MonoBehaviour
    {
        public GameObject FirstSelected => _firstSelected;

        [SerializeField] private MenuPanel _previousMenuPanel;
        [SerializeField] private GameObject _firstSelected;
        [SerializeField] private float _duration;

        private Vector3 _resetPosition;

        protected virtual void Start() => _resetPosition = transform.position;

        public virtual void MoveInToViewPort()
        {
            transform.DOMove(new Vector3(0, 0, transform.position.z), _duration);
            EventSystem.current.SetSelectedGameObject(_firstSelected);
        }

        public void MoveOutOfViewPort()
        {
            transform.DOMove(_resetPosition, _duration);
            if (_previousMenuPanel)
                EventSystem.current.SetSelectedGameObject(_previousMenuPanel.FirstSelected);
        }

        public abstract void Close();

        protected abstract bool HasChanges();
    }
}