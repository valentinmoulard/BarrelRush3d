using Base.GameManagement;

namespace Base.Ui.Buttons
{
    public class Button_TapToStart: Button_Base
    {
        protected override void OnClick()
        {
            base.OnClick();
            ManagersAccess.GameStateController.SetState(UIScreenType.Playing);
        }
    }
}