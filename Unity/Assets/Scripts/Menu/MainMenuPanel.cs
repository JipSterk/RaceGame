using UnityEngine;

namespace RaceGame.Menu
{
    public class MainMenuPanel : MenuPanel
    {
        [SerializeField] private MenuPanelDialogue _menuPanelDialogue;

        public override void Close()
        {
            if (HasChanges())
            {
                _menuPanelDialogue.MoveInToViewPort();
                return;
            }

            MoveOutOfViewPort();
        }
        
        protected override bool HasChanges() => true;

        public override void Save()
        {
        }

        public override void Discard() => Application.Quit();
    }
}