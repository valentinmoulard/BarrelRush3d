using Base.Ui.Buttons;
using Base.GameManagement;
using Game.Events;

namespace Game.Ui.Buttons
{
    public class Button_Revive : Button_Base
    {
        protected override void OnClick()
        {
            base.OnClick();
            ManagersAccess.GameStateController.SetState(Base.Ui.UIScreenType.Playing);
            Events_Revive.OnRevive?.Invoke(true);
        }
    }
}