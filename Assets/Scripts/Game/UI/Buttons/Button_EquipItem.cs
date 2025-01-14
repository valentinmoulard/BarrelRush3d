using Base.Ui.Buttons;

namespace Game.Ui.Buttons
{
    public class Button_EquipItem : Button_Base
    {
        public System.Action OnClickEquipButton;

        protected override void OnClick()
        {
            OnClickEquipButton?.Invoke();
        }
    }
}
