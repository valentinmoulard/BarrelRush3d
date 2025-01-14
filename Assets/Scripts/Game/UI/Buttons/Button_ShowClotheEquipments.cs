using Base.Ui.Buttons;

namespace Game.Ui.Buttons
{
    public class Button_ShowClotheEquipments : Button_Base
    {
        public System.Action OnClickShowClotheEquipments;

        protected override void OnClick()
        {
            OnClickShowClotheEquipments?.Invoke();
        }
    }
}