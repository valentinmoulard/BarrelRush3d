using Base.Ui.Buttons;
using Base.GameManagement;

namespace Game.Ui.Buttons
{
    public class Button_RestartGame : Button_Base
    {
        protected override void OnClick()
        {
            base.OnClick();
            ManagersAccess.LevelManager.RestartScene();
        }
    }
}