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

        private float _resetPosition;

        private void Start() => _resetPosition = ((RectTransform)transform).anchoredPosition.y;

        internal void MoveInToViewPort()
        {
            ((RectTransform) transform).DOAnchorPosY(0, _duration);
            EventSystem.current.SetSelectedGameObject(_firstSelected ? _firstSelected : null);
        }

        public void MoveOutOfViewPort()
        {
            ((RectTransform) transform).DOAnchorPosY(_resetPosition, _duration);
            EventSystem.current.SetSelectedGameObject(_previousMenuPanel ? _previousMenuPanel.FirstSelected : null);
        }
    }
}