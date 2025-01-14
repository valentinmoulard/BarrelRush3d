using Base.Ui.Buttons;

namespace Game.Ui.Buttons
{
    public class Button_UnlockItem : Button_Base
    {
        public System.Action OnClickUnlockItemButton;

        protected override void OnClick()
        {
            OnClickUnlockItemButton?.Invoke();
        }
    }
}