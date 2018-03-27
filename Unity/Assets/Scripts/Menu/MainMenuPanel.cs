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
                _menuPanelDialogue.MoveInToViewPort(null, Application.Quit);
                return;
            }

            MoveOutOfViewPort();
        }
        
        protected override bool HasChanges() => true;
    }
}