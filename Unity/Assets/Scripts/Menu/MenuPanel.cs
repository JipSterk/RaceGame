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

        private float _resetPosition;

        protected virtual void Start() => _resetPosition = ((RectTransform) transform).anchoredPosition.y;

        public virtual void MoveInToViewPort()
        {
            ((RectTransform) transform).DOAnchorPosY(0, _duration);
            EventSystem.current.SetSelectedGameObject(_firstSelected ? _firstSelected : null);
        }

        public void MoveOutOfViewPort()
        {
            ((RectTransform) transform).DOAnchorPosY(_resetPosition, _duration);
            EventSystem.current.SetSelectedGameObject(_previousMenuPanel ? _previousMenuPanel.FirstSelected : null);
        }

        public abstract void Close();

        protected abstract bool HasChanges();

        public abstract void Save();

        public abstract void Discard();
    }
}